using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KAS.Trukman.Storage;
using Trukman.Interfaces;
using KAS.Trukman.Data.Interfaces;
using System.Threading.Tasks;
using KAS.Trukman.Messages;
using Xamarin.Forms;
using KAS.Trukman.Languages;
using Xamarin.Forms.Maps;
using KAS.Trukman.Data.Classes;
using KAS.Trukman.Data.Infos;
using KAS.Trukman.Data.Enums;
using KAS.Trukman.Storage.ParseClasses;
using Parse;

namespace KAS.Trukman.AppContext
{
    #region TrukmanContext
    public static class TrukmanContext
    {
        #region States
        internal enum States
        {
            None,
            Working,
            Completed
        }
        #endregion

        private static readonly LocalStorage _localStorage = null;

        private static States _state = States.None;

        private static System.Timers.Timer _synchronizeTimer = null;

        static TrukmanContext()
        {
            _state = States.None;
            _localStorage = new LocalStorage();
        }

        public static void Initialize()
        {
            Task.Run(async () =>
            {
                if ((_state == States.None) && (!Initialized))
                {
                    _state = States.Working;
                    try
                    {
                        var user = _localStorage.Become();
                        if (user != null)
                        {
                            User = user;
                            Company = await _localStorage.SelectUserCompany();

                            _localStorage.SaveUser(User as User);
                            _localStorage.SaveCompany(Company as Company);

                            _localStorage.SetSettings(LocalStorage.USER_ID_SETTINGS_KEY, User.ID);
                            _localStorage.SetSettings(LocalStorage.COMPANY_ID_SETTINGS_KEY, Company.ID);
                        }
                        else
                        {
                            var userID = _localStorage.GetSettings(LocalStorage.USER_ID_SETTINGS_KEY);
                            var companyID = _localStorage.GetSettings(LocalStorage.COMPANY_ID_SETTINGS_KEY);
                            User = _localStorage.GetUserByID(userID);
                            Company = _localStorage.GetCompanyByID(companyID);
                        }
                        if (User != null)
                            InitializeContext();
                        _state = States.Completed;
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                        ShowToastMessage.Send(exception.Message);
                        _state = States.None;
                    }
                    finally
                    {
                        Initialized = true;
                    }
                }
            });
        }

        public static void RegisterUser(User user)
        {
        }

        private static void InitializeContext()
        {
            if (User.Role == UserRole.UserRoleDriver)
                Driver = new DriverContext(_localStorage);
            else if (User.Role == UserRole.UserRoleDispatch)
                Dispatcher = new DispatcherContext(_localStorage);
            else if (User.Role == UserRole.UserRoleOwner)
                Owner = new OwnerContext(_localStorage);

            Synchronize();
        }

        public static async Task InitializeOwnerContext()
        {
            User = await _localStorage.GetCurrentUser();
            Company = await _localStorage.SelectUserCompany();
            _localStorage.SaveUser(User as User);
            _localStorage.SetSettings(LocalStorage.USER_ID_SETTINGS_KEY, User.ID);
            InitializeContext();
            ShowTopPageMessage.Send();
        }

        public static async Task InitializeDriverContext()
        {
            User = await _localStorage.GetCurrentUser();
            Company = await _localStorage.SelectUserCompany();
            _localStorage.SaveUser(User as User);
            _localStorage.SetSettings(LocalStorage.USER_ID_SETTINGS_KEY, User.ID);
            InitializeContext();
            ShowTopPageMessage.Send();
        }

        public static async Task<User> SelectRequestedUser(string companyID)
        {
            return await _localStorage.SelectRequestedUser(companyID);
        }

        public static async Task<ICompany[]> SelectCompanies(string filter)
        {
            return await _localStorage.SelectCompanies(filter);
        }

        public static async Task<ICompany> SelectCompanyByName(string name)
        {
            return await _localStorage.SelectCompanyByName(name);
        }

        public static async Task<ICompany> SelectUserCompany()
        {
            return await _localStorage.SelectUserCompany();
        }

        public static async Task<ITrip[]> SelectActiveTrips()
        {
            return await _localStorage.SelectActiveTrips();
        }

        public static void InitializeOwnerNotification()
        {
            _localStorage.InitializeOwnerNotification();
        }

        public static async Task<Position> SelectDriverPosition(string tripID)
        {
            return await _localStorage.SelectDriverPosition(tripID);
        }

        public static async Task<ITrip> SelectTripByID(string tripID)
        {
            var trip = await _localStorage.SelectTripByIDExternal(tripID);
            return trip;
        }

        public static async Task<Company> RegisterCompanyAsync(CompanyInfo companyInfo)
        {
            var company = await _localStorage.RegisterCompanyAsync(companyInfo);
            User = await _localStorage.GetCurrentUser();
            Company = company;

            _localStorage.SaveUser(User as User);
            _localStorage.SaveCompany(Company as Company);

            _localStorage.SetSettings(LocalStorage.USER_ID_SETTINGS_KEY, User.ID);
            _localStorage.SetSettings(LocalStorage.COMPANY_ID_SETTINGS_KEY, Company.ID);

            return company;
        }

        public static async Task<Company> RegisterDriverAsync(DriverInfo driverInfo)
        {
            var company = await _localStorage.RegisterDriverAsync(driverInfo);
            User = await _localStorage.GetCurrentUser();
            Company = company;

            _localStorage.SaveUser(User as User);
            _localStorage.SaveCompany(Company as Company);

            _localStorage.SetSettings(LocalStorage.USER_ID_SETTINGS_KEY, User.ID);
            _localStorage.SetSettings(LocalStorage.COMPANY_ID_SETTINGS_KEY, Company.ID);

            return company;
        }

        public static async Task<DriverState> GetDriverState()
        {
            var state = (User != null ? (DriverState)User.Status : DriverState.Waiting);
            try
            {
                var user = await _localStorage.GetCurrentUser();
                if (state != (DriverState)user.Status)
                {
                    User = user;
                    state = (DriverState)User.Status;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            return state;
        }

        public static async Task AcceptDriverToCompany(User user)
        {
            await _localStorage.AcceptDriverToCompany(user);
        }

        public static async Task DeclineDriverToCompany(User user)
        {
            await _localStorage.DeclineDriverToCompany(user);
        }

		static public async Task<ParseCompany> FetchParseCompany(string name)
		{
			var query = new ParseQuery<ParseCompany>()
				.WhereEqualTo("name", name.ToLower());
			var parseCompany = await query.FirstOrDefaultAsync();
			return parseCompany;
		}

		static public async Task<IEnumerable<ParseUser>> GetBrokersFromCompany(ParseCompany company)
		{
			ParseRelation<ParseUser> relation = company.GetRelation<ParseUser> ("brokers");
			ParseQuery<ParseUser> query = relation.Query;
			var brokers = await query.FindAsync ();
			return brokers;
		}

		static public async Task<IEnumerable<ParseUser>> GetDriversFromCompany(ParseCompany company)
		{
			ParseRelation<ParseUser> relation = company.GetRelation<ParseUser> ("drivers");
			ParseQuery<ParseUser> query = relation.Query;
			var brokers = await query.FindAsync ();
			return brokers;
		}


        private static void StartSynchronizeTimer()
        {
            if (_synchronizeTimer == null)
            {
                _synchronizeTimer = new System.Timers.Timer { Interval = 15000 };
                _synchronizeTimer.Elapsed += (sender, args) =>
                {
                    Synchronize();
                };
            }
            _synchronizeTimer.Start();
        }

        private static bool _inSynchronization = false;

        private static void Synchronize()
        {
            Task.Run(async () => {
                if (!_inSynchronization)
                {
                    StopSynchronizeTimer();
                    _inSynchronization = true;
                    try
                    {
                        if (User != null)
                        {
                            if (User.Role == UserRole.UserRoleDriver)
                            {
                                await _localStorage.SynchronizeDriverContext();
                                Driver.Synchronize();
                            }
                            else if (User.Role == UserRole.UserRoleOwner)
                            {
                                _localStorage.SynchronizeOwnerContext();
                                Owner.Synchronize();
                            }
                            else if (User.Role == UserRole.UserRoleDispatch)
                            {
                                _localStorage.SynchronyzeDispatcherContex();
                                Dispatcher.Synchronize();
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                    }
                    finally
                    {
                        _inSynchronization = false;
                        StartSynchronizeTimer();
                    }
                }
            });
        }

        private static void StopSynchronizeTimer()
        {
            if (_synchronizeTimer != null)
                _synchronizeTimer.Stop();
        }

        public static bool AppWorking { get; set; }

        public static DriverContext Driver { get; set; }

        public static OwnerContext Owner { get; set; }

        public static DispatcherContext Dispatcher { get; set; }

        public static IUser User { get; private set; }

        public static ICompany Company { get; private set; }

        public static bool Initialized { get; private set; }
    }
    #endregion
}
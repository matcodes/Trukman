using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KAS.Trukman.Storage;
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
using Trukman.Helpers;
using KAS.Trukman.Data.Route;
using KAS.Trukman.Helpers;
using KAS.Trukman.Extensions;

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
            RouteHelper.Initialize(_localStorage);
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
                            var state = (User != null ? (UserState)User.Status : UserState.Waiting);
                            if (state == UserState.Waiting)
                            {
                                var companyId = _localStorage.GetSettings(LocalStorage.COMPANY_ID_SETTINGS_KEY);
                                Company = _localStorage.GetCompanyByID(companyId);
                            }
                            else
                                Company = await _localStorage.SelectUserCompany();
                            _localStorage.Become();
                        }

                        //var user = _localStorage.Become();
                        //                 if (user != null)
                        //                 {
                        //                     User = user;
                        //                     Company = await _localStorage.SelectUserCompany();

                        //                     _localStorage.SaveUser(User as User);
                        //                     _localStorage.SaveCompany(Company as Company);

                        //                     _localStorage.SetSettings(LocalStorage.USER_ID_SETTINGS_KEY, User.ID);
                        //                     _localStorage.SetSettings(LocalStorage.COMPANY_ID_SETTINGS_KEY, Company.ID);
                        //                 }
                        //                 else
                        //                 {
                        //await ParseUser.LogOutAsync();

                        //                     //var userID = _localStorage.GetSettings(LocalStorage.USER_ID_SETTINGS_KEY);
                        //                     //var companyID = _localStorage.GetSettings(LocalStorage.COMPANY_ID_SETTINGS_KEY);
                        //                     //User = _localStorage.GetUserByID(userID);
                        //                     //Company = _localStorage.GetCompanyByID(companyID);
                        //                 }
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
            }).LogExceptions("TrukmanContext Initialize");
        }

        public static void RegisterUser(User user)
        {
        }

        private static void InitializeContext()
        {
            if (User.Role == UserRole.Driver)
                Driver = new DriverContext(_localStorage);
            else if (User.Role == UserRole.Dispatch)
                Dispatcher = new DispatcherContext(_localStorage);
            else if (User.Role == UserRole.Owner)
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

        public static async Task InitializeDispatcherContext()
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

        public static async Task<Company[]> SelectCompanies(string filter)
        {
            return await _localStorage.SelectCompanies(filter);
        }

        //public static async Task<Company> SelectCompanyByName(string name)
        //{
        //    return await _localStorage.SelectCompanyByName(name);
        //}

        public static async Task<Company> SelectUserCompany()
        {
            return await _localStorage.SelectUserCompany();
        }

        public static async Task<Trip[]> SelectActiveTrips()
        {
            return await _localStorage.SelectActiveTrips(Guid.Parse(TrukmanContext.Company.ID));
        }

        public static async Task<Trip[]> SelectCompletedTrips()
        {
            return await _localStorage.SelectCompletedTrips(Guid.Parse(TrukmanContext.Company.ID));
        }

        public static void InitializeOwnerNotification()
        {
            _localStorage.InitializeOwnerNotification();
        }

        public static async Task<Position> SelectDriverPosition(string tripID)
        {
            return await _localStorage.SelectDriverPosition(tripID);
        }

        public static async Task<Trip> SelectTripByID(string tripID)
        {
            var trip = await _localStorage.SelectTripByIDExternal(tripID);
            return trip;
        }

        public static async Task<ComcheckRequestState> GetComcheckStateAsync(string tripID, ComcheckRequestType requestType)
        {
            var state = await _localStorage.GetComcheckStateAsync(tripID, requestType);
            return state;
        }

        public static async Task<string> GetComcheckAsync(string tripID, ComcheckRequestType requestType)
        {
            var comcheck = await _localStorage.GetComcheckAsync(tripID, requestType);
            return comcheck;
        }

        public static async Task SendComcheckRequestAsync(string tripID, ComcheckRequestType requestType)
        {
            try
            {
                await _localStorage.SendComcheckRequestAsync(tripID, requestType);

                //var message = (requestType == ComcheckRequestType.FuelAdvance ? AppLanguages.CurrentLanguage.OwnerFuelRequestedSystemMessage : AppLanguages.CurrentLanguage.OwnerLumperRequestedSystemMessage);
                //message = String.Format(message, Driver.Trip.JobRef, User.FullName);

                //await _localStorage.SendNotification(Driver.Trip, message);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw new Exception(AppLanguages.CurrentLanguage.CheckInternetConnectionErrorMessage);
            }
        }

        public static async Task CancelComcheckRequestAsync(string tripID, ComcheckRequestType requestType)
        {
            await _localStorage.CancelComcheckRequestAsync(tripID, requestType);
        }

        public static async Task SendJobAlertAsync(string tripID, int alertType, string alertText)
        {
            await _localStorage.SendJobAlertAsync(tripID, alertType, alertText);
        }

        public static async Task<JobAlert[]> SelectJobAlertsAsync()
        {
            var jobAlerts = await _localStorage.SelectJobAlertsAsync(Guid.Parse(TrukmanContext.Company.ID));
            return jobAlerts;
        }

        public static async Task SetJobAlertIsViewedAsync(string jobAlertID, bool isViewed)
        {
            await _localStorage.SetJobAlertIsViewedAsync(jobAlertID, isViewed);
        }

        public static async Task<Advance[]> SelectFuelAdvancesAsync(int requestType)
        {
            var advances = await _localStorage.SelectFuelAdvancesAsync(requestType);
            return advances;
        }

        public static async Task SetAdvanceStateAsync(Advance advance)
        {
            await _localStorage.SetAdvanceStateAsync(advance);
        }

        //public static async Task AddPointsAsync(string jobID, string text, int points)
        //{
        //    await _localStorage.AddPointsAsync(jobID, text, points);
        //}

        public static async Task<int> GetPointsByJobIDAsync(string jobID)
        {
            var points = await _localStorage.GetPointsByJobIDAsync(jobID);
            return points;
        }

        public static async Task<int> GetPointsByDriverIDAsync(string driverID)
        {
            var points = await _localStorage.GetPointsByDriverIDAsync(driverID);
            return points;
        }

        public static async Task<JobPoint[]> SelectJobPointsAsync()
        {
            var jobPoints = await _localStorage.SelectJobPointsAsync();
            return jobPoints;
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

        public static async Task<bool> DriverLogin(DriverInfo driverInfo)
        {
            User = await _localStorage.DriverLogin(driverInfo);
            _localStorage.SaveUser(User as User);
            _localStorage.SetSettings(LocalStorage.USER_ID_SETTINGS_KEY, User.ID);
            return true;
        }

        public static async Task<bool> DispatcherLogin(DispatcherInfo dispatcherInfo)
        {
            User = await _localStorage.DispatcherLogin(dispatcherInfo);
            _localStorage.SaveUser(User as User);
            _localStorage.SetSettings(LocalStorage.USER_ID_SETTINGS_KEY, User.ID);
            return true;
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

        public static async Task<Company> RegisterDispatcherAsync(DispatcherInfo dispatcherInfo)
        {
            var company = await _localStorage.RegisterDispatcherAsync(dispatcherInfo);
            User = await _localStorage.GetCurrentUser();
            Company = company;

            _localStorage.SaveUser(User as User);
            _localStorage.SaveCompany(Company as Company);

            _localStorage.SetSettings(LocalStorage.USER_ID_SETTINGS_KEY, User.ID);
            _localStorage.SetSettings(LocalStorage.COMPANY_ID_SETTINGS_KEY, Company.ID);

            return company;
        }

        public static async Task<bool> Verification(string code)
        {
            try
            {
                var guid = Guid.Parse(User.ID);
                var verified = await _localStorage.Verification(guid, code);
                if (verified)
                {
                    User = await _localStorage.GetCurrentUser();
                    _localStorage.SaveUser(User as User);
                }
                return verified;
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception);
                return false;
            }
        }

        public static async Task<bool> ResendVerificationCode()
        {
            var guid = Guid.Parse(User.ID);
            return await _localStorage.ResendVerificationCode(guid);
        }

        public static async Task<UserState> GetUserState()
        {
            var state = (User != null ? (UserState)User.Status : UserState.Waiting);
            try
            {
                //var user = await _localStorage.GetCurrentUser();
                UserState _userState = UserState.Waiting;
                if (User.Role == UserRole.Driver)
                    _userState = await _localStorage.GetDriverState(Company.ID, User.ID);
                else if (User.Role == UserRole.Dispatch)
                    _userState = await _localStorage.GetDispatcherState(Company.ID, User.ID);
                if (state != _userState)
                {
                    User = await _localStorage.GetCurrentUser();
                    state = _userState;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            return state;
        }

        public static async Task CancelUserRequest()
        {
            if (User.Role == UserRole.Driver)
            await _localStorage.CancelDriverRequest(Company.ID, User.ID);
            else if (User.Role == UserRole.Dispatch)
                await _localStorage.CancelDispatcherRequest(Company.ID, User.ID);

            _localStorage.RemoveUser(User);
            _localStorage.RemoveCompany(Company);
            _localStorage.SetSettings(LocalStorage.COMPANY_ID_SETTINGS_KEY, string.Empty);
            _localStorage.SetSettings(LocalStorage.USER_ID_SETTINGS_KEY, string.Empty);
            User = null;
            Company = null;
        }

        public static async Task AcceptUserToCompany(string companyID, User user)
        {
            await _localStorage.AcceptUserToCompany(companyID, user);
        }

        public static async Task DeclineUserToCompany(string companyID, User user)
        {
            await _localStorage.DeclineUserToCompany(companyID, user);
        }

        public static async Task<Trip> CreateTripAsync(Trip trip)
        {
            var result = await _localStorage.CreateTripAsync(trip);
            return result;
        }

        public static async Task<User[]> SelectBrockersAsync()
        {
            var brockers = await _localStorage.SelectBrockersAsync();
            return brockers;
        }

        public static async Task<User> SaveBrokerAsync(BrokerUser brokerInfo)
        {
            var brockers = await _localStorage.SaveBrokerAsync(brokerInfo);
            return brockers;
        }

        public static async Task<User[]> SelectDriversAsync()
        {
            var users = await _localStorage.SelectDriversAsync();
            return users;
        }

        public static async Task<string> CreateInvoiceForJobAsync(string tripID)
        {
            return await _localStorage.CreateInvoiceForJobAsync(tripID);
        }

        public static async Task<Photo[]> SelectPhotosAsync()
        {
            var photos = await _localStorage.SelectPhotosAsync(Guid.Parse(TrukmanContext.Company.ID));
            return photos;
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
            Task.Run(async () =>
            {
                if (!_inSynchronization)
                {
                    StopSynchronizeTimer();
                    _inSynchronization = true;
                    try
                    {
                        if (User != null)
                        {
                            if (User.Role == UserRole.Driver)
                            {
                                await _localStorage.SynchronizeDriverContext();
                                Driver.Synchronize();
                            }
                            else if (User.Role == UserRole.Owner)
                            {
                                _localStorage.SynchronizeOwnerContext();
                                Owner.Synchronize();
                            }
                            else if (User.Role == UserRole.Dispatch)
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
            }).LogExceptions("TrukmanContext Synchronize");
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

        public static User User { get; private set; }

        public static Company Company { get; private set; }

        public static bool Initialized { get; private set; }
    }
    #endregion
}
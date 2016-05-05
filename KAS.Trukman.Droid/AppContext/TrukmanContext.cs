using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
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
using Android.Support.V7.App;

namespace KAS.Trukman.Droid.AppContext
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
                        User = await _localStorage.BecomeAsync();
                        if (User != null)
                        {
                            Company = await _localStorage.SelectUserCompany();
                            InitializeContext();
                        }
                        _state = States.Completed;
                        ShowTopPageMessage.Send();
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

        public static async Task JoinDriver()
        {
            try
            {
                //             StopSynchronizeTimer();
                //               await _localStorage.JoinDriver();
                //                GetUserInfo();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            finally
            {
                StartSynchronizeTimer();
            }
        }

        public static void SendSystemMessage(string message)
        {
            if (!AppWorking)
            {
                try
                {
                    var soundUri = Android.Net.Uri.Parse("android.resource://" + Forms.Context.PackageName + "/" + Resource.Raw.notification_sound);
                    var wearableExtender = new NotificationCompat.WearableExtender()
                        .SetHintHideIcon(true);
                    var builder = new NotificationCompat.Builder(Forms.Context)
                        .SetSmallIcon(Resource.Drawable.icon)
                        .SetContentTitle(AppLanguages.CurrentLanguage.AppName)
                        .SetContentText(message)
                        .Extend(wearableExtender)
                        .SetSound(soundUri)
                        .SetAutoCancel(true);
                    var resultIntent = new Intent(Forms.Context, typeof(MainActivity));
                    resultIntent.SetFlags(ActivityFlags.NewTask);
                    var stackBuilder = Android.Support.V4.App.TaskStackBuilder.Create(Forms.Context);
                    stackBuilder.AddNextIntent(resultIntent);
                    var resultPendingIntent = stackBuilder.GetPendingIntent(0, (int)PendingIntentFlags.UpdateCurrent);
                    builder.SetContentIntent(resultPendingIntent);

                    var notificationManager = Android.Support.V4.App.NotificationManagerCompat.From(Forms.Context);
                    notificationManager.Notify(0, builder.Build());
                }
                // Analysis disable once EmptyGeneralCatchClause
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }
            else
                ShowToastMessage.Send(message);
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
            ITrip trip = null;
            await Task.Run(() =>
            {
                trip = _localStorage.SelectTripByIDExternal(tripID);
            });
            return trip;
        }

        public static async Task<Company> RegisterCompanyAsync(CompanyInfo companyInfo)
        {
            var company = await _localStorage.RegisterCompanyAsync(companyInfo);
            return company;
        }

        public static async Task<Company> RegisterDriverAsync(DriverInfo driverInfo)
        {
            var company = await _localStorage.RegisterDriverAsync(driverInfo);
            return company;
        }

        public static async Task<DriverState> GetDriverState()
        {
            var state = await _localStorage.GetDriverState();
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
            Task.Run(() => {
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
                                _localStorage.SynchronizeDriverContext();
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
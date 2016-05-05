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
using Android.Util;
using System.Timers;
using KAS.Trukman.Droid.Helpers;
using KAS.Trukman.Droid.AppContext;

namespace KAS.Trukman.Droid.Services
{
    #region TrukmanService
    [Service]
    public class TrukmanService : Service
    {
        #region Static members
        private static readonly string TAG = "TrukmanService";
        #endregion

        private Timer _serviceTimer = null;

        public TrukmanService()
        {
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            Log.Debug(TAG, "On start command.");

            TrukmanContext.Initialize();

            this.UpdateLocation();
            this.StartTimer();

            return StartCommandResult.Sticky;
        }

        [Obsolete("deprecated")]
        public override void OnStart(Intent intent, int startId)
        {
            Log.Debug(TAG, "On start.");

            base.OnStart(intent, startId);
        }

        public override void OnDestroy()
        {
            this.CreateAlarm();

            Log.Debug(TAG, "On destroy.");

            this.StopTimer();

            base.OnDestroy();
        }

        public override IBinder OnBind(Intent intent)
        {
            Log.Debug(TAG, "On start command.");

            var binder = new TrukmanServiceBinder(this);
            return binder;
        }

        private void CreateAlarm()
        {
            var alarmManager = ((AlarmManager)ApplicationContext.GetSystemService(Context.AlarmService));
            var serviceIntent = new Intent(this, typeof(TrukmanService));
            var locationIntent = PendingIntent.GetService(ApplicationContext, 10, serviceIntent, PendingIntentFlags.UpdateCurrent);
            // Workaround as on Android 4.4.2 START_STICKY has currently no
            // effect
            // -> restart service every 1 mins
            alarmManager.Set(AlarmType.Rtc, Java.Lang.JavaSystem.CurrentTimeMillis() + 1000 * 60, locationIntent);
        }

        private void StartTimer()
        {
            if (_serviceTimer == null)
            {
                _serviceTimer = new Timer { Interval = 5000 };
                _serviceTimer.Elapsed += (sender, args) =>
                {
                    _serviceTimer.Stop();
                    this.UpdateLocation();
                    _serviceTimer.Start();
                };
            }
            _serviceTimer.Start();
        }

        private void StopTimer()
        {
            if (_serviceTimer == null)
                _serviceTimer.Stop();
        }

        private void UpdateLocation()
        {
            LocationHelper.Update();
        }
    }
    #endregion
}
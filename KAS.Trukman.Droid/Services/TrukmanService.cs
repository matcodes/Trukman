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
using KAS.Trukman.AppContext;
using KAS.Trukman.Messages;
using Xamarin.Forms;
using Android.Support.V7.App;
using KAS.Trukman.Languages;

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

            ShowNotificationMessage.Subscribe(this, this.ShowNotification);

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
            ShowNotificationMessage.Unsubscribe(this);

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

        private void ShowNotification(ShowNotificationMessage message)
        {
            if (!TrukmanContext.AppWorking)
            {
                try
                {
                    var soundUri = Android.Net.Uri.Parse("android.resource://" + Forms.Context.PackageName + "/" + Resource.Raw.notification_sound);
                    var wearableExtender = new NotificationCompat.WearableExtender()
                        .SetHintHideIcon(true);
                    var builder = new NotificationCompat.Builder(this)
                        .SetSmallIcon(Resource.Drawable.icon)
                        .SetContentTitle(AppLanguages.CurrentLanguage.AppName)
                        .SetContentText(message.MessageText)
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
                ShowToastMessage.Send(message.MessageText);
        }

        private void StartTimer()
        {
            if (_serviceTimer == null)
            {
                _serviceTimer = new Timer { Interval = 5000 };
                _serviceTimer.Elapsed += (sender, args) =>
                {
					try
					{
						Console.WriteLine("Update location from service");
                    	this.UpdateLocation();
					}
					catch (Exception exception)
					{
						Console.WriteLine(exception);
					}

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
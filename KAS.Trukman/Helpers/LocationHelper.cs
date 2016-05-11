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
using System.Threading.Tasks;
using Plugin.Geolocator.Abstractions;
using Plugin.Geolocator;
using Android.Util;
using KAS.Trukman.Messages;

namespace KAS.Trukman.Helpers
{
    #region LocationHelper
    public class LocationHelper
    {
        #region States
        internal enum States
        {
            Wait,
            Working
        }
        #endregion

        private static readonly string TAG = "LocationHelper";

        private static readonly int UPDATE_LOCATION_TIME = 15;
        private static readonly int UPDATE_LOCATION_TIMEOUT = 10;
        private static readonly int LOCATION_DESIRED_ACCURACY = 5;

        private static readonly IGeolocator _locator;

        private static DateTime _lastTime = DateTime.MinValue;

        static LocationHelper()
        {
			IsSelfPermission = false;
			IsSelfIntent = true;

            _locator = CrossGeolocator.Current;
            _locator.DesiredAccuracy = LOCATION_DESIRED_ACCURACY;

            _lastTime = DateTime.Now.AddMinutes(-1);

            State = States.Wait;
        }

        public static void Update()
        {
            Task.Run(async () => {
				if ((State == States.Wait) && (CheckTime()) && (IsSelfPermission) && (IsSelfIntent))
					await GetLocationAsync();
            });
        }

        private static bool CheckTime()
        {
            var timeSpan = DateTime.Now - _lastTime;
            return (timeSpan.TotalSeconds > UPDATE_LOCATION_TIME);
        }

        private static async Task GetLocationAsync()
        {
            State = States.Working;
            var logMessage = "";
            try
            {
                var now = DateTime.Now;
                var position = await _locator.GetPositionAsync(UPDATE_LOCATION_TIMEOUT * 1000);
                GeoLocationChangedMessage.Send(position.Latitude, position.Longitude);
                _lastTime = now;
                logMessage = String.Format("Current Position: ({0}, {1})", position.Latitude, position.Longitude);
            }
            catch (Exception exception)
            {
                logMessage = exception.Message;
            }
            finally
            {
                State = States.Wait;
            }
            Log.Debug(TAG, logMessage);
        }

        private static States State { get; set; }

		public static bool IsSelfPermission { get; set; }

		public static bool IsSelfIntent { get; set; }
    }
    #endregion
}
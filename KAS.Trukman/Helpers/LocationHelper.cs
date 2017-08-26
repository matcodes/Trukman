using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using Plugin.Geolocator.Abstractions;
using Plugin.Geolocator;
using KAS.Trukman.Messages;
using KAS.Trukman.Extensions;

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
            _locator.AllowsBackgroundUpdates = true;

            _lastTime = DateTime.Now.AddMinutes(-1);

            State = States.Wait;
        }

        public static void Initialize()
        {
            _locator.PositionChanged += (sender, args) =>
            {
                Update(args.Position);
            };
            Update();
        }

        public static void Update()
        {
            Task.Run(async () =>
            {
                if ((State == States.Wait) && (CheckTime()) && (IsSelfPermission) && (IsSelfIntent))
                    await GetLocationAsync();
            }).LogExceptions("LocationHelper Update");
        }

        public static void Update(Position position)
        {
            Task.Run(() =>
            {
                if ((State == States.Wait) && (CheckTime()) && (IsSelfPermission) && (IsSelfIntent))
                {
                    State = States.Working;
                    try
                    {
                        var now = DateTime.Now;
                        GeoLocationChangedMessage.Send(position.Latitude, position.Longitude);
                        _lastTime = now;
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception.Message);
                    }
                    finally
                    {
                        State = States.Wait;
                    }
                }
            }).LogExceptions("LocationHelper Update");
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
            Console.WriteLine(logMessage);
        }

        private static States State { get; set; }

        public static bool IsSelfPermission { get; set; }

        public static bool IsSelfIntent { get; set; }
    }
    #endregion
}
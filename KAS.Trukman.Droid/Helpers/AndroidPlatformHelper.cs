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
using KAS.Trukman.Helpers;
using Android.Graphics;
using Android.Locations;

namespace KAS.Trukman.Droid
{
    #region AndroidPlatformHelper
    public class AndroidPlatformHelper : IPlatformHelper
    {
        private Activity _activity = null;

        private float _ratio = 1.0f;

        private LocationManager _locationManager = null;

        public AndroidPlatformHelper(Activity activity)
        {
            _activity = activity;

            _locationManager = (LocationManager)_activity.GetSystemService(Context.LocationService);

            Display display = activity.WindowManager.DefaultDisplay;
            Point size = new Point();
            display.GetSize(size);

            _ratio = size.X / 320.0f;

            this.DisplayWidth = (int)(size.X / _ratio);
            this.DisplayHeight = (int)(size.Y / _ratio);
            this.ActionBarHeight = (int)(this.DisplayWidth / 6);
        }

        public bool CheckGPS()
        {
            var result = _locationManager.IsProviderEnabled(LocationManager.GpsProvider);
            return result;
        }

        #region IPlatformHelper
        #region Sizes
        public int ActionBarHeight { get; private set; }

        public int DisplayHeight { get; private set; }

        public int DisplayWidth { get; private set; }
        #endregion

        #region Font sizes
        public double TitleBarFontSize
        {
            get { return Xamarin.Forms.Device.GetNamedSize(Xamarin.Forms.NamedSize.Large, typeof(Xamarin.Forms.Label)); }
        }
        #endregion

        #region Colors
        public Xamarin.Forms.Color EntryPlaceholderColor
        {
            get { return Xamarin.Forms.Color.Gray; }
        }

        public Xamarin.Forms.Color TitleTextColor
        {
            get { return Xamarin.Forms.Color.White; }
        }

        public Xamarin.Forms.Color MainMenuEnabledColor
        {
            get { return Xamarin.Forms.Color.White; }
        }

        public Xamarin.Forms.Color MainMenuDisabledColor
        {
            get { return Xamarin.Forms.Color.Gray; }
        }

        public Xamarin.Forms.Color HomeTextColor
        {
            get { return Xamarin.Forms.Color.White; }
        }

        public Xamarin.Forms.Color HomeItemColor
        {
            get { return Xamarin.Forms.Color.Gray; }
        }

        public Xamarin.Forms.Color HomeSelectedItemColor
        {
            get { return Xamarin.Forms.Color.White; }
        }

        public Xamarin.Forms.Color HomeTimeColor
        {
            get { return Xamarin.Forms.Color.White; }
        }

        public Xamarin.Forms.Color HomeTimeOverColor
        {
            get { return Xamarin.Forms.Color.Red; }
        }

        public Xamarin.Forms.Color TripItemColor
        {
            get { return Xamarin.Forms.Color.Gray; }
        }

        public Xamarin.Forms.Color TripSelectedItemColor
        {
            get { return Xamarin.Forms.Color.White; }
        }

        public Xamarin.Forms.Color ContractorTextColor
        {
            get { return Xamarin.Forms.Color.White; }
        }

        public Xamarin.Forms.Color FuelAdvanceTextColor
        {
            get { return Xamarin.Forms.Color.White; }
        }

        public Xamarin.Forms.Color DelayEmergencyTextColor
        {
            get { return Xamarin.Forms.Color.White; }
        }

        public Xamarin.Forms.Color DelayEmergencyItemColor
        {
            get { return Xamarin.Forms.Color.Gray; }
        }

        public Xamarin.Forms.Color DelayEmergencySelectedItemColor
        {
            get { return Xamarin.Forms.Color.White; }
        }

        public Xamarin.Forms.Color RouteTextColor
        {
            get { return Xamarin.Forms.Color.White; }
        }

        public Xamarin.Forms.Color DriverAuthorizationTextColor
        {
            get { return Xamarin.Forms.Color.White; }
        }
        #endregion

        #region Image sources
        public string BackgroundImageSource
        {
            get { return "background"; }
        }

        public string BackgroundMenuImageSource
        {
            get { return "background_menu"; }
        }

        public string MenuImageSource
        {
            get { return "menu"; }
        }

        public string HomeImageSource
        {
            get { return "home"; }
        }

        public string MailImageSource
        {
            get { return "mail"; }
        }

        public string MapImageSource
        {
            get { return "map"; }
        }

        public string LocationImageSource
        {
            get { return "location"; }
        }

        public string CameraImageSource
        {
            get { return "camera"; }
        }

        public string LeftImageSource
        {
            get { return "left"; }
        }

        public string RightImageSource
        {
            get { return "right"; }
        }

        public string HomeMapImageSource
        {
            get { return "home_map"; }
        }

        public string FuelImageSource
        {
            get { return "fuel"; }
        }

        public string FuelRequestedImageSource
        {
            get { return "fuel_requested"; }
        }

        public string FuelReceivedImageSource
        {
            get { return "fuel_received"; }
        }

        public string LumperImageSource
        {
            get { return "lumper"; }
        }

        public string LumperRequestedImageSource
        {
            get { return "lumper_requested"; }
        }

        public string LumperReceivedImageSource
        {
            get { return "lumper_received"; }
        }

        public string TimeImageSource
        {
            get { return "time"; }
        }

        public string LikeImageSource
        {
            get { return "like"; }
        }

        public string DislikeImageSource
        {
            get { return "dislike"; }
        }

        public string ClockImageSource
        {
            get { return "clock"; }
        }

        public string GpsOnImageSource
        {
            get { return "gps_on"; }
        }

        public string GpsOffImageSource
        {
            get { return "gps_off"; }
        }

        public string GpsOffWarningImageSource
        {
            get { return "gps_off_warning"; }
        }

        public string TurnLeftImageSource
        {
            get { return "turn_left"; }
        }

        public string TurnRightImageSource
        {
            get { return "turn_right"; }
        }

        public string TurnNoneImageSource
        {
            get { return "turn_none"; }
        }

        public string LockImageSource
        {
            get { return "lock"; }
        }
        #endregion
        #endregion
    }
    #endregion
}
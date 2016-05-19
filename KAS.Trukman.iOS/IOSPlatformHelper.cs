using System;
using KAS.Trukman.Helpers;
using UIKit;
using CoreLocation;

namespace KAS.Trukman.iOS
{
	#region IOSPlatformHelpert
	public class IOSPlatformHelper : IPlatformHelper
	{
		private nfloat _ratio = (nfloat)1.0;

		public IOSPlatformHelper ()
		{
			var bounds = UIScreen.MainScreen.Bounds;

			_ratio = bounds.Width / (nfloat)320.0;

			this.DisplayWidth = (int)(bounds.Width / _ratio);
			this.DisplayHeight = (int)(bounds.Height / _ratio);
			this.ActionBarHeight = (int)(this.DisplayWidth / 6);
		}
	
		public bool CheckGPS()
		{
			return CLLocationManager.LocationServicesEnabled;
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
		public Xamarin.Forms.Color SignUpTextColor
		{
			get { return Xamarin.Forms.Color.White; }
		}

		public Xamarin.Forms.Color SignUpSelectedItemColor
		{
			get { return Xamarin.Forms.Color.White; }
		}

		public Xamarin.Forms.Color SignUpItemColor
		{
			get { return Xamarin.Forms.Color.Gray; }
		}

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

        public Xamarin.Forms.Color RegularTextColor
        {
            get { return Xamarin.Forms.Color.FromHex("FF8F8E"); }
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

		public string LockNormalImageSource
		{
			get { return "lock_normal"; }
		}

		public string LockRedImageSource
		{
			get { return "lock_red"; }
		}

		public string UnlockImageSource
		{
			get { return "unlock"; }
		}

		public string LogoImageSource
		{
			get { return "logo"; }
		}
		#endregion
		#endregion
	}
	#endregion
}


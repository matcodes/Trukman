using System;
using Android.App;
using Android.Views;
using Android.Graphics;

namespace Trukman.Droid
{
	#region AndroidPlatformHelper
	public class AndroidPlatformHelper : IPlatformHelper
	{
		private Activity _activity = null;

		private float _ratio = 1.0f;

		public AndroidPlatformHelper(Activity activity)
		{
			_activity = activity;

			Display display = activity.WindowManager.DefaultDisplay;
			Point size = new Point();
			display.GetSize(size);

			_ratio = size.X / 320.0f;

			this.DisplayWidth = (int)(size.X / _ratio);
			this.DisplayHeight = (int)(size.Y / _ratio);
			this.ActionBarHeight = (int)(this.DisplayWidth / 6);

			this.TitleBarFontSize = 30;
		}

		#region IPlatformHelper
		#region Sizes
		public int ActionBarHeight { get; private set; }

		public int DisplayHeight { get; private set; }

		public int DisplayWidth { get; private set; }
		#endregion

		#region Font sizes
		public double TitleBarFontSize { get; private set; }
		#endregion

		#region Colors
		public Xamarin.Forms.Color TitleTextColor
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
		#endregion

		#region Image sources
		public string MenuImageSource
		{
			get { return "menu"; }
		}

		public string HomeImageSource
		{
			get { return "home"; }
		}

		public string LeftImageSource
		{
			get { return "left"; }
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

		public string MessageImageSource 
		{
			get { return "message"; }
		}

		public string TripImageSource 
		{
			get { return "map"; }
		}

		public string AdvancesImageSource 
		{
			get { return "advances"; }
		}

		public string DelayImageSource 
		{
			get { return "clock"; }
		}

		public string ClockImageSource
		{
			get { return "clock"; }
		}

		public string CameraImageSource 
		{
			get { return "photo"; }
		}

		public string ArrowImageSource
		{
			get { return "arrow"; }
		}
		#endregion
		#endregion
	}
	#endregion
}
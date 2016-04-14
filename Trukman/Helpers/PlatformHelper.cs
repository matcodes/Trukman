using System;
using Xamarin.Forms;

namespace Trukman
{
	#region IPlatformHelper
	public interface IPlatformHelper
	{
		#region Sizes
		int DisplayWidth { get; }
		int DisplayHeight { get; }
		int ActionBarHeight { get; }
		#endregion

		#region Font sizes
		double TitleBarFontSize { get; }
		#endregion

		#region Colors
		Color TitleTextColor { get; }
		Color ContractorTextColor { get; }
		Color FuelAdvanceTextColor { get; }
		#endregion

		#region Image sources
		string MenuImageSource { get; }
		string HomeImageSource { get; }
		string LeftImageSource { get; }
		string FuelImageSource { get; }
		string FuelRequestedImageSource { get; }
		string FuelReceivedImageSource { get; }
		string MessageImageSource { get; }
		string TripImageSource { get; }
		string AdvancesImageSource { get; }
		string DelayImageSource { get; }
		string ClockImageSource { get; }
		string CameraImageSource { get; }
		string ArrowImageSource { get; }
		#endregion
	}
	#endregion

	#region PlatformHelper
	public static class PlatformHelper
	{
		private static IPlatformHelper _platformHelper = null;

		public static void Initialize(IPlatformHelper platformHelper)
		{
			_platformHelper = platformHelper;
		}

		#region Sizes
		public static int DisplayWidth
		{
			get { return (_platformHelper != null ? _platformHelper.DisplayWidth : 0); }
		}

		public static int DisplayHeight
		{
			get { return (_platformHelper != null ? _platformHelper.DisplayHeight : 0); }
		}

		public static int ActionBarHeight
		{
			get { return (_platformHelper != null ? _platformHelper.ActionBarHeight : 0); }
		}
		#endregion

		#region Font sizes
		public static double TitleBarFontSize
		{
			get { return (_platformHelper != null ? _platformHelper.TitleBarFontSize : Device.GetNamedSize(NamedSize.Large, typeof(Label))); }
		}
		#endregion

		#region Colors
		public static Color TitleTextColor
		{
			get { return (_platformHelper != null ? _platformHelper.TitleTextColor : Color.White); }
		}

		public static Color ContractorTextColor
		{
			get { return (_platformHelper != null ? _platformHelper.ContractorTextColor : Color.White); }
		}

		public static Color FuelAdvanceTextColor
		{
			get { return (_platformHelper != null ? _platformHelper.FuelAdvanceTextColor : Color.White); }
		}
		#endregion

		#region Image sources
		public static string MenuImageSource
		{
			get { return (_platformHelper != null ? _platformHelper.MenuImageSource : "menu"); }
		}

		public static string HomeImageSource
		{
			get { return (_platformHelper != null ? _platformHelper.HomeImageSource : "home"); }
		}

		public static string LeftImageSource
		{
			get { return (_platformHelper != null ? _platformHelper.LeftImageSource : "left"); }
		}

		public static string FuelImageSource
		{
			get { return (_platformHelper != null ? _platformHelper.FuelImageSource : "fuel"); }
		}

		public static string FuelRequestedImageSource
		{
			get { return (_platformHelper != null ? _platformHelper.FuelRequestedImageSource : "fuel_requested"); }
		}

		public static string FuelReceivedImageSource
		{
			get { return (_platformHelper != null ? _platformHelper.FuelReceivedImageSource : "fuel_received"); }
		}

		public static string MessageImageSource 
		{ 
			get { return (_platformHelper != null ? _platformHelper.MessageImageSource : "message"); }
		}

		public static string TripImageSource 
		{ 
			get { return (_platformHelper != null ? _platformHelper.TripImageSource : "trip"); }
		}

		public static string AdvancesImageSource 
		{
			get {return (_platformHelper != null ? _platformHelper.AdvancesImageSource : "advances");}
		}

		public static string DelayImageSource 
		{ 
			get { return (_platformHelper != null ? _platformHelper.DelayImageSource : "delay"); }
		}

		public static string ClockImageSource
		{
			get { return (_platformHelper != null ? _platformHelper.ClockImageSource : "clock"); }
		}

		public static string CameraImageSource 
		{ 
			get { return (_platformHelper != null ? _platformHelper.CameraImageSource : "camera"); }
		}

		public static string ArrowImageSource
		{
			get { return (_platformHelper != null ? _platformHelper.ArrowImageSource : "arrow"); }
		}
		#endregion
	}
	#endregion
}

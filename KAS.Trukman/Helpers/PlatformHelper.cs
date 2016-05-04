using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Helpers
{
    #region IPlatformHelper
    public interface IPlatformHelper
    {
        bool CheckGPS();

        #region Sizes
        int DisplayWidth { get; }

        int DisplayHeight { get; }

        int ActionBarHeight { get; }
        #endregion

        #region Font sizes
        double TitleBarFontSize { get; }
        #endregion

        #region Colors
        Color SignUpTextColor { get; }

        Color SignUpSelectedItemColor { get; }

        Color SignUpItemColor { get; }

        Color EntryPlaceholderColor { get; }

        Color TitleTextColor { get; }

        Color MainMenuEnabledColor { get; }

        Color MainMenuDisabledColor { get; }

        Color HomeTextColor { get; }

        Color HomeItemColor { get; }

        Color HomeSelectedItemColor { get; }

        Color HomeTimeColor { get; }

        Color HomeTimeOverColor { get; }

        Color TripItemColor { get; }

        Color TripSelectedItemColor { get; }

        Color ContractorTextColor { get; }

        Color FuelAdvanceTextColor { get; }

        Color DelayEmergencyTextColor { get; }

        Color DelayEmergencyItemColor { get; }

        Color DelayEmergencySelectedItemColor { get; }

        Color RouteTextColor { get; }

        Color DriverAuthorizationTextColor { get; }
        #endregion

        #region Image sources
        string BackgroundImageSource { get; }

        string BackgroundMenuImageSource { get; }

        string MenuImageSource { get; }

        string HomeImageSource { get; }

        string MailImageSource { get; }

        string MapImageSource { get; }

        string LocationImageSource { get; }

        string CameraImageSource { get; }

        string LeftImageSource { get; }

        string RightImageSource { get; }

        string HomeMapImageSource { get; }

        string FuelImageSource { get; }

        string FuelRequestedImageSource { get; }

        string FuelReceivedImageSource { get; }

        string LumperImageSource { get; }

        string LumperRequestedImageSource { get; }

        string LumperReceivedImageSource { get; }

        string TimeImageSource { get; }

        string LikeImageSource { get; }

        string DislikeImageSource { get; }

        string ClockImageSource { get; }

        string GpsOnImageSource { get; }

        string GpsOffImageSource { get; }

        string GpsOffWarningImageSource { get; }

        string TurnLeftImageSource { get; }

        string TurnRightImageSource { get; }

        string TurnNoneImageSource { get; }

        string LockNormalImageSource { get; }

        string LockRedImageSource { get; }

        string UnlockImageSource { get; }

        string LogoImageSource { get; }
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

        public static bool CheckGPS()
        {
            var result = false;
            if (_platformHelper != null)
                result = _platformHelper.CheckGPS();
            return result;
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
        public static Color SignUpTextColor
        {
            get { return (_platformHelper != null ? _platformHelper.SignUpTextColor : Color.White); }
        }

        public static Color SignUpSelectedItemColor
        {
            get { return (_platformHelper != null ? _platformHelper.SignUpSelectedItemColor : Color.White); }
        }

        public static Color SignUpItemColor
        {
            get { return (_platformHelper != null ? _platformHelper.SignUpItemColor : Color.Gray); }
        }

        public static Color EntryPlaceholderColor
        {
            get { return (_platformHelper != null ? _platformHelper.EntryPlaceholderColor : Color.Gray); }
        }

        public static Color TitleTextColor
        {
            get { return (_platformHelper != null ? _platformHelper.TitleTextColor : Color.White); }
        }

        public static Color MainMenuEnabledColor
        {
            get { return (_platformHelper != null ? _platformHelper.MainMenuEnabledColor : Color.White); }
        }

        public static Color MainMenuDisabledColor
        {
            get { return (_platformHelper != null ? _platformHelper.MainMenuDisabledColor : Color.Gray); }
        }

        public static Color HomeTextColor
        {
            get { return (_platformHelper != null ? _platformHelper.HomeTextColor : Color.White); }
        }

        public static Color HomeItemColor
        {
            get { return (_platformHelper != null ? _platformHelper.HomeItemColor : Color.Gray); }
        }

        public static Color HomeSelectedItemColor
        {
            get { return (_platformHelper != null ? _platformHelper.HomeSelectedItemColor : Color.White); }
        }

        public static Color HomeTimeColor
        {
            get { return (_platformHelper != null ? _platformHelper.HomeTimeColor : Color.White); }
        }

        public static Color HomeTimeOverColor
        {
            get { return (_platformHelper != null ? _platformHelper.HomeTimeOverColor : Color.Red); }
        }

        public static Color TripItemColor
        {
            get { return (_platformHelper != null ? _platformHelper.TripItemColor : Color.Gray); }
        }

        public static Color TripSelectedItemColor
        {
            get { return (_platformHelper != null ? _platformHelper.TripSelectedItemColor : Color.White); }
        }

        public static Color ContractorTextColor
        {
            get { return (_platformHelper != null ? _platformHelper.ContractorTextColor : Color.White); }
        }

        public static Color FuelAdvanceTextColor
        {
            get { return (_platformHelper != null ? _platformHelper.FuelAdvanceTextColor : Color.White); }
        }

        public static Color DelayEmergencyTextColor
        {
            get { return (_platformHelper != null ? _platformHelper.DelayEmergencyTextColor : Color.White); }
        }

        public static Color DelayEmergencyItemColor
        {
            get { return (_platformHelper != null ? _platformHelper.DelayEmergencyItemColor : Color.Gray); }
        }

        public static Color DelayEmergencySelectedItemColor
        {
            get { return (_platformHelper != null ? _platformHelper.DelayEmergencySelectedItemColor : Color.White); }
        }

        public static Color RouteTextColor
        {
            get { return (_platformHelper != null ? _platformHelper.RouteTextColor : Color.White); }
        }

        public static Color DriverAuthorizationTextColor
        {
            get { return (_platformHelper != null ? _platformHelper.DriverAuthorizationTextColor : Color.White); }
        }
        #endregion

        #region Image sources
        public static string BackgroundImageSource
        {
            get { return (_platformHelper != null ? _platformHelper.BackgroundImageSource : "background"); }
        }

        public static string BackgroundMenuImageSource
        {
            get { return (_platformHelper != null ? _platformHelper.BackgroundMenuImageSource : "background_menu"); }
        }

        public static string MenuImageSource
        {
            get { return (_platformHelper != null ? _platformHelper.MenuImageSource : "menu"); }
        }

        public static string HomeImageSource
        {
            get { return (_platformHelper != null ? _platformHelper.HomeImageSource : "home"); }
        }

        public static string MailImageSource
        {
            get { return (_platformHelper != null ? _platformHelper.MailImageSource : "mail"); }
        }

        public static string MapImageSource
        {
            get { return (_platformHelper != null ? _platformHelper.MapImageSource : "map"); }
        }

        public static string LocationImageSource
        {
            get { return (_platformHelper != null ? _platformHelper.LocationImageSource : "location"); }
        }

        public static string CameraImageSource
        {
            get { return (_platformHelper != null ? _platformHelper.CameraImageSource : "camera"); }
        }

        public static string LeftImageSource
        {
            get { return (_platformHelper != null ? _platformHelper.LeftImageSource : "left"); }
        }

        public static string RightImageSource
        {
            get { return (_platformHelper != null ? _platformHelper.RightImageSource : "right"); }
        }

        public static string HomeMapImageSource
        {
            get { return (_platformHelper != null ? _platformHelper.HomeMapImageSource : "home_map"); }
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

        public static string LumperImageSource
        {
            get { return (_platformHelper != null ? _platformHelper.LumperImageSource : "lumper"); }
        }

        public static string LumperRequestedImageSource
        {
            get { return (_platformHelper != null ? _platformHelper.LumperRequestedImageSource : "lumper_requested"); }
        }

        public static string LumperReceivedImageSource
        {
            get { return (_platformHelper != null ? _platformHelper.LumperReceivedImageSource : "lumper_received"); }
        }

        public static string TimeImageSource
        {
            get { return (_platformHelper != null ? _platformHelper.TimeImageSource : "time"); }
        }

        public static string LikeImageSource
        {
            get { return (_platformHelper != null ? _platformHelper.LikeImageSource : "like"); }
        }

        public static string DislikeImageSource
        {
            get { return (_platformHelper != null ? _platformHelper.DislikeImageSource : "dislike"); }
        }

        public static string ClockImageSource
        {
            get { return (_platformHelper != null ? _platformHelper.ClockImageSource : "clock"); }
        }

        public static string GpsOnImageSource
        {
            get { return (_platformHelper != null ? _platformHelper.GpsOnImageSource : "gps_on"); }
        }

        public static string GpsOffImageSource
        {
            get { return (_platformHelper != null ? _platformHelper.GpsOffImageSource : "gps_off"); }
        }

        public static string GpsOffWarningImageSource
        {
            get { return (_platformHelper != null ? _platformHelper.GpsOffWarningImageSource : "gps_off_warning"); }
        }

        public static string TurnLeftImageSource
        {
            get { return (_platformHelper != null ? _platformHelper.TurnLeftImageSource : "turn_left"); }
        }

        public static string TurnRightImageSource
        {
            get { return (_platformHelper != null ? _platformHelper.TurnRightImageSource : "turn_right"); }
        }

        public static string TurnNoneImageSource
        {
            get { return (_platformHelper != null ? _platformHelper.TurnNoneImageSource : "turn_none"); }
        }

        public static string LockNormalImageSource
        {
            get { return (_platformHelper != null ? _platformHelper.LockNormalImageSource : "lock_normal"); }
        }

        public static string LockRedImageSource
        {
            get { return (_platformHelper != null ? _platformHelper.LockRedImageSource : "lock_red"); }
        }

        public static string UnlockImageSource
        {
            get { return (_platformHelper != null ? _platformHelper.UnlockImageSource : "unlock"); }
        }

        public static string LogoImageSource
        {
            get { return (_platformHelper != null ? _platformHelper.LogoImageSource : "logo"); }
        }
        #endregion
    }
    #endregion
}

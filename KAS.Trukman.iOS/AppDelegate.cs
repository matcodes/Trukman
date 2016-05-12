﻿using Foundation;
using UIKit;
using KAS.Trukman.Helpers;
using KAS.Trukman.AppContext;
using CoreLocation;
using HockeyApp;


namespace KAS.Trukman.iOS
{
	#region AppDelegate
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
		private CLLocationManager _locationManager = null;

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
			var hockeyManager = BITHockeyManager.SharedHockeyManager;
			hockeyManager.Configure ("d30cee35c8b5469d8987e7d557b150f8");
			hockeyManager.StartManager ();

			PlatformHelper.Initialize (new IOSPlatformHelper ());

			TrukmanContext.Initialize ();

            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new Trukman.App());

			app.StatusBarHidden = true;
			_locationManager = new CLLocationManager ();
			_locationManager.RequestWhenInUseAuthorization();

            LocationHelper.IsSelfPermission = true;
            LocationHelper.Initialize ();
            
            TrukmanContext.Initialize ();

            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new Trukman.App());

            return base.FinishedLaunching(app, options);
        }

		public override bool OpenUrl (UIApplication app, NSUrl url, NSDictionary options) {
			if (url.IsFileUrl == true) {
				RateConfirmationViewController.handleFileURL (url);
			}

			return true;
		}

		public override bool HandleOpenURL(UIApplication application, NSUrl url) {
			if (url.IsFileUrl == true) {
				RateConfirmationViewController.handleFileURL (url);
			}
			return true;
		}
    }
	#endregion
}
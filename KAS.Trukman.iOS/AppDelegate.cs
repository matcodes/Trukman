using Foundation;
using UIKit;
using KAS.Trukman.Helpers;
using KAS.Trukman.AppContext;
using CoreLocation;

namespace KAS.Trukman.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.


    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
		public CLLocationManager locationManager = new CLLocationManager();

        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
			PlatformHelper.Initialize (new IOSPlatformHelper ());

			TrukmanContext.Initialize ();

            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new Trukman.App());

			app.StatusBarHidden = true;
			locationManager.RequestWhenInUseAuthorization();

            LocationHelper.IsSelfPermission = true;
            LocationHelper.Initialize ();
            
            TrukmanContext.Initialize ();

            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new Trukman.App());

            return base.FinishedLaunching(app, options);
        }

		public override bool OpenUrl (UIApplication app, NSUrl url, NSDictionary options) {
			if (url.IsFileUrl == true) {
				this.InvokeOnMainThread (() => {
					RateConfirmationViewController.handleFileURL (url);
				});
				return true;
			}

			return false;
		}

		public override bool HandleOpenURL(UIApplication application, NSUrl url) {
			if (url.IsFileUrl == true) {
				this.InvokeOnMainThread (() => {
					RateConfirmationViewController.handleFileURL (url);
				});
				return true;
			}
			return false;
		}
    }
}
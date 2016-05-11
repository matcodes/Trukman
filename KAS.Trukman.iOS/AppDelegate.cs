using Foundation;
using UIKit;
using KAS.Trukman.Helpers;
using KAS.Trukman.AppContext;
using CoreLocation;

namespace KAS.Trukman.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
		public CLLocationManager _locationManager = new CLLocationManager();

		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
			PlatformHelper.Initialize (new IOSPlatformHelper ());

			app.StatusBarHidden = true;
			_locationManager.RequestWhenInUseAuthorization();

			LocationHelper.IsSelfPermission = true;
			LocationHelper.Initialize ();
			
			TrukmanContext.Initialize ();

            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new Trukman.App());

            return base.FinishedLaunching(app, options);
        }
    }
}


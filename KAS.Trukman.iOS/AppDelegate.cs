using Foundation;
using UIKit;
using KAS.Trukman.Helpers;
using KAS.Trukman.AppContext;
using CoreLocation;
using HockeyApp;
using ToastIOS;
using KAS.Trukman.Messages;


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

			app.StatusBarHidden = true;
//			_locationManager = new CLLocationManager ();
//			_locationManager.RequestWhenInUseAuthorization();

            LocationHelper.IsSelfPermission = true;
            LocationHelper.Initialize ();
            
            TrukmanContext.Initialize ();

            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new Trukman.App());

            return base.FinishedLaunching(app, options);
        }

		public override void OnActivated (UIApplication uiApplication) {
			ShowToastMessage.Subscribe (this, this.ShowToast);
		}

		public override void DidEnterBackground (UIApplication uiApplication) {
			ShowToastMessage.Unsubscribe (this);
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

		private void ShowToast(ShowToastMessage message) 
		{
			this.InvokeOnMainThread (() => {
				Toast.MakeText(message.Text).Show();
			});
		}
    }
	#endregion
}
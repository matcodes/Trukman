using Foundation;
using UIKit;
using KAS.Trukman.Helpers;
using KAS.Trukman.AppContext;
using CoreLocation;
using HockeyApp;
using ToastIOS;
using KAS.Trukman.Messages;
using Parse;
using System.Threading.Tasks;
using System;

namespace KAS.Trukman.iOS
{
	#region AppDelegate
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
		private LocationHelperIOS _locationManager = null;
		private CameraHelper _cameraHelper = null;
		private PushHelperIOS _pushHelper = null;

		private bool launchPdfFromZero = false;

		public NSUrl fileUrl = null;

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
			var hockeyManager = BITHockeyManager.SharedHockeyManager;
			hockeyManager.Configure ("d30cee35c8b5469d8987e7d557b150f8");
			hockeyManager.StartManager ();

			TaskScheduler.UnobservedTaskException += (sender, args) => {
				Console.WriteLine(args.Exception);
			};

			_cameraHelper = new CameraHelper ();
			PlatformHelper.Initialize (new IOSPlatformHelper ());

			app.StatusBarHidden = true;

			this.InvokeOnMainThread (() => {
				_locationManager = new LocationHelperIOS ();
				_pushHelper = new PushHelperIOS();

				LocationHelper.IsSelfPermission = true;
				LocationHelper.Initialize ();
			});

			if (options != null) {
				launchPdfFromZero = true;
				MainPageInitializedMessage.Subscribe (this, this.ShowPdf);
			} else {
				launchPdfFromZero = false;
			}


            TrukmanContext.Initialize ();
			global::Xamarin.Forms.Forms.Init();
			LoadApplication(new Trukman.App());

            return base.FinishedLaunching(app, options);
        }
			
		public override void RegisteredForRemoteNotifications (
			UIApplication application, NSData deviceToken)
		{
			_pushHelper.DidGetDeviceToken (deviceToken);
		}

		public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo) {
			// We need this to fire userInfo into ParsePushNotificationReceived.
			ParsePush.HandlePush(userInfo);
		}
			

		public void ReceivedLocalNotification (UIApplication application, UILocalNotification notification) {
			if (application.ApplicationState == UIApplicationState.Active) {
				ShowToastMessage.Send (notification.AlertBody);
			}
		}

		public override void OnActivated (UIApplication uiApplication) {
			_pushHelper.SubscribeMessages ();

			TrukmanContext.AppWorking = true;

			ShowToastMessage.Subscribe (this, this.ShowToast);
			TakePhotoFromCameraMessage.Subscribe(_cameraHelper, _cameraHelper.TakePhotoFromCamera);
			ShowGPSSettingsMessage.Subscribe (this, this.ShowGPSSettings);

			this.InvokeOnMainThread(() => {
				if (CLLocationManager.LocationServicesEnabled == false) {
					UIAlertView alertView = new UIAlertView ("Error", "GPS tracking is disabled in your device settings. Please, Turn on your GPS for correct working.", null, "OK", null);
					alertView.Show ();
				}
			});


			if (launchPdfFromZero == false && fileUrl != null) {
				ShowPdf (null);
			}
		}

		public override void DidEnterBackground (UIApplication uiApplication) {
			TrukmanContext.AppWorking = false;

			ShowToastMessage.Unsubscribe (this);
			TakePhotoFromCameraMessage.Unsubscribe(_cameraHelper);
			ShowGPSSettingsMessage.Unsubscribe (this);

			_pushHelper.UnsubscribeMessages ();
		}

		public override bool OpenUrl (UIApplication app, NSUrl url, NSDictionary options) {
			if (url.IsFileUrl == true) {
				fileUrl = url;
				return true;
			}

			return false;
		}

		public override bool HandleOpenURL(UIApplication application, NSUrl url) {
			if (url.IsFileUrl == true) {
				fileUrl = url;
				return true;
			}
			return false;
		}

		private void ShowPdf(MainPageInitializedMessage message)
		{
			this.InvokeOnMainThread (() => {
				RateConfirmationViewController.handleFileURL (fileUrl);
				MainPageInitializedMessage.Unsubscribe(this);
				fileUrl = null;
				launchPdfFromZero = false;
			});
		}

		private void ShowToast(ShowToastMessage message) 
		{
			this.InvokeOnMainThread (() => {
				Toast.MakeText(message.Text).Show();
			});
		}

		private void ShowGPSSettings(ShowGPSSettingsMessage message)
		{
			UIApplication.SharedApplication.OpenUrl (new NSUrl (UIApplication.OpenSettingsUrlString));
		}
    }
	#endregion
}
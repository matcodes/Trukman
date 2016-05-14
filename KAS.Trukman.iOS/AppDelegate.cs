﻿using Foundation;
using UIKit;
using KAS.Trukman.Helpers;
using KAS.Trukman.AppContext;
using CoreLocation;
using HockeyApp;
using ToastIOS;
using KAS.Trukman.Messages;
using Parse;

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


		public override void OnActivated (UIApplication uiApplication) {
			ShowToastMessage.Subscribe (this, this.ShowToast);
			TakePhotoFromCameraMessage.Subscribe(_cameraHelper, _cameraHelper.TakePhotoFromCamera);
			ShowSignUpOwnerWelcomePageMessage.Subscribe(_pushHelper, _pushHelper.Register);

			if (launchPdfFromZero == false && fileUrl != null) {
				ShowPdf (null);
			}
		}

		public override void DidEnterBackground (UIApplication uiApplication) {
			ShowToastMessage.Unsubscribe (this);
			TakePhotoFromCameraMessage.Unsubscribe(_cameraHelper);
			ShowSignUpOwnerWelcomePageMessage.Unsubscribe(_pushHelper);
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
    }
	#endregion
}
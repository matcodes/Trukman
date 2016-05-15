using System;
using KAS.Trukman.Messages;
using UIKit;
using Foundation;
using System.Threading;

namespace KAS.Trukman.iOS
{
	public class CameraHelper:NSObject, UIImagePickerControllerDelegate
	{
		static UIImagePickerController picker;

		public bool presented = false;

		public CameraHelper () {
			CameraHelper.Initialize ();
			picker.WeakDelegate = this;
		}

		[Export("imagePickerController:didFinishPickingMediaWithInfo:")]
		public override void FinishedPickingMedia (UIImagePickerController picker, NSDictionary info)
		{
			presented = false;
			var photo = info.ValueForKey(new NSString("UIImagePickerControllerOriginalImage")) as UIImage;
			NSData imageData = photo.AsJPEG((nfloat)0.5);
			Byte[] byteArray = new Byte[imageData.Length];
			System.Runtime.InteropServices.Marshal.Copy(imageData.Bytes, byteArray, 0, Convert.ToInt32(imageData.Length));

			SendPhotoMessage.Send(byteArray);

			picker.DismissViewController (true, null);
		}

		static void Initialize ()
		{
			if (picker != null)
				return;

			picker = new UIImagePickerController ();
		}

		public void TakePhotoFromCamera(TakePhotoFromCameraMessage message) {
			this.InvokeOnMainThread (() => {
				if (presented == true) {
					return;
				}

				presented = true;
				picker.SourceType = UIImagePickerControllerSourceType.Camera;

				var window = UIApplication.SharedApplication.KeyWindow;
				var vc = window.RootViewController;
				while (vc.PresentedViewController != null)
				{
					vc = vc.PresentedViewController;
				}

				UIImagePickerController presentedPicker = (UIImagePickerController)vc;
				if (presentedPicker == null) {
					vc.PresentModalViewController (picker, true);
				} 
			});
		}
	}
}


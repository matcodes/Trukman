using System;
using KAS.Trukman.Messages;
using UIKit;
using Foundation;
using System.Threading;

namespace KAS.Trukman.iOS
{
	public class CameraHelper:NSObject
	{
		static UIImagePickerController picker;

		class CameraDelegate : UIImagePickerControllerDelegate {
			public override void FinishedPickingMedia (UIImagePickerController picker, NSDictionary info)
			{
				var photo = info.ValueForKey(new NSString("UIImagePickerControllerOriginalImage")) as UIImage;
				NSData imageData = photo.AsJPEG((nfloat)0.5);
				Byte[] byteArray = new Byte[imageData.Length];
				System.Runtime.InteropServices.Marshal.Copy(imageData.Bytes, byteArray, 0, Convert.ToInt32(imageData.Length));

				SendPhotoMessage.Send(byteArray);

				picker.DismissViewController (true, null);
			}
		}

		public CameraHelper () {
			CameraHelper.Init ();
		}

		static void Init ()
		{
			if (picker != null)
				return;

			picker = new UIImagePickerController ();
			picker.Delegate = new CameraDelegate ();
		}

		public void TakePhotoFromCamera(TakePhotoFromCameraMessage message) {
			this.InvokeOnMainThread (() => {
				picker.SourceType = UIImagePickerControllerSourceType.Camera;

				var window = UIApplication.SharedApplication.KeyWindow;
				var vc = window.RootViewController;
				while (vc.PresentedViewController != null)
				{
					vc = vc.PresentedViewController;
				}

				vc.PresentModalViewController (picker, true);
			});
		}
	}
}


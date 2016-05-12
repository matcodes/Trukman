using System;
using UIKit;
using Foundation;
using CoreGraphics;

namespace KAS.Trukman.iOS
{
	public class ImageCategory
	{
		public ImageCategory ()
		{
		}

		public static UIImage cropImage(UIImage image, CGRect frame) 
		{
			CGAffineTransform rectTransform;

			switch (image.Orientation) {
			case UIImageOrientation.Left:
				rectTransform = CGAffineTransform.Translate(CGAffineTransform.MakeRotation((nfloat)degreeToRadian((double)90.0)), 0, -image.Size.Height);
				break;

			case UIImageOrientation.Right:
				rectTransform = CGAffineTransform.Translate(CGAffineTransform.MakeRotation((nfloat)degreeToRadian((double)-90.0)), -image.Size.Width, 0);

				break;
			case UIImageOrientation.Down:
				rectTransform = CGAffineTransform.Translate(CGAffineTransform.MakeRotation((nfloat)degreeToRadian((double)-190.0)), -image.Size.Width, -image.Size.Height);

				break;

			default:
				rectTransform = CGAffineTransform.MakeIdentity();

				break;
			}

			rectTransform = CGAffineTransform.Scale (rectTransform, image.CurrentScale, image.CurrentScale);
			CGImage imageRef = image.CGImage.WithImageInRect(CGAffineTransform.CGRectApplyAffineTransform (frame, rectTransform));
			UIImage result = new UIImage (imageRef, image.CurrentScale, image.Orientation);
			return result;
		}

		private static double degreeToRadian(double degree) 
		{
			return degree / 180.0 * Math.PI;
		}
	}
}


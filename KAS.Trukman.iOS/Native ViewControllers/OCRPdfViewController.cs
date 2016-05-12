using System;

using UIKit;
using Foundation;
using KAS.SPResizableView;
using CoreGraphics;

namespace KAS.Trukman.iOS
{
	public partial class OCRPdfViewController : UIViewController
	{
		public NSUrl pdfUrl;
		public SPUserResizableView highlightBox;

		public OCRPdfViewController () : base ("OCRPdfViewController", null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
			NSUrlRequest request = new NSUrlRequest(pdfUrl);
			webView.LoadRequest (request);
			webView.ScalesPageToFit = true;
			scanButton.Enabled = false;

			CGRect frame = new CGRect (100, 100, 200, 100); 
			highlightBox = new SPUserResizableView (frame);

			UIView contentView = new UIView (highlightBox.Bounds);
			contentView.BackgroundColor = UIColor.Blue.ColorWithAlpha (0.2f);
			highlightBox.ContentView = contentView;


			highlightBox.PreventsPositionOutsideSuperview = true;
			highlightBox.Hidden = true;


			this.View.AddSubview(highlightBox);
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		partial void scanPressed (UIBarButtonItem sender)
		{
			Console.WriteLine("highlightBox frame:{0}",highlightBox.Frame);
		}

		partial void highlightPressed (UIBarButtonItem sender)
		{
			highlightBox.Hidden = !highlightBox.Hidden;
			webView.UserInteractionEnabled = highlightBox.Hidden;

			scanButton.Enabled = !highlightBox.Hidden;
		}
	}
}



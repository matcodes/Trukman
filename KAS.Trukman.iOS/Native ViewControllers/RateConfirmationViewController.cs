using System;
using UIKit;
using KAS.Esignature;
using Foundation;
using MessageUI;

namespace KAS.Trukman.iOS
{
	public partial class RateConfirmationViewController : UIViewController, IEditPdfDelegate, IMFMailComposeViewControllerDelegate
	{
		public NSUrl notSignedRateConfirmationURL;
		public NSUrl signedRateConfirmationURL;

		public RateConfirmationViewController () : base ("RateConfirmationViewController", null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
			logo.Image = UIImage.FromBundle("logo");
			sendButton.Alpha = (nfloat)0.5;
			sendButton.Enabled = false;

			this.Title = "Rate Confirmation";
			UIBarButtonItem item = new UIBarButtonItem ();
			item.Style = UIBarButtonItemStyle.Done;
			item.Title = "Done";
			item.TintColor = sendButton.BackgroundColor;
			item.Clicked += (sender , e) => {this.DismissViewController(true, null);};

			this.NavigationItem.RightBarButtonItem = item;
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		partial void signPressed(UIButton button)
		{
			SignPdfViewController signPdfViewController = new SignPdfViewController(notSignedRateConfirmationURL, PDFHandler.uniqueTimestamp());
			signPdfViewController.WeakDelegate = this;
			var navigationController = new UINavigationController (signPdfViewController);
			this.PresentViewController (navigationController, true, null);
		}
			
		partial void sendPressed(UIButton button)
		{
			if (MFMailComposeViewController.CanSendMail && signedRateConfirmationURL != null) {
				MFMailComposeViewController mail = new MFMailComposeViewController ();
				mail.WeakMailComposeDelegate = this;
				mail.SetSubject("Signed Rate Confirmation");

				NSData data = NSData.FromUrl (signedRateConfirmationURL);
				mail.AddAttachmentData (data, "application/pdf", "SignedRateConfirmation.pdf");
				this.PresentViewController (mail, true, null);
			} else {
				new UIAlertView("Error", "Configured email account was not found in your device settings. Please, setup your account first.", null, "Ok", null, null, null);
			}
		}
			
		partial void createJobPressed(UIButton button)
		{
			OCRPdfViewController ocr = new OCRPdfViewController();
			ocr.pdfUrl = notSignedRateConfirmationURL;
			NavigationController.PushViewController(ocr, true);
		}

		public void GetStoredPdfPath (string pdfPath) 
		{
			try {
				signedRateConfirmationURL = new NSUrl(pdfPath, false);
			} catch {
				
			}

			if (signedRateConfirmationURL != null) {
				sendButton.Alpha = 1;
				sendButton.Enabled = true;
			}
		}

		//
		//MFMailComposer Delegate Methods
		[Export ("mailComposeController:didFinishWithResult:error:")]
		public void Finished (MFMailComposeViewController controller, MFMailComposeResult result, NSError error)
		{
			if (error != null || result == MFMailComposeResult.Failed) {
				new UIAlertView ("Error", error.LocalizedDescription, null, "Ok", null, null, null);
			} else if (result == MFMailComposeResult.Sent || result == MFMailComposeResult.Cancelled) {
				this.DismissViewController(true, null);
			}
		}

		public static void handleFileURL(NSUrl url) {
			var rateConfirmationViewController = new RateConfirmationViewController();
			rateConfirmationViewController.notSignedRateConfirmationURL = url;

			var window = UIApplication.SharedApplication.KeyWindow;
			var vc = window.RootViewController;
			while (vc.PresentedViewController != null)
			{
				vc = vc.PresentedViewController;
			}

			var navigationController = new UINavigationController (rateConfirmationViewController);
			try {
				vc.PresentViewController (navigationController, true, null);
			} catch (Exception e) {
				Console.WriteLine("Rate Confirmation Presentation Exception: {0}", e.ToString());
			}
		}
	}
}



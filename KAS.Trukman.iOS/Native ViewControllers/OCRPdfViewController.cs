using System;
using System.Threading.Tasks;
using UIKit;
using Foundation;
using KAS.SPResizableView;
using CoreGraphics;
using KAS.Trukman.OCR;
using MBProgressHUD;
using KAS.Trukman.Storage.ParseClasses;
using KAS.Trukman.Data.Classes;

namespace KAS.Trukman.iOS
{
	public partial class OCRPdfViewController : UIViewController
	{
		public NSUrl pdfUrl;
		public TRUserResizableView highlightBox;
//		public ParseJob job;
		public Trip job;

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
			highlightBox = new TRUserResizableView (frame);

			UIView contentView = new UIView (highlightBox.Bounds);
			contentView.BackgroundColor = UIColor.Blue.ColorWithAlpha (0.2f);
			highlightBox.ContentView = contentView;


			highlightBox.PreventsPositionOutsideSuperview = true;
			highlightBox.Hidden = true;

			this.View.AddSubview(highlightBox);

			job = new Trip (); // ParseJob.Create<ParseJob> ();

			UIBarButtonItem rightBarItem = new UIBarButtonItem ();
			rightBarItem.Title = "Next";
			rightBarItem.Clicked += (object sender, EventArgs e) => {
				JobCreateViewController newJobViewController = new JobCreateViewController ();
				newJobViewController.job = this.job;
				this.NavigationController.PushViewController (newJobViewController, true);
			};
			this.NavigationItem.RightBarButtonItem = rightBarItem;
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		partial void scanPressed (UIBarButtonItem sender)
		{
			var hud = new MTMBProgressHUD (View) {
				LabelText = "Scanning...",
				RemoveFromSuperViewOnHide = true
			};
			hud.Show(true);

			View.AddSubview (hud);

			UIGraphics.BeginImageContextWithOptions(this.View.Bounds.Size, false, (nfloat)0.0);

			this.View.DrawViewHierarchy(this.View.Bounds,false);
			UIImage snapshot = UIGraphics.GetImageFromCurrentImageContext ();
			UIGraphics.EndImageContext();

			CGRect convertedFrame = this.View.ConvertRectFromView(highlightBox.ContentView.Frame, highlightBox);
			UIImage croppedImage = ImageCategory.cropImage(snapshot, convertedFrame);
			NSData imageData = croppedImage.AsJPEG();
			Byte[] byteArray = new Byte[imageData.Length];
			System.Runtime.InteropServices.Marshal.Copy(imageData.Bytes, byteArray, 0, Convert.ToInt32(imageData.Length));

			OCRApi ocr = new OCRApi();
			Console.WriteLine("highlightBox frame:{0}",highlightBox.Frame);

			try {
				Task parseTask = ocr.Parse(byteArray)
					.ContinueWith((task) => {
						InvokeOnMainThread ( () => {
							// manipulate UI controls
							hud.Hide(true);

							if (task != null) {
								// And so on...
								OCRResponse response = task.Result;

								if (response.ParsedResults.Count > 0) {
									Parsedresult results = response.ParsedResults[0];
									if (response.OCRExitCode == 1) {
										OCRResultViewController vc = new OCRResultViewController();
										vc.text = results.ParsedText;
										vc.job = job;
										this.PresentViewController(vc, true, null);
										return;
									}
								}
							}
							UIAlertView alertView = new UIAlertView("Error", "Can not scan the image", null, "Ok", null);
							alertView.Show();
						});
					});
			} catch (Exception e) {
				Console.WriteLine("OCR exception:{0}",e);
			
				hud.Hide(true);
			}
		}

		partial void highlightPressed (UIBarButtonItem sender)
		{
			highlightBox.Hidden = !highlightBox.Hidden;
			webView.UserInteractionEnabled = highlightBox.Hidden;

			scanButton.Enabled = !highlightBox.Hidden;
		}
	}
}



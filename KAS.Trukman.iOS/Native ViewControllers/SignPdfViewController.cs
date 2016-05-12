using System;
using KAS.Esignature;
using UIKit;
using Foundation;

namespace KAS.Trukman.iOS
{
	public partial class SignPdfViewController : EditPdfViewController
	{
		public SignPdfViewController (NSUrl pdfUrl, string fileName) : base(pdfUrl, fileName) {
			
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
			//this.NavigationController.NavigationBar.BarTintColor = UIColor.White;
			//this.NavigationController.NavigationBar.TintColor = UIColor.Black;
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}



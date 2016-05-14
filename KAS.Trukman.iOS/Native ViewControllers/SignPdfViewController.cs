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
			UINavigationBar.Appearance.BarTintColor = UIColor.White;
			UINavigationBar.Appearance.TintColor = UIColor.Black;
			UINavigationBar.Appearance.TitleTextAttributes = new UIStringAttributes() { ForegroundColor = UIColor.Black};

			UIToolbar.Appearance.TintColor = UIColor.Black;
			UIToolbar.Appearance.BarTintColor = UIColor.White;
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}



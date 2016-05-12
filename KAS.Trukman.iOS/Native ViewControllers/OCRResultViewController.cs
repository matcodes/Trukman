using System;

using UIKit;
using System.Collections.Generic;

namespace KAS.Trukman.iOS
{
	public partial class OCRResultViewController : UIViewController, IUIActionSheetDelegate
	{
		public OCRResultViewController () : base ("OCRResultViewController", null)
		{
		}

		public String text;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
			scannedTextView.Text = text;
			scannedTextView.BecomeFirstResponder ();
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		partial void savePressed (UIBarButtonItem sender)
		{
			List<String> list = new List<String>();
			list.Add("Pickup Address");
			list.Add("Drop off Address");
			list.Add("Contractor");
			list.Add("Weight");

			// You can convert it back to an array if you would like to
			String[] options = list.ToArray();
			UIActionSheet sheet = new UIActionSheet("Save as", this, "Cancel", null, options);
			sheet.ShowInView(this.View);
			scannedTextView.ResignFirstResponder();
		}

		partial void cancelPressed (UIBarButtonItem sender)
		{
			this.DismissViewController(true, null);
		}
	}
}



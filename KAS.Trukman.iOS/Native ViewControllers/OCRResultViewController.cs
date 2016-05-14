using System;

using UIKit;
using System.Collections.Generic;
using KAS.Trukman.Storage.ParseClasses;


namespace KAS.Trukman.iOS
{
	public partial class OCRResultViewController : UIViewController, IUIActionSheetDelegate
	{
		public ParseJob job;

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
			list.Add("Drop Time");
			list.Add("Pickup Time");

			// You can convert it back to an array if you would like to
			String[] options = list.ToArray();
			UIActionSheet sheet = new UIActionSheet("Save as", null, "Cancel", null, options);
			sheet.ShowInView(this.View);
			sheet.Clicked += delegate(object a, UIButtonEventArgs b) {
				Console.WriteLine ("Job Button " + b.ButtonIndex.ToString () + " clicked");
				if (scannedTextView.Text != null) {
					switch (b.ButtonIndex) {
					case 0:
						job.FromAddress = scannedTextView.Text;
						break;

					case 1:
						job.ToAddress = scannedTextView.Text;
						break;

					case 2:

						break;

					case 3:
						int weight = 0;
						if (Int32.TryParse (scannedTextView.Text, out weight)) {
							job.Weight = weight;
						}					
						break;

					case 4:
						DateTime pickupDate;
						if (DateTime.TryParse(scannedTextView.Text, out pickupDate))
						{
							job.PickupDatetime = pickupDate;
						} else {
							UIAlertView view = new UIAlertView("Error", "Can not parse your time input. Please try following format: \"April 20 2016 15:00\"", null, "Ok", null);
							view.Show();
						}

						break;

					case 5:
						DateTime dropDate;
						if (DateTime.TryParse(scannedTextView.Text, out dropDate))
						{
							job.DeliveryDatetime = dropDate;
						} else {
							UIAlertView alert = new UIAlertView("Error", "Can not define your time input. Please try following format: \"April 20 2016 15:00\"", null, "Ok", null);
							alert.Show();
						}

						break;
					default:
						break;
					}
					this.DismissViewController(true, null);
				}
			};

			scannedTextView.ResignFirstResponder();
		}
			
		partial void cancelPressed (UIBarButtonItem sender)
		{
			this.DismissViewController(true, null);
		}
	}
}



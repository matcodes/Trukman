using System;

using UIKit;
using KAS.Trukman.Data.Maps;

namespace KAS.Trukman.iOS
{
	#region CarInfoViewController
	public partial class CarInfoViewController : UIViewController
	{
		public CarInfoViewController () 
			: base ("CarInfoViewController", null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			distanceLabel.Text = (this.CarInfo != null ? this.CarInfo.GetDistanceTextFromMiles () : "");
			timeLabel.Text = (this.CarInfo != null ? this.CarInfo.GetDurationText () : "");
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
		}

		public CarInfo CarInfo { get; set; }
	}
	#endregion
}



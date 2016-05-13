using System;
using Foundation;
using UIKit;
using KAS.Trukman.Storage.ParseClasses;

namespace KAS.Trukman.iOS
{
	public partial class JobCreateViewController : UIViewController, IUITableViewDataSource, IUITableViewDelegate
	{
		enum JobFields {
			Number,
			From,
			To,
			Broker,
			Driver
		};

		public ParseJob job = null;

		public JobCreateViewController () : base ("JobCreateViewController", null)
		{
		}


		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
			UINib nibButton = UINib.FromName ("JobButtonCreateViewCell", NSBundle.MainBundle);
			UINib nibText = UINib.FromName ("JobTextCreateViewCell", NSBundle.MainBundle);

			tableView.RegisterNibForCellReuse (nibButton, "userCell");
			tableView.RegisterNibForCellReuse (nibText, "detailCell");

			tableView.WeakDataSource = this;
			tableView.WeakDelegate = this;

			tableView.RowHeight = 50;

			this.Title = "New job";
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			tableView.ReloadData();
		}

		[Export ("tableView:cellForRowAtIndexPath:"), Preserve (Conditional = true)]
		public UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath) 
		{
			switch (indexPath.Row) {
			case (int)JobFields.Number:
				JobTextCreateViewCell numberCell = (JobTextCreateViewCell)tableView.DequeueReusableCell ("detailCell");
				numberCell.textField.Placeholder = "job number";
				numberCell._textLabel.Text = "Job #:";
					
				return numberCell;


			case (int)JobFields.From:
				JobTextCreateViewCell fromCell = (JobTextCreateViewCell)tableView.DequeueReusableCell ("detailCell");
				fromCell.textField.Placeholder = "pick up address";
				fromCell._textLabel.Text = "Pick up:";
				return fromCell;

			
			case (int)JobFields.To:
				JobTextCreateViewCell toCell = (JobTextCreateViewCell)tableView.DequeueReusableCell ("detailCell");
				toCell.textField.Placeholder = "delivery address";
				toCell._textLabel.Text = "Delivery:";
					
				return toCell;


			case (int)JobFields.Broker:
				JobButtonCreateViewCell brokerCell = (JobButtonCreateViewCell)tableView.DequeueReusableCell ("userCell");
				if (job.Driver == null) {
					brokerCell.actionButton.SetTitle ("assign", UIControlState.Normal);
				} else {
					brokerCell.actionButton.SetTitle (job.Broker.Username, UIControlState.Normal);
				}

				brokerCell._textLabel.Text = "Broker";

				return brokerCell;


			case (int)JobFields.Driver:
				JobButtonCreateViewCell driverCell = (JobButtonCreateViewCell)tableView.DequeueReusableCell ("userCell");
				if (job.Driver == null) {
					driverCell.actionButton.SetTitle ("assign", UIControlState.Normal);
				} else {
					driverCell.actionButton.SetTitle (job.Driver.Username, UIControlState.Normal);
				}

				driverCell._textLabel.Text = "Driver";
					
				return driverCell;

			default:
				UITableViewCell defaultCell = tableView.DequeueReusableCell ("detailCell");
				return defaultCell;
			}
		}



		[Export ("tableView:numberOfRowsInSection:"), Preserve (Conditional = true)]
		public nint RowsInSection (UITableView tableView, nint section)
		{
			return 5;
		}
	}
}



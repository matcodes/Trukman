using System;
using Foundation;
using UIKit;
using KAS.Trukman.Storage.ParseClasses;
using KAS.Trukman.Storage;
using KAS.Trukman.AppContext;
using System.Threading.Tasks;

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
		public ParseCompany company = null;

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

			UIBarButtonItem rightBarItem = new UIBarButtonItem ();
			rightBarItem.Title = "Done";
			rightBarItem.Clicked += (object sender, EventArgs e) => {
				Task.Run(async() => {
					Console.Write("Creating job with Ref Number:{0}", job.JobRef);

					if (company == null) {
						company = await TrukmanContext.FetchParseCompany(TrukmanContext.Company.Name);
					}
					this.InvokeOnMainThread(() => {
						if (job.Broker != null && job.Driver != null && company != null) {
							job.SaveAsync();
							this.NavigationController.DismissViewController(true, null);
						} else {
							UIAlertView alertView = new UIAlertView("Error", "Please, assign broker and driver to the job.", null, "Ok", null);
							alertView.Show();
						}
					});
				});
			};
			this.NavigationItem.RightBarButtonItem = rightBarItem;
			Task.Run(async() => {
				company = await TrukmanContext.FetchParseCompany(TrukmanContext.Company.Name);
			});
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
				numberCell.textField.Text = job.JobRef;
				numberCell.SelectionStyle = UITableViewCellSelectionStyle.None;

				numberCell.textField.EditingChanged += (object sender, EventArgs e) => {
					//Action
					var item = (sender as UITextField);
					if (sender != null) {
						job.JobRef = item.Text;
					}
				};
				numberCell.textField.EditingDidEndOnExit += (object sender, EventArgs e) => {
					var item = (sender as UITextField);
					if (sender != null) {
						item.ResignFirstResponder ();
					}
				};
					
				return numberCell;


			case (int)JobFields.From:
				JobTextCreateViewCell fromCell = (JobTextCreateViewCell)tableView.DequeueReusableCell ("detailCell");
				fromCell.textField.Placeholder = "pick up address";
				fromCell._textLabel.Text = "Pick up:";
				fromCell.textField.Text = job.FromAddress;
				fromCell.SelectionStyle = UITableViewCellSelectionStyle.None;

				fromCell.textField.EditingChanged += (object sender, EventArgs e) => {
					//Action
					var item = (sender as UITextField);
					if (sender != null) {
						job.FromAddress = item.Text;
					}
				};
				fromCell.textField.EditingDidEndOnExit += (object sender, EventArgs e) => {
					var item = (sender as UITextField);
					if (sender != null) {
						item.ResignFirstResponder ();
					}
				};
					

				return fromCell;

			
			case (int)JobFields.To:
				JobTextCreateViewCell toCell = (JobTextCreateViewCell)tableView.DequeueReusableCell ("detailCell");
				toCell.textField.Placeholder = "delivery address";
				toCell.textField.Text = job.ToAddress;
				toCell._textLabel.Text = "Delivery:";
				toCell.SelectionStyle = UITableViewCellSelectionStyle.None;

				toCell.textField.EditingDidEndOnExit += (object sender, EventArgs e) => {
					var item = (sender as UITextField);
					if (sender != null) {
						item.ResignFirstResponder ();
					}
				};

				toCell.textField.EditingChanged += (object sender, EventArgs e) => 
				{
					//Action
					var item = (sender as UITextField);
					if (sender != null) {
						job.ToAddress = item.Text;
					}
				};
					
				return toCell;


			case (int)JobFields.Broker:
				JobButtonCreateViewCell brokerCell = (JobButtonCreateViewCell)tableView.DequeueReusableCell ("userCell");
				if (job.Driver == null) {
					brokerCell.actionButton.SetTitle ("assign", UIControlState.Normal);
				} else {
					brokerCell.actionButton.SetTitle (job.Broker.Username, UIControlState.Normal);
				}

				brokerCell._textLabel.Text = "Broker:";
				brokerCell.SelectionStyle = UITableViewCellSelectionStyle.None;
				brokerCell.actionButton.TouchUpInside += (object sender, EventArgs e) => 
				{
					UserListViewController list = new UserListViewController();
					list.dataSourceType = UserListDataSource.Broker;
					list.company = company;

					this.NavigationController.PushViewController(list, true);
				};
				return brokerCell;


			case (int)JobFields.Driver:
				JobButtonCreateViewCell driverCell = (JobButtonCreateViewCell)tableView.DequeueReusableCell ("userCell");
				if (job.Driver == null) {
					driverCell.actionButton.SetTitle ("assign", UIControlState.Normal);
				} else {
					driverCell.actionButton.SetTitle (job.Driver.Username, UIControlState.Normal);
				}

				driverCell._textLabel.Text = "Driver:";
				driverCell.SelectionStyle = UITableViewCellSelectionStyle.None;
				driverCell.actionButton.TouchUpInside += (object sender, EventArgs e) => 
				{
					UserListViewController list = new UserListViewController();
					list.dataSourceType = UserListDataSource.Driver;
					list.company = company;

					this.NavigationController.PushViewController(list, true);
				};
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



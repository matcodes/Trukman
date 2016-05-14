using System;
using Foundation;
using UIKit;
using KAS.Trukman.Storage.ParseClasses;
using KAS.Trukman.Storage;
using KAS.Trukman.AppContext;
using System.Threading.Tasks;
using MBProgressHUD;

namespace KAS.Trukman.iOS
{
	public partial class JobCreateViewController : UIViewController, IUITableViewDataSource, IUITableViewDelegate
	{
		enum JobFields {
			Number,
			From,
			To,
			FromTime,
			ToTime,
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
				var hud = new MTMBProgressHUD (View) {
					LabelText = "Saving...",
					RemoveFromSuperViewOnHide = true
				};
				hud.Show(true);

				View.AddSubview (hud);
				Task.Run(async() => {
					Console.Write("Creating job with Ref Number:{0}", job.JobRef);

					if (company == null) {
						company = await TrukmanContext.FetchParseCompany(TrukmanContext.Company.Name);
					}
					this.InvokeOnMainThread(() => {
						if (/*job.Broker != null &&*/ job.Driver != null && company != null) {
							job.Company = company;
							try {
								job.SaveAsync();
							} catch {
							}
							this.NavigationController.DismissViewController(true, null);
						} else {
							UIAlertView alertView = new UIAlertView("Error", "Please, assign broker and driver to the job.", null, "Ok", null);
							alertView.Show();
						}
						hud.Hide(true);
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
				numberCell.textField.Tag = (int)JobFields.Number;

				numberCell.textField.EditingChanged -= TextFieldDidEndEditing;
				numberCell.textField.EditingChanged += TextFieldDidEndEditing;

				numberCell.textField.EditingDidEndOnExit -= TextFieldEditingDidEndOnExit;
				numberCell.textField.EditingDidEndOnExit += TextFieldEditingDidEndOnExit;
					
				return numberCell;


			case (int)JobFields.From:
				JobTextCreateViewCell fromCell = (JobTextCreateViewCell)tableView.DequeueReusableCell ("detailCell");
				fromCell.textField.Placeholder = "pick up address";
				fromCell._textLabel.Text = "Pick up:";
				fromCell.textField.Text = job.FromAddress;
				fromCell.SelectionStyle = UITableViewCellSelectionStyle.None;
				fromCell.textField.Tag = (int)JobFields.From;
				fromCell.textField.EditingChanged -= TextFieldDidEndEditing;
				fromCell.textField.EditingChanged += TextFieldDidEndEditing;

				fromCell.textField.EditingDidEndOnExit -= TextFieldEditingDidEndOnExit;
				fromCell.textField.EditingDidEndOnExit += TextFieldEditingDidEndOnExit;
					

				return fromCell;

			case (int)JobFields.FromTime:
				JobTextCreateViewCell fromTimeCell = (JobTextCreateViewCell)tableView.DequeueReusableCell ("detailCell");
				fromTimeCell.textField.Placeholder = "pick up time";
				fromTimeCell._textLabel.Text = "Pick Up Time:";
				if (job.PickupDatetime != DateTime.MinValue) {
					fromTimeCell.textField.Text = string.Format("{0} {1}",job.PickupDatetime.ToShortDateString (), job.PickupDatetime.ToShortTimeString ());
				} else {
					fromTimeCell.textField.Text = null;
				}
				fromTimeCell.SelectionStyle = UITableViewCellSelectionStyle.None;
				fromTimeCell.textField.Tag = (int)JobFields.FromTime;
				fromTimeCell.textField.EditingChanged -= TextFieldDidEndEditing;
				fromTimeCell.textField.EditingChanged += TextFieldDidEndEditing;

				fromTimeCell.textField.EditingDidEndOnExit -= TextFieldEditingDidEndOnExit;
				fromTimeCell.textField.EditingDidEndOnExit += TextFieldEditingDidEndOnExit;

				return fromTimeCell;

			
			case (int)JobFields.To:
				JobTextCreateViewCell toCell = (JobTextCreateViewCell)tableView.DequeueReusableCell ("detailCell");
				toCell.textField.Placeholder = "delivery address";
				toCell.textField.Text = job.ToAddress;
				toCell._textLabel.Text = "Delivery:";
				toCell.SelectionStyle = UITableViewCellSelectionStyle.None;

				toCell.textField.Tag = (int)JobFields.To;
				toCell.textField.EditingDidEndOnExit -= TextFieldEditingDidEndOnExit;
				toCell.textField.EditingDidEndOnExit += TextFieldEditingDidEndOnExit;

				toCell.textField.EditingChanged -= TextFieldDidEndEditing;
				toCell.textField.EditingChanged += TextFieldDidEndEditing; 
					
				return toCell;

			case (int)JobFields.ToTime:
				JobTextCreateViewCell toTimeCell = (JobTextCreateViewCell)tableView.DequeueReusableCell ("detailCell");
				toTimeCell.textField.Placeholder = "drop time";
				toTimeCell._textLabel.Text = "Drop Time:";
				if (job.DeliveryDatetime != DateTime.MinValue) {
					toTimeCell.textField.Text =  string.Format("{0} {1}",job.DeliveryDatetime.ToShortDateString (), job.DeliveryDatetime.ToShortTimeString ());;
				} else {
					toTimeCell.textField.Text = null;
				}
				toTimeCell.SelectionStyle = UITableViewCellSelectionStyle.None;
				toTimeCell.textField.Tag = (int)JobFields.ToTime;
				toTimeCell.textField.EditingChanged -= TextFieldDidEndEditing;
				toTimeCell.textField.EditingChanged += TextFieldDidEndEditing;

				toTimeCell.textField.EditingDidEndOnExit -= TextFieldEditingDidEndOnExit;
				toTimeCell.textField.EditingDidEndOnExit += TextFieldEditingDidEndOnExit;


				return toTimeCell;


			case (int)JobFields.Broker:
				JobButtonCreateViewCell brokerCell = (JobButtonCreateViewCell)tableView.DequeueReusableCell ("userCell");
				if (job.Broker == null) {
					brokerCell.actionButton.SetTitle ("assign", UIControlState.Normal);
				} else {
					brokerCell.actionButton.SetTitle (job.Broker.Username, UIControlState.Normal);
				}

				brokerCell._textLabel.Text = "Broker:";
				brokerCell.SelectionStyle = UITableViewCellSelectionStyle.None;
				brokerCell.actionButton.Tag = (int)JobFields.Broker;
				brokerCell.actionButton.TouchUpInside -= actionButtonClicked;
				brokerCell.actionButton.TouchUpInside += actionButtonClicked;
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
				driverCell.actionButton.Tag = (int)JobFields.Driver;
				driverCell.actionButton.TouchUpInside -= actionButtonClicked;
				driverCell.actionButton.TouchUpInside += actionButtonClicked;
				return driverCell;

			default:
				UITableViewCell defaultCell = tableView.DequeueReusableCell ("detailCell");
				return defaultCell;
			}
		}

		private void actionButtonClicked(object sender, EventArgs e)
		{
			UIButton button = (UIButton)sender;
			if (button != null) {
				UserListViewController list = new UserListViewController();

				if (button.Tag == (int)JobFields.Driver) {
					list.dataSourceType = UserListDataSource.Driver;
				} else {
					list.dataSourceType = UserListDataSource.Broker;
				}

				list.company = company;
				list.job = job;

				this.NavigationController.PushViewController(list, true);
			}
		}

		private void TextFieldEditingDidEndOnExit(object sender, EventArgs e) {
			UITextField field = (UITextField)sender;
			if (field != null) {
				field.ResignFirstResponder ();
			}
		}

		private void TextFieldDidEndEditing(object sender, EventArgs e) {
			UITextField field = (UITextField)sender;
			if (field != null) {
				switch (field.Tag) {
				case (int)JobFields.To:
					job.ToAddress = field.Text;
					break;

				case (int)JobFields.From:
					job.FromAddress = field.Text;
					break;
				case (int)JobFields.Number:
					job.JobRef = field.Text;
					break;

				}
			}
		}



		[Export ("tableView:numberOfRowsInSection:"), Preserve (Conditional = true)]
		public nint RowsInSection (UITableView tableView, nint section)
		{
			return 5;
		}
	}
}



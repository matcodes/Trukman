using System;
using Foundation;
using UIKit;
using KAS.Trukman.Storage.ParseClasses;
using KAS.Trukman.Storage;
using KAS.Trukman.AppContext;
using System.Threading.Tasks;
using MBProgressHUD;
using KAS.Trukman.Data.Classes;
using KAS.Trukman.Extensions;
using KAS.Trukman.Messages;

namespace KAS.Trukman.iOS
{
    public partial class JobCreateViewController : UIViewController, IUITableViewDataSource, IUITableViewDelegate
    {
        enum JobFields
        {
            Number,
            Weight,
            ShipperName,
            FromAddress,
            PickupTime,
            ReceiverName,
            ToAddress,
            DropTime,
            Price,
            Broker,
            Driver
        };

        public Trip job = null;
        public Company company = null;

        public JobCreateViewController() : base("JobCreateViewController", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            UINib nibButton = UINib.FromName("JobButtonCreateViewCell", NSBundle.MainBundle);
            UINib nibText = UINib.FromName("JobTextCreateViewCell", NSBundle.MainBundle);

            tableView.RegisterNibForCellReuse(nibButton, "userCell");
            tableView.RegisterNibForCellReuse(nibText, "detailCell");

            tableView.WeakDataSource = this;
            tableView.WeakDelegate = this;

            tableView.RowHeight = 50;
            this.Title = "New job";

            UIBarButtonItem rightBarItem = new UIBarButtonItem();
            rightBarItem.Title = "Done";
            rightBarItem.Clicked += (object sender, EventArgs e) =>
            {
                var hud = new MTMBProgressHUD(View)
                {
                    LabelText = "Saving...",
                    RemoveFromSuperViewOnHide = true
                };
                hud.Show(true);

                View.AddSubview(hud);
                Task.Run(() =>
                {
                    Console.Write("Creating job with Ref Number:{0}", job.JobRef);

                    if (company == null)
                    {
                        company = (TrukmanContext.Company as Company); // await TrukmanContext.FetchParseCompany(TrukmanContext.Company.Name);
                    }
                    this.InvokeOnMainThread(async () =>
                    {
                        if (/*job.Broker != null &&*/ job.Driver != null && company != null)
                        {
                            job.Company = company;
                            try
                            {
                                job = await TrukmanContext.CreateTripAsync(job); // job.SaveAsync();
                            }
                            catch (Exception exception)
                            {
                                ShowToastMessage.Send(exception.Message);
                            }
                            this.NavigationController.DismissViewController(true, null);
                        }
                        else
                        {
                            UIAlertView alertView = new UIAlertView("Error", "Please, assign broker and driver to the job.", null, "Ok", null);
                            alertView.Show();
                        }
                        hud.Hide(true);
                    });
                }).LogExceptions("JobCreateViewController ViewDidLoad");
            };
            this.NavigationItem.RightBarButtonItem = rightBarItem;
            Task.Run(() =>
            {
                company = (TrukmanContext.Company as Company); // await TrukmanContext.FetchParseCompany(TrukmanContext.Company.Name);
            }).LogExceptions("JobCreateViewController ViewDidLoad company");
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            tableView.ReloadData();
        }

        [Export("tableView:cellForRowAtIndexPath:"), Preserve(Conditional = true)]
        public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            switch (indexPath.Row)
            {
                case (int)JobFields.Number:
                    JobTextCreateViewCell numberCell = (JobTextCreateViewCell)tableView.DequeueReusableCell("detailCell");
                    numberCell.textField.Placeholder = "job number";
                    numberCell._textLabel.Text = "Job #:";
                    numberCell.textField.Text = job.JobRef;
                    numberCell.SelectionStyle = UITableViewCellSelectionStyle.None;
                    numberCell.textField.Tag = (int)JobFields.Number;
                    numberCell.textField.EditingDidEnd -= TextFieldDidEndEditing;
                    numberCell.textField.EditingDidEnd += TextFieldDidEndEditing;
                    numberCell.textField.EditingDidEndOnExit -= TextFieldEditingDidEndOnExit;
                    numberCell.textField.EditingDidEndOnExit += TextFieldEditingDidEndOnExit;
                    return numberCell;

                case (int)JobFields.Weight:
                    JobTextCreateViewCell weightCell = (JobTextCreateViewCell)tableView.DequeueReusableCell("detailCell");
                    weightCell.textField.Placeholder = "weight";
                    weightCell._textLabel.Text = "Weight:";
                    weightCell.textField.Text = job.Weight.ToString();
                    weightCell.SelectionStyle = UITableViewCellSelectionStyle.None;
                    weightCell.textField.Tag = (int)JobFields.Weight;
                    weightCell.textField.EditingDidEnd -= TextFieldDidEndEditing;
                    weightCell.textField.EditingDidEnd += TextFieldDidEndEditing;
                    weightCell.textField.EditingDidEndOnExit -= TextFieldEditingDidEndOnExit;
                    weightCell.textField.EditingDidEndOnExit += TextFieldEditingDidEndOnExit;
                    return weightCell;

                case (int)JobFields.ShipperName:
                    JobTextCreateViewCell shipperCell = (JobTextCreateViewCell)tableView.DequeueReusableCell("detailCell");
                    shipperCell.textField.Placeholder = "shipper name";
                    shipperCell._textLabel.Text = "Shipper name:";
                    shipperCell.textField.Text = job.Shipper.Name;
                    shipperCell.SelectionStyle = UITableViewCellSelectionStyle.None;
                    shipperCell.textField.Tag = (int)JobFields.ShipperName;
                    shipperCell.textField.EditingDidEnd -= TextFieldDidEndEditing;
                    shipperCell.textField.EditingDidEnd += TextFieldDidEndEditing;
                    shipperCell.textField.EditingDidEndOnExit -= TextFieldEditingDidEndOnExit;
                    shipperCell.textField.EditingDidEndOnExit += TextFieldEditingDidEndOnExit;
                    return shipperCell;

                case (int)JobFields.FromAddress:
                    JobTextCreateViewCell fromAddressCell = (JobTextCreateViewCell)tableView.DequeueReusableCell("detailCell");
                    fromAddressCell.textField.Placeholder = "pick up address";
                    fromAddressCell._textLabel.Text = "Pick Up:";
                    fromAddressCell.textField.Text = job.FromAddress;
                    fromAddressCell.SelectionStyle = UITableViewCellSelectionStyle.None;
                    fromAddressCell.textField.Tag = (int)JobFields.FromAddress;
                    fromAddressCell.textField.EditingDidEnd -= TextFieldDidEndEditing;
                    fromAddressCell.textField.EditingDidEnd += TextFieldDidEndEditing;
                    fromAddressCell.textField.EditingDidEndOnExit -= TextFieldEditingDidEndOnExit;
                    fromAddressCell.textField.EditingDidEndOnExit += TextFieldEditingDidEndOnExit;
                    return fromAddressCell;

                case (int)JobFields.PickupTime:
                    JobTextCreateViewCell pickupTimeCell = (JobTextCreateViewCell)tableView.DequeueReusableCell("detailCell");
                    pickupTimeCell.textField.Placeholder = "pick up time";
                    pickupTimeCell._textLabel.Text = "Pickup Time:";
                    if (job.PickupDatetime != DateTime.MinValue)
                        pickupTimeCell.textField.Text = string.Format("{0} {1}", job.PickupDatetime.ToShortDateString(), job.PickupDatetime.ToShortTimeString());
                    else
                        pickupTimeCell.textField.Text = null;
                    pickupTimeCell.SelectionStyle = UITableViewCellSelectionStyle.None;
                    pickupTimeCell.textField.Tag = (int)JobFields.PickupTime;
                    pickupTimeCell.textField.EditingDidEnd -= TextFieldDidEndEditing;
                    pickupTimeCell.textField.EditingDidEnd += TextFieldDidEndEditing;
                    pickupTimeCell.textField.EditingDidEndOnExit -= TextFieldEditingDidEndOnExit;
                    pickupTimeCell.textField.EditingDidEndOnExit += TextFieldEditingDidEndOnExit;
                    return pickupTimeCell;

                case (int)JobFields.ReceiverName:
                    JobTextCreateViewCell receiverCell = (JobTextCreateViewCell)tableView.DequeueReusableCell("detailCell");
                    receiverCell.textField.Placeholder = "receiver name";
                    receiverCell._textLabel.Text = "Receiver name:";
                    receiverCell.textField.Text = job.Receiver.Name;
                    receiverCell.SelectionStyle = UITableViewCellSelectionStyle.None;
                    receiverCell.textField.Tag = (int)JobFields.ReceiverName;
                    receiverCell.textField.EditingDidEndOnExit -= TextFieldEditingDidEndOnExit;
                    receiverCell.textField.EditingDidEndOnExit += TextFieldEditingDidEndOnExit;
                    receiverCell.textField.EditingDidEnd -= TextFieldDidEndEditing;
                    receiverCell.textField.EditingDidEnd += TextFieldDidEndEditing;
                    return receiverCell;

                case (int)JobFields.ToAddress:
                    JobTextCreateViewCell toAddressCell = (JobTextCreateViewCell)tableView.DequeueReusableCell("detailCell");
                    toAddressCell.textField.Placeholder = "delivery address";
                    toAddressCell._textLabel.Text = "Delivery:";
                    toAddressCell.textField.Text = job.ToAddress;
                    toAddressCell.SelectionStyle = UITableViewCellSelectionStyle.None;
                    toAddressCell.textField.Tag = (int)JobFields.ToAddress;
                    toAddressCell.textField.EditingDidEndOnExit -= TextFieldEditingDidEndOnExit;
                    toAddressCell.textField.EditingDidEndOnExit += TextFieldEditingDidEndOnExit;
                    toAddressCell.textField.EditingDidEnd -= TextFieldDidEndEditing;
                    toAddressCell.textField.EditingDidEnd += TextFieldDidEndEditing;
                    return toAddressCell;

                case (int)JobFields.DropTime:
                    JobTextCreateViewCell dropTimeCell = (JobTextCreateViewCell)tableView.DequeueReusableCell("detailCell");
                    dropTimeCell.textField.Placeholder = "drop time";
                    dropTimeCell._textLabel.Text = "Drop Time:";
                    if (job.DeliveryDatetime != DateTime.MinValue)
                        dropTimeCell.textField.Text = string.Format("{0} {1}", job.DeliveryDatetime.ToShortDateString(), job.DeliveryDatetime.ToShortTimeString());
                    else
                        dropTimeCell.textField.Text = null;
                    dropTimeCell.SelectionStyle = UITableViewCellSelectionStyle.None;
                    dropTimeCell.textField.Tag = (int)JobFields.DropTime;
                    dropTimeCell.textField.EditingDidEnd -= TextFieldDidEndEditing;
                    dropTimeCell.textField.EditingDidEnd += TextFieldDidEndEditing;
                    dropTimeCell.textField.EditingDidEndOnExit -= TextFieldEditingDidEndOnExit;
                    dropTimeCell.textField.EditingDidEndOnExit += TextFieldEditingDidEndOnExit;
                    return dropTimeCell;
                case (int)JobFields.Price:
                    JobTextCreateViewCell priceTimeCell = (JobTextCreateViewCell)tableView.DequeueReusableCell("detailCell");
                    priceTimeCell.textField.Placeholder = "price";
                    priceTimeCell._textLabel.Text = "Price:";
                    priceTimeCell.textField.Text = job.Price.ToString();
                    priceTimeCell.SelectionStyle = UITableViewCellSelectionStyle.None;
                    priceTimeCell.textField.Tag = (int)JobFields.Price;
                    priceTimeCell.textField.EditingDidEnd -= TextFieldDidEndEditing;
                    priceTimeCell.textField.EditingDidEnd += TextFieldDidEndEditing;
                    priceTimeCell.textField.EditingDidEndOnExit -= TextFieldEditingDidEndOnExit;
                    priceTimeCell.textField.EditingDidEndOnExit += TextFieldEditingDidEndOnExit;
                    return priceTimeCell;

                case (int)JobFields.Broker:
                    JobButtonCreateViewCell brokerCell = (JobButtonCreateViewCell)tableView.DequeueReusableCell("userCell");
                    if (job.Broker == null)
                        brokerCell.actionButton.SetTitle("assign", UIControlState.Normal);
                    else
                        brokerCell.actionButton.SetTitle(job.Broker.UserName, UIControlState.Normal);
                    brokerCell._textLabel.Text = "Broker:";
                    brokerCell.SelectionStyle = UITableViewCellSelectionStyle.None;
                    brokerCell.actionButton.Tag = (int)JobFields.Broker;
                    brokerCell.actionButton.TouchUpInside -= actionButtonClicked;
                    brokerCell.actionButton.TouchUpInside += actionButtonClicked;
                    return brokerCell;
                case (int)JobFields.Driver:
                    JobButtonCreateViewCell driverCell = (JobButtonCreateViewCell)tableView.DequeueReusableCell("userCell");
                    if (job.Driver == null)
                        driverCell.actionButton.SetTitle("assign", UIControlState.Normal);
                    else
                        driverCell.actionButton.SetTitle(job.Driver.UserName, UIControlState.Normal);
                    driverCell._textLabel.Text = "Driver:";
                    driverCell.SelectionStyle = UITableViewCellSelectionStyle.None;
                    driverCell.actionButton.Tag = (int)JobFields.Driver;
                    driverCell.actionButton.TouchUpInside -= actionButtonClicked;
                    driverCell.actionButton.TouchUpInside += actionButtonClicked;
                    return driverCell;

                default:
                    UITableViewCell defaultCell = tableView.DequeueReusableCell("detailCell");
                    return defaultCell;
            }
        }

        private void actionButtonClicked(object sender, EventArgs e)
        {
            UIButton button = (UIButton)sender;
            if (button != null)
            {
                UserListViewController list = new UserListViewController();

                if (button.Tag == (int)JobFields.Driver)
                    list.dataSourceType = UserListDataSource.Driver;
                else
                    list.dataSourceType = UserListDataSource.Broker;

                list.company = (TrukmanContext.Company as Company);
                list.job = job;

                this.NavigationController.PushViewController(list, true);
            }
        }

        private void TextFieldEditingDidEndOnExit(object sender, EventArgs e)
        {
            UITextField field = (UITextField)sender;
            if (field != null)
            {
                switch (field.Tag)
                {
                    case (int)JobFields.DropTime:
                        DateTime dropDate;
                        if (DateTime.TryParse(field.Text, out dropDate))
                            job.DeliveryDatetime = dropDate;
                        else
                        {
                            UIAlertView alert = new UIAlertView("Error", "Can not define your Delivery time input. Please try following format: \"April 20 2016 15:00\"", null, "Ok", null);
                            alert.Show();
                        }
                        break;
                    case (int)JobFields.PickupTime:
                        DateTime pickDate;
                        if (DateTime.TryParse(field.Text, out pickDate))
                            job.PickupDatetime = pickDate;
                        else
                        {
                            UIAlertView alert = new UIAlertView("Error", "Can not define your Pick up time input. Please try following format: \"April 20 2016 15:00\"", null, "Ok", null);
                            alert.Show();
                        }
                        break;

                    default:
                        break;
                }

                field.ResignFirstResponder();
            }
        }

        private void TextFieldDidEndEditing(object sender, EventArgs e)
        {
            UITextField field = (UITextField)sender;
            if (field != null)
            {
                switch (field.Tag)
                {
                    case (int)JobFields.Number:
                        job.JobRef = field.Text;
                        break;
                    case (int)JobFields.Weight:
                        int weight = 0;
                        if (int.TryParse(field.Text, out weight))
                            job.Weight = weight;
                        break;
                    case (int)JobFields.FromAddress:
                        job.FromAddress = field.Text;
                        break;
                    case (int)JobFields.PickupTime:
                        DateTime pickDate;
                        if (DateTime.TryParse(field.Text, out pickDate))
                            job.PickupDatetime = pickDate;
                        else
                        {
                            UIAlertView alert = new UIAlertView("Error", "Can not define your Pick up time input. Please try following format: \"April 20 2016 15:00\"", null, "Ok", null);
                            alert.Show();
                        }
                        break;
                    case (int)JobFields.ShipperName:
                        job.Shipper.Name = field.Text;
                        break;
                    case (int)JobFields.ToAddress:
                        job.ToAddress = field.Text;
                        break;
                    case (int)JobFields.DropTime:
                        DateTime dropDate;
                        if (DateTime.TryParse(field.Text, out dropDate))
                            job.DeliveryDatetime = dropDate;
                        else
                        {
                            UIAlertView alert = new UIAlertView("Error", "Can not define your Delivery time input. Please try following format: \"April 20 2016 15:00\"", null, "Ok", null);
                            alert.Show();
                        }
                        break;
                    case (int)JobFields.ReceiverName:
                        job.Receiver.Name = field.Text;
                        break;
                    case (int)JobFields.Price:
                        decimal price = 0;
                        if (decimal.TryParse(field.Text, out price))
                            job.Price = price;
                        break;
                }
            }
        }

        [Export("tableView:numberOfRowsInSection:"), Preserve(Conditional = true)]
        public nint RowsInSection(UITableView tableView, nint section)
        {
            return 11;
        }
    }
}

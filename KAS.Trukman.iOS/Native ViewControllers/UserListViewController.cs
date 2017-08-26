using System;
using Foundation;
using UIKit;
using System.Collections.Generic;
using KAS.Trukman.Storage.ParseClasses;
using Parse;
using MBProgressHUD;
using System.Threading.Tasks;
using System.Linq;
using KAS.Trukman.Storage;
using KAS.Trukman.AppContext;
using KAS.Trukman.Data.Classes;
using KAS.Trukman.Extensions;

namespace KAS.Trukman.iOS
{
    public enum UserListDataSource
    {
        Driver,
        Broker
    };

    public partial class UserListViewController : UIViewController, IUITableViewDataSource, IUITableViewDelegate
    {
        public UserListDataSource dataSourceType = UserListDataSource.Driver;
        public User[] dataSource = null;
        //		public ParseJob job = null;
        //		public ParseCompany company = null;
        public Trip job = null;
        public Company company = null;

        public UserListViewController() : base("UserListViewController", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            if (dataSourceType == UserListDataSource.Broker)
            {
                this.Title = "Brokers";
            }
            else
            {
                this.Title = "Drivers";
            }

            var hud = new MTMBProgressHUD(View)
            {
                LabelText = "Fetching...",
                RemoveFromSuperViewOnHide = true
            };
            hud.Show(true);

            View.AddSubview(hud);
            Task.Run(async () =>
            {
                //if (company == null)
                //{
                //    company = await TrukmanContext.FetchParseCompany(TrukmanContext.Company.Name);
                //}

                //if (company != null)
                //{
                    if (dataSourceType == UserListDataSource.Broker)
                    {
                        dataSource = await TrukmanContext.SelectBrockersAsync(); // GetBrokersFromCompany(company);
                    }
                    else
                    {
                        dataSource = await TrukmanContext.SelectDriversAsync(); // GetDriversFromCompany(company);
                    }
                //}
                this.InvokeOnMainThread(() =>
                {
                    hud.Hide(true);
                    tableView.ReloadData();
                });
            }).LogExceptions("UserListViewController ViewDidLoad");

            tableView.WeakDataSource = this;

            tableView.WeakDelegate = this;
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        [Export("tableView:cellForRowAtIndexPath:"), Preserve(Conditional = true)]
        public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = tableView.DequeueReusableCell("cell");
            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Default, "cell");
            }

            var user = dataSource[indexPath.Row];
            cell.TextLabel.Text = user.UserName;
            return cell;
        }

        [Export("tableView:numberOfRowsInSection:"), Preserve(Conditional = true)]
        public nint RowsInSection(UITableView tableView, nint section)
        {
            if (dataSource == null)
            {
                return 0;
            }
            else
            {
                return dataSource.Count();
            }
        }

        [Export("tableView:didSelectRowAtIndexPath:"), Preserve(Conditional = true)]
        public void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            tableView.DeselectRow(indexPath, true);
            var user = dataSource[indexPath.Row];
            if (dataSourceType == UserListDataSource.Broker)
            {
                job.Broker = user;
            }
            else
            {
                job.Driver = user;
            }

            this.NavigationController.PopViewController(true);
        }

    }
}



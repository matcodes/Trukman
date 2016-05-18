using System;

using UIKit;
using KAS.Trukman.Data.Maps;
using KAS.Trukman.Languages;

namespace KAS.Trukman.iOS
{
	#region ContractorInfoViewController
	public partial class ContractorInfoViewController : UIViewController
	{
		public ContractorInfoViewController () 
			: base ("ContractorInfoViewController", null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
			contractorTypeLabel.Text = this.ContractorTypeText;


			nameLabel.Text = AppLanguages.CurrentLanguage.ContractorPageNameLabel;
			phoneLabel.Text = AppLanguages.CurrentLanguage.ContractorPagePhoneLabel;
			faxLabel.Text = AppLanguages.CurrentLanguage.ContractorPageFaxLabel;
			addressLabel.Text = AppLanguages.CurrentLanguage.ContractorPageAddressLabel;

			nameValueLabel.Text = (this.ContractorInfo != null ? this.ContractorInfo.Contractor.Name : "");
			phoneValueLabel.Text = (this.ContractorInfo != null ? this.ContractorInfo.Contractor.Phone : "");
			faxValueLabel.Text = (this.ContractorInfo != null ? this.ContractorInfo.Contractor.Fax : "");
			addressValueLabel.Text = (this.ContractorInfo != null ? this.ContractorInfo.Contractor.Address : "");
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		public AddressInfo ContractorInfo { get; set; }

		public string ContractorTypeText { get; set;}
	}
	#endregion
}



// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace KAS.Trukman.iOS
{
	[Register ("ContractorInfoViewController")]
	partial class ContractorInfoViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel contractorAddressLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel contractorFaxLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel contractorFaxValueLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel contractorNameLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel contractorNameValueLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel contractorPhoneLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel contractorPhoneValueLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel contractorTypeLabel { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (contractorAddressLabel != null) {
				contractorAddressLabel.Dispose ();
				contractorAddressLabel = null;
			}
			if (contractorFaxLabel != null) {
				contractorFaxLabel.Dispose ();
				contractorFaxLabel = null;
			}
			if (contractorFaxValueLabel != null) {
				contractorFaxValueLabel.Dispose ();
				contractorFaxValueLabel = null;
			}
			if (contractorNameLabel != null) {
				contractorNameLabel.Dispose ();
				contractorNameLabel = null;
			}
			if (contractorNameValueLabel != null) {
				contractorNameValueLabel.Dispose ();
				contractorNameValueLabel = null;
			}
			if (contractorPhoneLabel != null) {
				contractorPhoneLabel.Dispose ();
				contractorPhoneLabel = null;
			}
			if (contractorPhoneValueLabel != null) {
				contractorPhoneValueLabel.Dispose ();
				contractorPhoneValueLabel = null;
			}
			if (contractorTypeLabel != null) {
				contractorTypeLabel.Dispose ();
				contractorTypeLabel = null;
			}
		}
	}
}

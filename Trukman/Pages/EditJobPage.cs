using System;

using Xamarin.Forms;

namespace Trukman
{
	public class EditJobPage : BasePage
	{
		TrukmanEditor edtName;
		TrukmanEditor edtDescription;
		TrukmanEditor edtShipperAddress;
		TrukmanEditor edtReceiveAddress;
		TrukmanEditor edtDriver;
		Button btnOk;
		Button btnCancel;

		public EditJobPage ()
		{
			Label lblTitle = new Label {
				HorizontalTextAlignment = TextAlignment.Center,
				Text = "TRUKMAN",
				FontSize = 33
			};

			edtName = new TrukmanEditor {
				Placeholder = Localization.getString(Localization.LocalStrings.JOB_NAME)
			};
			edtDescription = new TrukmanEditor {
				Placeholder = Localization.getString(Localization.LocalStrings.JOB_DESCRIPTION)
			};
			edtShipperAddress = new TrukmanEditor {
				Placeholder = Localization.getString(Localization.LocalStrings.SHIPPER_ADDRESS)
			};
			edtReceiveAddress = new TrukmanEditor {
				Placeholder = Localization.getString(Localization.LocalStrings.RECEIVE_ADDRESS)
			};
			edtDriver = new TrukmanEditor {
				Placeholder = Localization.getString(Localization.LocalStrings.DRIVER_NAME)
			};

			btnOk = new TrukmanButton{ Text = Localization.getString (Localization.LocalStrings.BTN_SAVE) };
			btnCancel = new TrukmanButton{ Text = Localization.getString (Localization.LocalStrings.BTN_CANCEL) };

			btnOk.Clicked += OkButton_Clicked;
			btnCancel.Clicked += BtnCancel_Clicked;

			var layout = new StackLayout () {
				VerticalOptions = LayoutOptions.Center,
				Spacing = Constants.StackLayoutDefaultSpacing,
				Padding = new Thickness (Constants.ViewsPadding),
				Children = { 
					lblTitle,
					edtName,
					edtDescription,
					edtShipperAddress,
					edtReceiveAddress,
					edtDriver,
					btnOk,
					btnCancel
				}
			};

			Content = layout;
		}

		async void BtnCancel_Clicked (object sender, EventArgs e)
		{
			await this.Navigation.PopModalAsync ();
		}

		async void OkButton_Clicked (object sender, EventArgs e)
		{
			await App.ServerManager.SaveJob (edtName.Text, edtDescription.Text, edtShipperAddress.Text, edtReceiveAddress.Text, edtDriver.Text);
			await this.Navigation.PopModalAsync ();
		}
	}
}



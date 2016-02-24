using System;

using Xamarin.Forms;

namespace Trukman
{
	public class EditJob : BasePage
	{
		TrukmanEditor edtName;
		TrukmanEditor edtDescription;
		TrukmanEditor edtShipperAddress;
		TrukmanEditor edtReceiveAddress;
		TrukmanEditor edtDriver;
		Button btnOk;
		Button btnCancel;

		public EditJob ()
		{
			Label lblTitle = new Label {
				HorizontalTextAlignment = TextAlignment.Center,
				Text = "TRUKMAN",
				FontSize = 33
			};

			edtName = new TrukmanEditor {
				Placeholder = "Job Name"//Localization.getString(Localization.LocalStrings.COMPANY_NAME)
			};
			edtDescription = new TrukmanEditor {
				Placeholder = "Description"//Localization.getString(Localization.LocalStrings.COMPANY_NAME)
			};
			edtShipperAddress = new TrukmanEditor {
				Placeholder = "Shipper address"//Localization.getString(Localization.LocalStrings.COMPANY_NAME)
			};
			edtReceiveAddress = new TrukmanEditor {
				Placeholder = "Receive address"	
			};
			edtDriver = new TrukmanEditor {
				Placeholder = "Driver"	
			};

			// TODO: для отладки, удалить код
			edtName.Text = "First company job";
			edtDescription.Text = "Job description";
			edtShipperAddress.Text = "Address 1";
			edtReceiveAddress.Text = "Address 2";
			edtDriver.Text = "Alex A Driver";

			// driver
			// map

			btnOk = new TrukmanButton{ Text = "Save" };
			btnCancel = new TrukmanButton{ Text="Cancel" };

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



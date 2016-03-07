using System;
using Xamarin.Forms;

namespace Trukman
{
	public class SignUpCompanyPage : BasePage
	{
		TrukmanEditor edtCompName;
		TrukmanEditor edtCompAddress;
		Label lblWelcome;

		public SignUpCompanyPage ()
		{
			NavigationPage.SetHasNavigationBar (this, false);

			Label lblTitle = new Label {
				HorizontalTextAlignment = TextAlignment.Center,
				Text = "TRUKMAN",
				FontSize = 33
			};

			lblWelcome = new Label {
				HorizontalTextAlignment = TextAlignment.Start,
				VerticalTextAlignment = TextAlignment.Center,
				Text = Localization.getString(Localization.LocalStrings.WELCOME),
				FontSize = 14,
				HeightRequest = 60,
			};

			edtCompName = new TrukmanEditor {
				Placeholder = Localization.getString(Localization.LocalStrings.COMPANY_NAME)
			};
			edtCompAddress = new TrukmanEditor {
				Placeholder = Localization.getString(Localization.LocalStrings.COMPANY_ADRESS)
			};

			TrukmanButton btnProceed = new TrukmanButton {
				Text = Localization.getString(Localization.LocalStrings.PROCEED_TO_FLEET_SIZE)
			};
			btnProceed.Clicked += proceedPressed;

			// TODO: для тестирования, потом убрать
			//edtCompName.Text = "DKG Company";

			Content = new StackLayout {
				VerticalOptions = LayoutOptions.CenterAndExpand,
				Spacing = Constants.StackLayoutDefaultSpacing,
				Padding = new Thickness (Constants.ViewsPadding),
				Children = {
					lblTitle,
					lblWelcome,
					edtCompName,
					edtCompAddress,
					new BoxView {
						HeightRequest = 30
					},
					btnProceed
				}
			};
		}

		async void proceedPressed (object sender, EventArgs e) {
			await App.ServerManager.AddCompany (edtCompName.Text);
			await Navigation.PushAsync (new SignUpFleetSizePage ());
		}
	}
}


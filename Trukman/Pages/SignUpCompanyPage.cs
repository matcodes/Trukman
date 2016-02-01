using System;
using Xamarin.Forms;

namespace Trukman
{
	public class SignUpCompanyPage : BasePage
	{
		TrukmanEditor edtCompName;
		TrukmanEditor edtCompAddress;

		public SignUpCompanyPage ()
		{
			NavigationPage.SetHasNavigationBar (this, false);

			Label lblTitle = new Label {
				HorizontalTextAlignment = TextAlignment.Center,
				Text = "TRUKMAN",
				FontSize = 33
			};

			Label lblWelcome = new Label {
				HorizontalTextAlignment = TextAlignment.Start,
				VerticalTextAlignment = TextAlignment.Center,
				Text = "Welcome to Trukman. Let's get you set-up",
				FontSize = 14,
				HeightRequest = 60,
			};

			edtCompName = new TrukmanEditor {
				Text = "COMPANY NAME"
			};
			edtCompAddress = new TrukmanEditor {
				Text = "COMPANY ADDRESS"
			};

			TrukmanButton btnProceed = new TrukmanButton {
				Text = "PROCEED TO FLEET SIZE"
			};
			btnProceed.Clicked += proceedPressed;

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
			await Navigation.PushAsync (new SignUpFleetSizePage ());
			App.ServerManager.AddCompany (edtCompName.Text);
		}
	}
}


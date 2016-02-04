using System;
using Xamarin.Forms;

namespace Trukman
{
	public class SignUpDriverPage : BasePage
	{
		TrukmanEditor edtName;
		TrukmanEditor edtPhone;
		TrukmanEditor edtCompany;

		public UserRole userRole;

		public SignUpDriverPage ()
		{
			NavigationPage.SetHasNavigationBar (this, false);


			Label lblTitle = new Label {
				HorizontalTextAlignment = TextAlignment.Center,
				Text = "TRUKMAN",
				FontSize = 33
			};

			TrukmanButton btnSend = new TrukmanButton {
				Text = "SEND"
			};

			btnSend.Clicked += sendButtonPressed;

			edtName = new TrukmanEditor {
				Placeholder = "FULL NAME"
			};

			edtPhone = new TrukmanEditor {
				Placeholder = "PHONE"
			};

			edtCompany = new TrukmanEditor {
				Placeholder = "COMPANY YOU WORK FOR"
			};

			Content = new StackLayout {
				VerticalOptions = LayoutOptions.Center,
				Spacing = Constants.StackLayoutDefaultSpacing,
				Padding = new Thickness (Constants.ViewsPadding),
				Children = {
					lblTitle,
					new BoxView {
						HeightRequest = 60
					},
					edtName,
					edtPhone,
					new BoxView {
						HeightRequest = 60
					},
					edtCompany,
					new BoxView {
						HeightRequest = 30
					},
					btnSend
				}
			};
		}

		async void sendButtonPressed (object sender, EventArgs e) {
			await App.ServerManager.Register (edtName.Text, edtPhone.Text, userRole);
			await App.ServerManager.RequestToJoinCompany (edtCompany.Text);
		}
	}
}


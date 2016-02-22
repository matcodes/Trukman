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

		// TODO: убрать из конструктора юзера
		public SignUpDriverPage (UserRole _userRole)
		{
			userRole = _userRole;
			NavigationPage.SetHasNavigationBar (this, false);


			Label lblTitle = new Label {
				HorizontalTextAlignment = TextAlignment.Center,
				Text = "TRUKMAN",
				FontSize = 33
			};

			TrukmanButton btnSend = new TrukmanButton {
				Text = Localization.getString(Localization.LocalStrings.SEND)
			};

			btnSend.Clicked += sendButtonPressed;

			edtName = new TrukmanEditor {
				Placeholder = Localization.getString(Localization.LocalStrings.FULL_NAME)
			};

			edtPhone = new TrukmanEditor {
				Placeholder = Localization.getString(Localization.LocalStrings.PHONE)
			};

			edtCompany = new TrukmanEditor {
				Placeholder = Localization.getString(Localization.LocalStrings.COMPANY_YOU_WORK_FOR)
			};

			// TODO: для тестирования, убрать потом
			if (userRole == UserRole.UserRoleDispatch) {
				edtName.Text = "dsp1";
				edtPhone.Text = "123";
				edtCompany.Text = "DKG Company";
			} else if (userRole == UserRole.UserRoleDriver) {
				edtName.Text = "Alex A Driver";
				edtPhone.Text = "123";
				edtCompany.Text = "DKG Company";
			}

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
			//await App.ServerManager.LogIn (edtName.Text, edtPhone.Text);
			await App.ServerManager.RequestToJoinCompany (edtCompany.Text);
			await Navigation.PushModalAsync (new RootPage());
		}
			
	}
}


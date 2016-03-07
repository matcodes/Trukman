using System;
using Xamarin.Forms;

namespace Trukman
{
	public class SignUpDriverPage : BasePage
	{
		TrukmanEditor edtName;
		TrukmanEditor edtPhone;
		TrukmanEditor edtCompany;

		public SignUpDriverPage ()
		{
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
			/*edtName.Text = "Alex A";
			edtPhone.Text = "123";
			edtCompany.Text = "company1";*/

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
			try{
				await App.ServerManager.Register (edtName.Text, edtPhone.Text, UserRole.UserRoleDriver);
				bool isJoinCompany = await App.ServerManager.RequestToJoinCompany (edtCompany.Text);
				if (isJoinCompany)
					await Navigation.PushModalAsync (new RootPage ());
			}
			catch(Exception exc) {
				await AlertHandler.ShowAlert (exc.Message);
			}
		}
			
	}
}


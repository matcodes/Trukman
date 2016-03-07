using System;

using Xamarin.Forms;

namespace Trukman
{
	public class SignUpDispatchPage : BasePage
	{
		TrukmanEditor edtName;
		TrukmanEditor edtPhone;
		TrukmanEditor edtCompany;

		public SignUpDispatchPage ()
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
			/*edtName.Text = "dsp1";
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
				await App.ServerManager.Register (edtName.Text, edtPhone.Text, UserRole.UserRoleDispatch);
				bool isJoinToCompany = await App.ServerManager.RequestToJoinCompany (edtCompany.Text);
				if (isJoinToCompany)
					await Navigation.PushModalAsync (new RootPage ());
			}
			catch(Exception exc) {
				await AlertHandler.ShowAlert (exc.Message);
			}
		}
	}
}


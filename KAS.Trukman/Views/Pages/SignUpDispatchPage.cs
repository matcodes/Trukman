using System;

using Xamarin.Forms;
using KAS.Trukman;

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
				/*bool findCompany = await App.ServerManager.FindCompany(edtCompany.Text);
				if (!findCompany)
					await AlertHandler.ShowCheckCompany (edtCompany.Text);
				else
				{
					await App.ServerManager.Register (edtName.Text, edtPhone.Text, UserRole.UserRoleDispatch, null, null);
					SettingsServiceHelper.SaveCompany(edtCompany.Text);
					bool isJoinToCompany = await App.ServerManager.RequestToJoinCompany (edtCompany.Text);
					if (isJoinToCompany)
						await Navigation.PushAsync (new RootPage ());
					else
					{
						await AlertHandler.ShowAlert (string.Format ("The owner of the company {0} has not yet added you to the company", edtCompany.Text));
						await App.ServerManager.LogOut();
					}
				}*/
			}
			catch(Exception exc) {
				//AlertHandler.ShowAlert (exc.Message);
			}
		}
	}
}


using System;
using Xamarin.Forms;

namespace Trukman
{
	public class SignUpDriverPage : BasePage
	{
		Label lblSignup;
		Label lblUserRole;
		TrukmanEditor edtFirstName;
		TrukmanEditor edtLastName;
		TrukmanEditor edtCompany;
		TrukmanButton btnSubmit;
		Label lblHaveAccount;

		public SignUpDriverPage ()
		{
			NavigationPage.SetHasNavigationBar (this, false);

			//Image menuImage = new Image{ Source = ImageSource.FromResource ("hamburger.png"), Aspect = Aspect.AspectFit };

			TrukmanButton btnEng = new TrukmanButton {
				//wid
				Text = "ENG",
				Tag = 1,
				//BackgroundColor = Color.Transparent,
				TextColor = Color.FromHex ("E3EBEB"),
				FontSize = 8
			};
			TrukmanButton btnEsp = new TrukmanButton {
				Text = "ESP",
				Tag = 2,
				//BackgroundColor = Color.Transparent,
				TextColor = Color.FromHex ("FF8C8C"),
				FontSize = 8
			};
			btnEng.Clicked += btnLan_Clicked;;
			btnEsp.Clicked += btnLan_Clicked;

			lblSignup = new Label {
				HorizontalTextAlignment = TextAlignment.Center,
				//Text = ,
				FontSize = 33
			};

			lblUserRole = new Label { HorizontalTextAlignment = TextAlignment.Center, FontSize = 16 };

			Image logo = new Image{ Source = ImageSource.FromResource ("logo.png"), Aspect = Aspect.AspectFit };

			edtFirstName = new TrukmanEditor ();
			edtLastName = new TrukmanEditor ();
			edtCompany = new TrukmanEditor ();

			btnSubmit = new TrukmanButton ();
			btnSubmit.Clicked += btnSubmit_Clicked;

			lblHaveAccount = new Label { HorizontalOptions = LayoutOptions.Center };

			/*TrukmanButton btnSend = new TrukmanButton {
				Text = Localization.getString(Localization.LocalStrings.SEND)
			};

			btnSend.Clicked += sendButtonPressed;

			edtPhone = new TrukmanEditor {
				Placeholder = Localization.getString(Localization.LocalStrings.PHONE)
			};*/

			StackLayout stackLayout = new StackLayout {
				Spacing = Constants.StackLayoutDefaultSpacing,
				Padding = new Thickness(Constants.ViewsPadding),
				VerticalOptions = LayoutOptions.CenterAndExpand,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Children = {
					edtFirstName,
					edtLastName,
					edtCompany, 
					btnSubmit,
					new BoxView { HeightRequest = 20 },
					lblHaveAccount
				}
			};

			RelativeLayout relativeLayout = new RelativeLayout ();

			/*relativeLayout.Children.Add (menuImage, 
				Constraint.RelativeToParent (parent => Constants.ViewsBottomPadding),
				Constraint.RelativeToParent (parent => Constants.ViewsBottomPadding)
			);*/
			relativeLayout.Children.Add(lblSignup, 
				Constraint.RelativeToParent(parent => parent.Width / 2 - lblSignup.Width / 2),
				Constraint.RelativeToParent(parent => Constants.ViewsBottomPadding)
			);
			relativeLayout.Children.Add (btnEsp,
				Constraint.RelativeToParent(parent => parent.Width - btnEsp.Width - Constants.ViewsBottomPadding),
				Constraint.RelativeToParent(parent => Constants.ViewsBottomPadding)					
			);
			relativeLayout.Children.Add (btnEng, 
				Constraint.RelativeToView(btnEsp, (parent, view) => parent.Width - view.Width - btnEng.Width - Constants.ViewsBottomPadding),
				Constraint.RelativeToParent(parent => Constants.ViewsBottomPadding)
			);
			relativeLayout.Children.Add (lblUserRole,
				Constraint.RelativeToParent (parent => parent.Width / 2 - lblUserRole.Width / 2),
				Constraint.RelativeToView (lblSignup, (parent, view) => view.Y + view.Height + Constants.ViewsBottomPadding)
			);
			relativeLayout.Children.Add(logo,
				Constraint.RelativeToParent(parent => parent.Width / 2 - logo.Width / 2),
				Constraint.RelativeToView(lblUserRole, (parent, view) => view.Y + view.Height + Constants.ViewsBottomPadding)
			);
			relativeLayout.Children.Add (stackLayout,
				Constraint.RelativeToParent (parent => parent.Width / 2 - stackLayout.Width / 2),
				//Constraint.RelativeToParent (parent => parent.Height / 2 - stackLayout.Height / 2),
				Constraint.RelativeToView (logo, (parent, view) => view.Y + view.Height + Constants.ViewsBottomPadding),
				Constraint.RelativeToParent (parent => parent.Width)
			);

			Icon = "hamburger.png";
			Content = relativeLayout;
				
			/*new StackLayout {
				VerticalOptions = LayoutOptions.Center,
				Spacing = Constants.StackLayoutDefaultSpacing,
				Padding = new Thickness (Constants.ViewsPadding),
				Children = {
					lblSignup,
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
			};*/
			UpdateText ();
		}

		void btnLan_Clicked (object sender, EventArgs e)
		{
			if (((TrukmanButton)sender).Tag == 1)
				Localization.language = Localization.Languages.ENGLISH;
			else if (((TrukmanButton)sender).Tag == 2)
				Localization.language = Localization.Languages.ESPANIOL;

			UpdateText ();
		}

		void UpdateText()
		{
			lblSignup.Text = Localization.getString (Localization.LocalStrings.SIGN_UP).ToUpper();
			lblUserRole.Text = Localization.getString (Localization.LocalStrings.DRIVER).ToUpper();
			edtFirstName.Placeholder = Localization.getString (Localization.LocalStrings.FIRST_NAME);
			edtLastName.Placeholder = Localization.getString (Localization.LocalStrings.LAST_NAME);
			edtCompany.Placeholder = Localization.getString (Localization.LocalStrings.COMPANY_YOU_WORK_FOR);
			btnSubmit.Text = Localization.getString (Localization.LocalStrings.SUBMIT);
			lblHaveAccount.Text = Localization.getString (Localization.LocalStrings.HAVE_ACCOUNT_QUESTION);
		}

		async void btnSubmit_Clicked (object sender, EventArgs e) {
			try {
				/*bool findCompany = await App.ServerManager.FindCompany (edtCompany.Text);
				if (!findCompany)
					await AlertHandler.ShowCheckCompany (edtCompany.Text);
				else {
					await App.ServerManager.Register (edtName.Text, edtPhone.Text, UserRole.UserRoleDriver);
					bool isJoinCompany = await App.ServerManager.RequestToJoinCompany (edtCompany.Text);
					if (isJoinCompany)
						await Navigation.PushModalAsync (new RootPage ());
					else{
						await AlertHandler.ShowAlert (string.Format ("The owner of the company {0} has not yet added you to the company", edtCompany.Text));
						await App.ServerManager.LogOut();
					}
				}*/
			} catch (Exception exc) {
				await AlertHandler.ShowAlert (exc.Message);
			}
		}			
	}
}


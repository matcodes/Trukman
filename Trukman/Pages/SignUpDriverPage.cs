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
		Button btnEng;
		Button btnEsp;

		public SignUpDriverPage ()
		{
			NavigationPage.SetHasNavigationBar (this, false);

			Image backgroundImage = new Image{ Source = ImageSource.FromResource ("background.png"), Aspect = Aspect.Fill };
			Image hamburgerImage = new Image{ Source = ImageSource.FromResource ("hamburger.png"), Aspect = Aspect.Fill };
			Image logoImage = new Image{ Source = ImageSource.FromResource ("logo.png"), Aspect = Aspect.AspectFit };

			btnEng = new Button {
				Text = "ENG",
				TextColor = Color.FromHex ("E3EBEB"), 
				BackgroundColor = Color.Transparent,
				FontSize = 12,
			};
			btnEng.Text = "ENG";
			btnEsp = new Button {
				Text = "ESP",
				TextColor = Color.FromHex ("FF8F8E"), 
				BackgroundColor = Color.Transparent,
				FontSize = 12
			};
			btnEng.Clicked += btnLan_Clicked;
			btnEsp.Clicked += btnLan_Clicked;

			lblSignup = new Label {
				HorizontalTextAlignment = TextAlignment.Center,
				TextColor = Color.FromHex ("F5FFFF"),
				FontSize = 33
			};

			lblUserRole = new Label {
				HorizontalTextAlignment = TextAlignment.Center,
				FontSize = 18,
				TextColor = Color.FromHex ("FF8F8E")
			};

			var btnFirstName = new Button { Style = (Style)App.Current.Resources ["buttonForEntryRadiusStyle"] };
			edtFirstName = new TrukmanEditor { Style = (Style)App.Current.Resources ["entryRadiusStyle"] };
			var btnLastName = new Button { Style = (Style)App.Current.Resources ["buttonForEntryRadiusStyle"] };
			edtLastName = new TrukmanEditor { Style = (Style)App.Current.Resources ["entryRadiusStyle"] };
			var btnCompany = new Button{ Style = (Style)App.Current.Resources ["buttonForEntryRadiusStyle"] };
			edtCompany = new TrukmanEditor { Style = (Style)App.Current.Resources ["entryRadiusStyle"] };

			RelativeLayout userInfoLayout = new RelativeLayout ();
			userInfoLayout.HorizontalOptions = LayoutOptions.FillAndExpand;
			userInfoLayout.Children.Add (btnFirstName, 
				Constraint.RelativeToParent (parent => 0),
				Constraint.RelativeToParent (parent => 0),
				Constraint.RelativeToParent (parent => parent.Width)
			);
			userInfoLayout.Children.Add (edtFirstName, 
				Constraint.RelativeToView (btnFirstName, (parent, View) => View.X + Constants.ViewsPadding / 2),
				Constraint.RelativeToView (btnFirstName, (parent, View) => View.Y),
				Constraint.RelativeToView (btnFirstName, (parent, View) => View.Width - Constants.ViewsPadding),
				Constraint.RelativeToView (btnFirstName, (parent, View) => View.Height)
			);				
			userInfoLayout.Children.Add (btnLastName, 
				Constraint.RelativeToParent (parent => 0),
				Constraint.RelativeToView(btnFirstName, (parent, View) => View.Y + View.Height + Constants.ViewsBottomPadding),
				Constraint.RelativeToParent (parent => parent.Width)
			);
			userInfoLayout.Children.Add (edtLastName, 
				Constraint.RelativeToView (btnLastName, (parent, View) => View.X + Constants.ViewsPadding / 2),
				Constraint.RelativeToView (btnLastName, (parent, View) => View.Y),
				Constraint.RelativeToView (btnLastName, (parent, View) => View.Width - Constants.ViewsPadding),
				Constraint.RelativeToView (btnLastName, (parent, View) => View.Height)
			);				
			userInfoLayout.Children.Add (btnCompany, 
				Constraint.RelativeToParent (parent => 0),
				Constraint.RelativeToView(btnLastName, (parent, View) => View.Y + View.Height + Constants.ViewsBottomPadding),
				Constraint.RelativeToParent (parent => parent.Width)
			);
			userInfoLayout.Children.Add (edtCompany, 
				Constraint.RelativeToView (btnCompany, (parent, View) => View.X + Constants.ViewsPadding / 2),
				Constraint.RelativeToView (btnCompany, (parent, View) => View.Y),
				Constraint.RelativeToView (btnCompany, (parent, View) => View.Width - Constants.ViewsPadding),
				Constraint.RelativeToView (btnCompany, (parent, View) => View.Height)
			);				

			btnSubmit = new TrukmanButton ();
			btnSubmit.Clicked += btnSubmit_Clicked;

			lblHaveAccount = new Label { HorizontalOptions = LayoutOptions.Center };

			StackLayout stackLayout = new StackLayout {
				Spacing = Constants.StackLayoutDefaultSpacing,
				Padding = new Thickness(Constants.ViewsPadding),
				VerticalOptions = LayoutOptions.CenterAndExpand,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Children = {
					userInfoLayout,
					btnSubmit,
					new BoxView { HeightRequest = 10 },
					lblHaveAccount
				}
			};

			RelativeLayout relativeLayout = new RelativeLayout ();

			relativeLayout.Children.Add (backgroundImage, 
				Constraint.RelativeToParent (parent => 0),
				Constraint.RelativeToParent (parent => 0),
				Constraint.RelativeToParent (parent => parent.Width),
				Constraint.RelativeToParent (parent => parent.Height)
			);
			relativeLayout.Children.Add (lblSignup, 
				Constraint.RelativeToParent (parent => parent.Width / 2 - lblSignup.Width / 2),
				Constraint.RelativeToParent (parent => Constants.ViewsBottomPadding)
			);
			relativeLayout.Children.Add (hamburgerImage, 
				Constraint.RelativeToParent (parent => Constants.ViewsBottomPadding),
				Constraint.RelativeToParent (parent => Constants.ViewsBottomPadding),
				Constraint.RelativeToView (lblSignup, (parent, lblSignup) => lblSignup.Height / 2),
				Constraint.RelativeToView (lblSignup, (parent, lblSignup) => lblSignup.Height / 2)
			);
			relativeLayout.Children.Add (btnEsp,
				Constraint.RelativeToParent (parent => parent.Width - btnEsp.Width),
				Constraint.RelativeToParent (parent => 0),
				Constraint.RelativeToParent (parent => 50)
			);
			relativeLayout.Children.Add (btnEng, 
				Constraint.RelativeToView (btnEsp, (parent, view) => parent.Width - view.Width - btnEng.Width),
				Constraint.RelativeToParent (parent => 0),
				Constraint.RelativeToParent (parent => 50)
			);
			relativeLayout.Children.Add (lblUserRole,
				Constraint.RelativeToParent (parent => 0), //parent.Width / 2 - lblUserRole.Width / 2),
				Constraint.RelativeToView (lblSignup, (parent, view) => view.Y + view.Height + Constants.ViewsBottomPadding),
				Constraint.RelativeToParent(Parent => Parent.Width)
			);
			relativeLayout.Children.Add (logoImage,
				Constraint.RelativeToParent (parent => parent.Width / 2 - logoImage.Width / 2),
				Constraint.RelativeToView (lblUserRole, (parent, view) => view.Y + view.Height + Constants.ViewsBottomPadding)
			);
			relativeLayout.Children.Add (stackLayout,
				Constraint.RelativeToParent (parent => parent.Width / 2 - stackLayout.Width / 2),
				//Constraint.RelativeToParent (parent => parent.Height / 2 - stackLayout.Height / 2),
				Constraint.RelativeToView (logoImage, (parent, view) => view.Y + view.Height + Constants.ViewsBottomPadding),
				Constraint.RelativeToParent (parent => parent.Width)
			);

			Content = relativeLayout;

			UpdateText ();
		}

		void btnLan_Clicked (object sender, EventArgs e)
		{
			if ((Button)sender == btnEng) {
				Localization.language = Localization.Languages.ENGLISH;
				btnEng.TextColor = Color.FromHex ("E3EBEB");
				btnEsp.TextColor = Color.FromHex ("FF8F8E");
			} else if ((Button)sender == btnEsp) {
				Localization.language = Localization.Languages.ESPANIOL;
				btnEng.TextColor = Color.FromHex ("FF8F8E");
				btnEsp.TextColor = Color.FromHex ("E3EBEB");;
			}

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
				string username = string.Format('{0} {1}', edtFirstName, edtLastName);
				await App.ServerManager.Register (username, edtPhone.Text, UserRole.UserRoleDriver);
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


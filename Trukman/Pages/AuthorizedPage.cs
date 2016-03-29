using System;

using Xamarin.Forms;

namespace Trukman
{
	public class AuthorizedPage : BasePage
	{
		Label lblSignup;
		Label lblUserRole;
		Button btnEng;
		Button btnEsp;
		TrukmanButton btnContinue;
		Label lblAuthorized;

		public AuthorizedPage ()
		{
			NavigationPage.SetHasNavigationBar (this, false);

			Image backgroundImage = new Image{ Source = ImageSource.FromResource ("background.png"), Aspect = Aspect.Fill };
			Image hamburgerImage = new Image{ Source = ImageSource.FromResource ("hamburger.png"), Aspect = Aspect.Fill };
			Image authorizedImage = new Image{ Source = ImageSource.FromResource ("authorized.png"), Aspect = Aspect.Fill };

			lblSignup = new Label {
				HorizontalTextAlignment = TextAlignment.Center,
				TextColor = Color.FromHex (Constants.SignUpFontColor),
				FontSize = 33
			};

			lblUserRole = new Label {
				HorizontalTextAlignment = TextAlignment.Center,
				FontSize = 18,
				TextColor = Color.FromHex (Constants.RegularFontColor)
			};

			btnEng = new Button {
				Text = "ENG",
				TextColor = Color.FromHex (Constants.SelectedFontColor), 
				BackgroundColor = Color.Transparent,
				FontSize = 12,
			};
			btnEng.Text = "ENG";
			btnEsp = new Button {
				Text = "ESP",
				TextColor = Color.FromHex (Constants.RegularFontColor), 
				BackgroundColor = Color.Transparent,
				FontSize = 12
			};
			btnEng.Clicked += btnLan_Clicked;
			btnEsp.Clicked += btnLan_Clicked;

			lblAuthorized= new TrukmanLabel ();
			lblAuthorized.FontSize = 18;

			btnContinue = new TrukmanButton ();
			btnContinue.Clicked += BtnContinue_Clicked;

			var relativeLayout = new RelativeLayout ();

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
			relativeLayout.Children.Add (authorizedImage,
				Constraint.RelativeToParent (parent => parent.Width / 2 - authorizedImage.Width / 2),
				Constraint.RelativeToView (lblUserRole, (parent, view) => view.Y + view.Height + Constants.ViewsPadding)
			);
			relativeLayout.Children.Add (lblAuthorized,
				Constraint.RelativeToParent (parent => parent.Width / 2 - lblAuthorized.Width / 2),
				Constraint.RelativeToView (authorizedImage, (parent, view) => view.Y + view.Height + Constants.ViewsPadding)
			);
			relativeLayout.Children.Add (btnContinue, 
				Constraint.RelativeToParent (parent => parent.Width / 2 - btnContinue.Width / 2),
				Constraint.RelativeToView (lblAuthorized, (parent, view) => view.Y + view.Height + Constants.ViewsBottomPadding)
			);

			Content = relativeLayout;
		
			UpdateText ();
		}

		async void BtnContinue_Clicked (object sender, EventArgs e)
		{
			await Navigation.PushAsync (new RootPage ());
		}

		void btnLan_Clicked (object sender, EventArgs e)
		{
			if ((Button)sender == btnEng) {
				Localization.language = Localization.Languages.ENGLISH;
				btnEng.TextColor = Color.FromHex (Constants.SelectedFontColor);
				btnEsp.TextColor = Color.FromHex (Constants.RegularFontColor);
			} else if ((Button)sender == btnEsp) {
				Localization.language = Localization.Languages.ESPANIOL;
				btnEng.TextColor = Color.FromHex (Constants.RegularFontColor);
				btnEsp.TextColor = Color.FromHex (Constants.SelectedFontColor);;
			}

			UpdateText ();
		}

		void UpdateText()
		{
			lblSignup.Text = Localization.getString (Localization.LocalStrings.SIGN_UP).ToUpper();
			lblUserRole.Text = Localization.getString (Localization.LocalStrings.DRIVER).ToUpper();
			lblAuthorized.Text = string.Format(Localization.getString(Localization.LocalStrings.AUTHORIZED_BY_COMPANY), App.ServerManager.GetCurrentCompanyName());
			btnContinue.Text = Localization.getString (Localization.LocalStrings.CONTINUE);
		}
	}
}



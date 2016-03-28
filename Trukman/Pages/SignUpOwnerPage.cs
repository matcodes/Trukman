using System;

using Xamarin.Forms;

namespace Trukman
{
	public class SignUpOwnerPage : BasePage
	{
        Label lblSignup;

		TrukmanEditor edtMC;
        TrukmanButton btnSubmit;

		public SignUpOwnerPage ()
		{
            NavigationPage.SetHasNavigationBar (this, false);
            Localization.language = Localization.Languages.ENGLISH;

            Image backgroundImage = new Image{ Source = ImageSource.FromResource ("background.png"), Aspect = Aspect.Fill };
            Image hamburgerImage = new Image{ Source = ImageSource.FromResource ("hamburger.png"), Aspect = Aspect.Fill };

            lblSignup = new Label {
				HorizontalTextAlignment = TextAlignment.Center,
                Text = Localization.getString(Localization.LocalStrings.SIGN_UP).ToUpper(),
                TextColor = Color.FromHex (Constants.SignUpFontColor),
				FontSize = 33
			};

            Button btnEditMC = new Button { Style = (Style)App.Current.Resources ["buttonForEntryRadiusStyle"] };
            edtMC = new TrukmanEditor { 
                Style = (Style)App.Current.Resources ["entryRadiusStyle"],
                Placeholder = Localization.getString(Localization.LocalStrings.MC)
            };

			btnSubmit = new TrukmanButton {
				Text = Localization.getString(Localization.LocalStrings.SUBMIT)
			};

            RelativeLayout userInfoLayout = new RelativeLayout ();
            userInfoLayout.HorizontalOptions = LayoutOptions.FillAndExpand;
            userInfoLayout.Children.Add (btnEditMC, 
                Constraint.RelativeToParent (parent => 0),
                Constraint.RelativeToParent (parent => 0),
                Constraint.RelativeToParent (parent => parent.Width)
            );
            userInfoLayout.Children.Add (edtMC, 
                Constraint.RelativeToView (btnEditMC, (parent, View) => View.X + Constants.ViewsPadding / 2),
                Constraint.RelativeToView (btnEditMC, (parent, View) => View.Y),
                Constraint.RelativeToView (btnEditMC, (parent, View) => View.Width - Constants.ViewsPadding),
                Constraint.RelativeToView (btnEditMC, (parent, View) => View.Height)
            );
			btnSubmit.Clicked += buttonClicked;

			// TODO: для тестирования, удалить потом
//			edtName.Text = "DKG";
//			edtMC.Text = "158851";

            StackLayout stackLayout = new StackLayout {
                Spacing = Constants.StackLayoutDefaultSpacing,
                Padding = new Thickness(Constants.ViewsPadding),
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Children = {
                    userInfoLayout,
                    btnSubmit
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
            relativeLayout.Children.Add (stackLayout,
                Constraint.RelativeToParent (parent => parent.Width / 2 - stackLayout.Width / 2),
                Constraint.RelativeToView (lblSignup, (parent, view) => view.Y + view.Height + Constants.ViewsBottomPadding),
                Constraint.RelativeToParent (parent => parent.Width)
            );

            Content = relativeLayout;
		}

		async void buttonClicked (object sender, EventArgs e) {
			try
			{
                await App.ServerManager.Register(edtMC.Text, edtMC.Text, UserRole.UserRoleOwner);

				await Navigation.PushAsync(new SignUpCompanyPage());
			}
			catch(Exception exc) {
				await AlertHandler.ShowAlert (exc.Message);
			}
		}
	}
}



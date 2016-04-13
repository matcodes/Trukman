using System;

using Xamarin.Forms;

namespace Trukman
{
	public class SignUpOwnerPage : BasePage
	{
        Label lblSignup;
        Label lblUserRole;
		TrukmanEditor edtMC;
        TrukmanButton failedMCLabel;
        TrukmanButton btnSubmit;
        Button btnEng;
        Button btnEsp;

        StackLayout stackLayout;
        RelativeLayout failedMCLayout;

        int failedAttempts;
        int maxFailAttempts = 3;

		public SignUpOwnerPage ()
		{
            NavigationPage.SetHasNavigationBar (this, false);
            Localization.language = Localization.Languages.ENGLISH;
            failedAttempts = 0;
            Image backgroundImage = new Image{ Source = ImageSource.FromResource ("background.png"), Aspect = Aspect.Fill };
            Image hamburgerImage = new Image{ Source = ImageSource.FromResource ("hamburger.png"), Aspect = Aspect.Fill };
            Image logoImage = new Image{ Source = ImageSource.FromResource ("logo.png"), Aspect = Aspect.AspectFit };

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
            failedMCLabel = new TrukmanButton {
                Text = "MC# not found",
                FontSize = 18,
                BackgroundColor = Color.Transparent,
                TextColor = Color.White,
                Style = (Style)App.Current.Resources ["buttonTransparentEntry"]
            };

            Button btnEditMC = new Button { Style = (Style)App.Current.Resources ["buttonForEntryRadiusStyle"] };
            edtMC = new TrukmanEditor { 
                Style = (Style)App.Current.Resources ["entryRadiusStyle"]
            };

            btnSubmit = new TrukmanButton();

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

            failedMCLayout = new RelativeLayout();
            failedMCLayout.Children.Add(failedMCLabel,
                Constraint.RelativeToParent (parent => 0),
                Constraint.RelativeToParent (parent => 0),
                Constraint.RelativeToParent (parent => parent.Width)
            );

			btnSubmit.Clicked += buttonClicked;

            stackLayout = new StackLayout {
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
            relativeLayout.Children.Add (logoImage,
                Constraint.RelativeToParent (parent => parent.Width / 2 - logoImage.Width / 2),
                Constraint.RelativeToView (lblSignup, (parent, view) => view.Y + view.Height + Constants.ViewsBottomPadding)
            );
            relativeLayout.Children.Add (lblUserRole,
                Constraint.RelativeToParent (parent => 0),
                Constraint.RelativeToView (logoImage, (parent, view) => view.Y + view.Height + Constants.ViewsBottomPadding),
                Constraint.RelativeToParent(parent => parent.Width)
            );
            relativeLayout.Children.Add (stackLayout,
                Constraint.RelativeToParent (parent => parent.Width / 2 - stackLayout.Width / 2),
                Constraint.RelativeToView (lblUserRole, (parent, view) => view.Y + view.Height),
                Constraint.RelativeToParent (parent => parent.Width)
            );

            Content = relativeLayout;
            UpdateText ();
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
            lblUserRole.Text = Localization.getString(Localization.LocalStrings.OWNER_or_OPERATOR).ToUpper();
            lblSignup.Text = Localization.getString (Localization.LocalStrings.SIGN_UP).ToUpper();
            edtMC.Placeholder = Localization.getString (Localization.LocalStrings.MC);
            btnSubmit.Text = Localization.getString (Localization.LocalStrings.SUBMIT);
        }

		async void buttonClicked (object sender, EventArgs e) {
			try
			{
                MCResponse data = await MCQuery.verifyMC(edtMC.Text);
                if(data.success) {
                    await Navigation.PushAsync(new SignUpCompanyPage(data));
                } else {
                    throw new Exception("Didn't found company with this MC");
                }
			}
			catch(Exception) {
                failedAttempts++;
                if (failedAttempts >= maxFailAttempts)
                {
                    await DisplayAlert("", 
                        Localization.getString(Localization.LocalStrings.FAILED_OWNER_MC), 
                        Localization.getString(Localization.LocalStrings.CONTINUE));
                    await Navigation.PushAsync(new SignUpOwnerPage());
                }
                else if(failedAttempts == 1)
                {
                    // Insert before submit button
                    stackLayout.Children.Insert(1, failedMCLayout);
                }
			}
		}
	}
}



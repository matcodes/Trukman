using System;

using Xamarin.Forms;

namespace Trukman
{
	public class AuthorizationDeclinedPage : BasePage
	{
		Label lblSignup;
		Label lblUserRole;
		TrukmanButton btnContinue;
		Label lblDeclineAuthorize;

		public AuthorizationDeclinedPage ()
		{
			NavigationPage.SetHasNavigationBar (this, false);

			Image leftImage = new Image{ Source = ImageSource.FromFile ("left.png"), Aspect = Aspect.Fill };
			Image authorizedImage = new Image{ Source = ImageSource.FromResource ("authorize.png"), Aspect = Aspect.Fill };
			Image logoImage = new Image{ Source = ImageSource.FromResource ("logo.png"), Aspect = Aspect.AspectFit };

			lblSignup = new Label {
				HorizontalTextAlignment = TextAlignment.Center,
				TextColor = Color.FromHex (Constants.TitleFontColor),
				FontSize = 33
			};

			lblUserRole = new Label {
				HorizontalTextAlignment = TextAlignment.Center,
				FontSize = 18,
				TextColor = Color.FromHex (Constants.RegularFontColor)
			};

			var segmentLan = new SegmentedControl {
				Children = {
					new SegmentedControlOption{ Text = "ENG" },
					new SegmentedControlOption{ Text = "ESP"}
				}
			};
			segmentLan.ValueChanged += SegmentLan_ValueChanged;

			lblDeclineAuthorize = new TrukmanLabel {
				HorizontalTextAlignment = TextAlignment.Center
			};
			//lblDeclineAuthorize.FontSize = 18;

			btnContinue = new TrukmanButton ();
			btnContinue.Clicked += BtnContinue_Clicked;

			var relativeLayout = new RelativeLayout ();

			relativeLayout.Children.Add (lblSignup, 
				Constraint.RelativeToParent (parent => parent.Width / 2 - lblSignup.Width / 2),
				Constraint.RelativeToParent (parent => Constants.ViewsBottomPadding)
			);
			relativeLayout.Children.Add (leftImage, 
				Constraint.RelativeToParent (parent => Constants.ViewsBottomPadding),
				Constraint.RelativeToParent (parent => Constants.ViewsBottomPadding),
				Constraint.RelativeToView (lblSignup, (parent, View) => View.Height / 2),
				Constraint.RelativeToView (lblSignup, (parent, View) => View.Height / 2)
			);
			relativeLayout.Children.Add (segmentLan, 
				Constraint.RelativeToParent (parent => parent.Width - segmentLan.Width),
				Constraint.RelativeToParent (parent => 0)
			);
			relativeLayout.Children.Add (logoImage,
				Constraint.RelativeToParent (parent => parent.Width / 2 - logoImage.Width / 2),
				Constraint.RelativeToView (lblSignup, (parent, view) => view.Y + view.Height + Constants.ViewsBottomPadding)
			);
			relativeLayout.Children.Add (lblUserRole,
				Constraint.RelativeToParent (parent => 0), //parent.Width / 2 - lblUserRole.Width / 2),
				Constraint.RelativeToView (logoImage, (parent, view) => view.Y + view.Height + Constants.ViewsBottomPadding),
				Constraint.RelativeToParent(Parent => Parent.Width)
			);
			relativeLayout.Children.Add (authorizedImage,
				Constraint.RelativeToParent (parent => parent.Width / 2 - authorizedImage.Width / 2),
				Constraint.RelativeToView (lblUserRole, (parent, view) => view.Y + view.Height + Constants.ViewsPadding)
			);
			relativeLayout.Children.Add (lblDeclineAuthorize,
				Constraint.RelativeToParent (parent => parent.Width / 2 - lblDeclineAuthorize.Width / 2),
				Constraint.RelativeToView (authorizedImage, (parent, view) => view.Y + view.Height + Constants.ViewsPadding),
				Constraint.RelativeToParent(parent => parent.Width - Constants.ViewsPadding)
			);
			relativeLayout.Children.Add (btnContinue, 
				Constraint.RelativeToParent (parent => parent.Width / 2 - btnContinue.Width / 2),
				Constraint.RelativeToView (lblDeclineAuthorize, (parent, view) => view.Y + view.Height + Constants.ViewsBottomPadding),
				Constraint.RelativeToParent(parent => parent.Width - Constants.ViewsPadding)
			);

			Content = relativeLayout;

			UpdateText ();
		}

		async void BtnContinue_Clicked (object sender, EventArgs e)
		{
			await Navigation.PopToRootAsync ();
		}

		void SegmentLan_ValueChanged (object sender, EventArgs e)
		{
			if (((SegmentedControl)sender).SelectedValue == ((SegmentedControl)sender).Children [0].Text)
				Localization.language = Localization.Languages.ENGLISH;
			else if (((SegmentedControl)sender).SelectedValue == ((SegmentedControl)sender).Children [1].Text)
				Localization.language = Localization.Languages.ESPANIOL;

			UpdateText ();
		}

		void UpdateText()
		{
			lblSignup.Text = Localization.getString (Localization.LocalStrings.SIGN_UP).ToUpper();
			lblUserRole.Text = Localization.getString (Localization.LocalStrings.DRIVER).ToUpper();
			lblDeclineAuthorize.Text = string.Format (Localization.getString (Localization.LocalStrings.DECLINE_AUTHORIZED_BY_COMPANY), SettingsServiceHelper.GetCompany ());
			btnContinue.Text = Localization.getString (Localization.LocalStrings.CONTINUE);
		}
	}
}



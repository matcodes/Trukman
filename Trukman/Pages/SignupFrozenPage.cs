using System;

using Xamarin.Forms;

namespace Trukman
{
	public class SignupFrozenPage : BasePage
	{
		Label lblSignup;
		Label lblUserRole;
		TrukmanButton btnContinue;
		Label lblDecline;

		public SignupFrozenPage ()
		{
			NavigationPage.SetHasNavigationBar (this, false);

			Image leftImage = new Image{ Source = ImageSource.FromFile ("left.png"), Aspect = Aspect.Fill };
			Image authorizationImage = new Image{ Source = ImageSource.FromResource ("lock-transparent.png"), Aspect = Aspect.Fill };
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

			lblDecline = new TrukmanLabel {
				HorizontalTextAlignment = TextAlignment.Center
			};
			//lblAuthorized.FontSize = 18;

			btnContinue = new TrukmanButton ();
			btnContinue.Clicked += (async (sender, e) => await Navigation.PopToRootAsync ());

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
			relativeLayout.Children.Add (authorizationImage,
				Constraint.RelativeToParent (parent => parent.Width / 2 - authorizationImage.Width / 2),
				Constraint.RelativeToView (lblUserRole, (parent, view) => view.Y + view.Height + Constants.ViewsPadding)
			);
			relativeLayout.Children.Add (lblDecline,
				Constraint.RelativeToParent (parent => parent.Width / 2 - lblDecline.Width / 2),
				Constraint.RelativeToView (authorizationImage, (parent, view) => view.Y + view.Height + Constants.ViewsPadding),
				Constraint.RelativeToParent(parent => parent.Width - Constants.ViewsPadding)
			);
			relativeLayout.Children.Add (btnContinue, 
				Constraint.RelativeToParent (parent => parent.Width / 2 - btnContinue.Width / 2),
				Constraint.RelativeToView (lblDecline, (parent, view) => view.Y + view.Height + Constants.ViewsBottomPadding),
				Constraint.RelativeToParent(parent => parent.Width - Constants.ViewsPadding)
			);

			Content = relativeLayout;

			UpdateText ();
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
			lblDecline.Text = Localization.getString(Localization.LocalStrings.FROZEN_AUTHORIZATION);
			btnContinue.Text = Localization.getString (Localization.LocalStrings.CONTINUE);
		}
	}
}



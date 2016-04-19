using System;

using Xamarin.Forms;
using KAS.Trukman.Helpers;
using KAS.Trukman;

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

			Image leftImage = new Image{ Source = "left.png", Aspect = Aspect.Fill };

			lblSignup = new Label {
				HorizontalTextAlignment = TextAlignment.Center,
				TextColor = Color.FromHex (Constants.TitleFontColor),
				FontSize = 33
			};

			var segmentLan = new SegmentedControl {
				Children = {
					new SegmentedControlOption{ Text = "ENG" },
					new SegmentedControlOption{ Text = "ESP"}
				}
			};
			segmentLan.ValueChanged += SegmentLan_ValueChanged;

			var titleGrid = new Grid {
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill,
				RowSpacing = 0,
				ColumnSpacing = 0,
				RowDefinitions = {
					new RowDefinition { Height = new GridLength (1, GridUnitType.Auto) }
				}
			};
			titleGrid.ColumnDefinitions.Add (new ColumnDefinition { Width = new GridLength (PlatformHelper.ActionBarHeight, GridUnitType.Absolute) });
			titleGrid.ColumnDefinitions.Add (new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) });
			titleGrid.ColumnDefinitions.Add (new ColumnDefinition { Width = new GridLength (PlatformHelper.ActionBarHeight * 2, GridUnitType.Absolute) });
			titleGrid.Children.Add (leftImage, 0, 0);
			titleGrid.Children.Add (lblSignup, 1, 0);
			titleGrid.Children.Add (segmentLan, 2, 0);

			Image logoImage = new Image 
			{ 
				Source = "logo.png", 
				Aspect = Aspect.AspectFit,
				HorizontalOptions = LayoutOptions.Center
			};

			var logoContent = new ContentView {
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill,
				Padding = new Thickness (20, 20, 20, 20),
				Content = logoImage
			};

			lblUserRole = new Label {
				HorizontalTextAlignment = TextAlignment.Center,
				FontSize = 18,
				TextColor = Color.FromHex (Constants.RegularFontColor)
			};

			var roleContent = new ContentView {
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill,
				Padding = new Thickness (20, 20, 20, 20),
				Content = lblUserRole
			};

			Image frozenImage = new Image{ Source = "lock_transparent.png" };
			var frozenImageContent = new ContentView {
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill,
				Padding = new Thickness (20, 20, 20, 20),
				Content = frozenImage
			};

			lblDecline = new TrukmanLabel {
				HorizontalOptions = LayoutOptions.Center
			};
			var frozenContent = new ContentView {
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill,
				Padding = new Thickness (20, 20, 20, 20),
				Content = lblDecline
			};

			btnContinue = new TrukmanButton ();
			btnContinue.Clicked += ((sender, e) => ShowTopPageMessage.Send());
			var buttonContent = new ContentView {
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill,
				Padding = new Thickness (20, 20, 20, 20),
				Content = btnContinue
			};

			var content = new Grid {
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill,
				RowSpacing = 0,
				ColumnSpacing = 0,
				RowDefinitions = {
					new RowDefinition { Height = new GridLength (1, GridUnitType.Auto) },
					new RowDefinition { Height = new GridLength (1, GridUnitType.Auto) },
					new RowDefinition { Height = new GridLength (1, GridUnitType.Auto) },
					new RowDefinition { Height = new GridLength (1, GridUnitType.Auto) },
					new RowDefinition { Height = new GridLength (1, GridUnitType.Auto) },
					new RowDefinition { Height = new GridLength (1, GridUnitType.Auto) },
					new RowDefinition { Height = new GridLength (1, GridUnitType.Star) }
				}
			};

			content.Children.Add (titleGrid, 0, 0);
			content.Children.Add (logoContent, 0, 1);
			content.Children.Add (roleContent, 0, 2);
			content.Children.Add (frozenImageContent, 0, 3);
			content.Children.Add (frozenContent, 0, 4);
			content.Children.Add (buttonContent, 0, 5);

			Content = content;
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



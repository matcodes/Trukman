using System;

using Xamarin.Forms;
using Trukman.Helpers;
using KAS.Trukman.Views.Pages;
using KAS.Trukman.Helpers;
using KAS.Trukman;

namespace Trukman
{
	public class AuthorizedPage : BasePage
	{
		Label lblSignup;
		Label lblUserRole;
		TrukmanButton btnContinue;
		Label lblAuthorized;

		public AuthorizedPage ()
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

			Image authorizedImage = new Image{ Source = "authorized.png" };
			var authImageContent = new ContentView {
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill,
				Padding = new Thickness (20, 20, 20, 20),
				Content = authorizedImage
			};

			lblAuthorized = new TrukmanLabel {
				HorizontalOptions = LayoutOptions.Center
			};
			//lblAuthorized.FontSize = 18;
			var authContent = new ContentView {
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill,
				Padding = new Thickness (20, 20, 20, 20),
				Content = lblAuthorized
			};

			btnContinue = new TrukmanButton ();
			btnContinue.Clicked += BtnContinue_Clicked;
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
			content.Children.Add (authImageContent, 0, 3);
			content.Children.Add (authContent, 0, 4);
			content.Children.Add (buttonContent, 0, 5);

			Content = content;
		
			UpdateText ();
		}

		void BtnContinue_Clicked (object sender, EventArgs e)
		{
			ShowMainPageMessage.Send ();
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
			lblAuthorized.Text = string.Format (Localization.getString (Localization.LocalStrings.AUTHORIZED_BY_COMPANY), SettingsServiceHelper.GetCompany ());
			btnContinue.Text = Localization.getString (Localization.LocalStrings.CONTINUE);
		}
	}
}



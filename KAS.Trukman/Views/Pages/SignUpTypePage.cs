using System;
using Xamarin.Forms;
using KAS.Trukman.Helpers;

namespace Trukman
{
	public class SignUpTypePage : BasePage
	{
		Label lblSignUpAs;
		SegmentedControl segment;
		Button btnEng;
		Button btnEsp;

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			NavigationPage.SetHasNavigationBar (this, false);
		}

		public SignUpTypePage ()
		{
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

			var titleGrid = new Grid {
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill,
				RowSpacing = 0,
				ColumnSpacing = 0,
				RowDefinitions = {
					new RowDefinition { Height = new GridLength (1, GridUnitType.Auto) }
				}
			};
			titleGrid.ColumnDefinitions.Add (new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) });
			titleGrid.ColumnDefinitions.Add (new ColumnDefinition { Width = new GridLength (PlatformHelper.ActionBarHeight, GridUnitType.Absolute) });
			titleGrid.ColumnDefinitions.Add (new ColumnDefinition { Width = new GridLength (PlatformHelper.ActionBarHeight, GridUnitType.Absolute) });
			titleGrid.Children.Add (btnEng, 1, 0);
			titleGrid.Children.Add (btnEsp, 2, 0);

			Image logoImage = new Image { 
				Source = "logo.png", 
				Aspect = Aspect.AspectFit,
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Center
			};

			var logoContent = new ContentView {
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill,
				Padding = new Thickness (20, 0, 20, 0),
				Content = logoImage
			};

			lblSignUpAs = new Label {
				HorizontalTextAlignment = TextAlignment.Center,
				VerticalTextAlignment = TextAlignment.Center,
				TextColor = Color.FromHex ("F5FFFF"),
				FontSize = Device.GetNamedSize (NamedSize.Large, typeof(Xamarin.Forms.Label)),
				HeightRequest = 60,
			};

			var labelContent = new ContentView {
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill,
				Padding = new Thickness (20, 0, 20, 0),
				Content = lblSignUpAs
			};

			segment = new SegmentedControl {
				HorizontalOptions = LayoutOptions.Center,
				Children = {
					new SegmentedControlOption{ },
					new SegmentedControlOption{ },
					new SegmentedControlOption{ }
				}
			};
			segment.ValueChanged += Segment_ValueChanged;
			
			var segmentContent = new ContentView {
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill,
				Padding = new Thickness (20, 0, 20, 0),
				Content = segment
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
					new RowDefinition { Height = new GridLength (1, GridUnitType.Star) }
				}
			};

			content.Children.Add (titleGrid, 0, 0);
			content.Children.Add (logoContent, 0, 1);
			content.Children.Add (new BoxView{ HeightRequest = 20 }, 0, 2);
			content.Children.Add (labelContent, 0, 3);
			content.Children.Add (segmentContent, 0, 4);
			//pageContent.Children.Add (busyIndicator);

			Content = content;
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

		void SegmentLan_ValueChanged (object sender, EventArgs e)
		{
			if (((SegmentedControl)sender).SelectedValue == ((SegmentedControl)sender).Children [0].Text)
				Localization.language = Localization.Languages.ENGLISH;
			else if (((SegmentedControl)sender).SelectedValue == ((SegmentedControl)sender).Children [1].Text)
				Localization.language = Localization.Languages.ESPANIOL;

			UpdateText ();
		}

		void UpdateText(){
			lblSignUpAs.Text = Localization.getString (Localization.LocalStrings.SIGN_UP_AS).ToUpper ();
			segment.Children [0].Text = Localization.getString (Localization.LocalStrings.DRIVER).ToUpper();
			segment.Children [1].Text = Localization.getString (Localization.LocalStrings.DISPATCH).ToUpper();
			segment.Children [2].Text = Localization.getString (Localization.LocalStrings.OWNER_or_OPERATOR).ToUpper();
		}

		void ClearSegmentValue (object sender)
		{
			((SegmentedControl)sender).ValueChanged -= Segment_ValueChanged;
			((SegmentedControl)sender).SelectedValue = null;
			((SegmentedControl)sender).ValueChanged += Segment_ValueChanged;
		}

		async void Segment_ValueChanged (object sender, EventArgs e)
		{
			if (((SegmentedControl)sender).SelectedValue == ((SegmentedControl)sender).Children [0].Text) {
				ClearSegmentValue (sender);
				await Navigation.PushAsync (new SignUpDriverPage ());
			} 
			else if (((SegmentedControl)sender).SelectedValue == ((SegmentedControl)sender).Children [1].Text) {
				ClearSegmentValue (sender);
				await Navigation.PushAsync (new SignUpDispatchPage ());
			} 
			else if (((SegmentedControl)sender).SelectedValue == ((SegmentedControl)sender).Children [2].Text) {
				ClearSegmentValue (sender);
				await Navigation.PushAsync (new SignUpOwnerPage ());
			}
		}
	}
}



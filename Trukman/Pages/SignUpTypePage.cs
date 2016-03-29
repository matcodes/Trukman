using System;
using Xamarin.Forms;

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
			// TODO: добавить сюда кнопки переключения языка

			Image logoImage = new Image { Source = ImageSource.FromResource ("logo.png"), Aspect = Aspect.AspectFit };

			Image backgroundImage = new Image{ Source = ImageSource.FromResource("background.png"), Aspect = Aspect.Fill};
			//BackgroundImage = "background.png"; не работает

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

			lblSignUpAs = new Label {
				HorizontalTextAlignment = TextAlignment.Center,
				VerticalTextAlignment = TextAlignment.Center,
				TextColor = Color.FromHex("F5FFFF"),
				FontSize = 19,
				HeightRequest = 60,
			};

			segment = new SegmentedControl {
				HorizontalOptions = LayoutOptions.Center,
				Children = {
					new SegmentedControlOption{},
					new SegmentedControlOption{},
					new SegmentedControlOption{}
				}
			};
			segment.ValueChanged += Segment_ValueChanged;
			//segment.WidthRequest = 255;
			//segment.MinimumWidthRequest = 85;

			StackLayout stackLayout = new StackLayout { 
				Spacing = Constants.StackLayoutDefaultSpacing,
				Padding = new Thickness(Constants.ViewsPadding),
				VerticalOptions = LayoutOptions.CenterAndExpand,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Children = {
					lblSignUpAs,
					segment, 
				}
			};

			RelativeLayout relativeLayout = new RelativeLayout ();

			relativeLayout.Children.Add (backgroundImage, 
				Constraint.RelativeToParent (parent => 0),
				Constraint.RelativeToParent (parent => 0),
				Constraint.RelativeToParent (parent => parent.Width),
				Constraint.RelativeToParent (parent => parent.Height)
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
			relativeLayout.Children.Add (stackLayout,
				Constraint.RelativeToParent (parent => parent.Width / 2 - stackLayout.Width / 2),
				Constraint.RelativeToParent (parent => parent.Height / 2 - stackLayout.Height / 2),
				Constraint.RelativeToParent (parent => parent.Width)
			);
			relativeLayout.Children.Add (logoImage,  
				Constraint.RelativeToParent (parent => parent.Width / 2 - logoImage.Width / 2),
				Constraint.RelativeToView (stackLayout, (parent, view) => {
					return ((parent.Height - view.Height) / 2 - logoImage.Height) / 2;
				})
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

		void UpdateText(){
			lblSignUpAs.Text = Localization.getString (Localization.LocalStrings.SIGN_UP_AS).ToUpper ();
			segment.Children [0].Text = Localization.getString (Localization.LocalStrings.DISPATCH);
			segment.Children [1].Text = Localization.getString (Localization.LocalStrings.DRIVER);
			segment.Children [2].Text = Localization.getString (Localization.LocalStrings.OWNER);
		}

		async void Segment_ValueChanged (object sender, EventArgs e)
		{
			if (((SegmentedControl)sender).SelectedValue == ((SegmentedControl)sender).Children [0].Text)
				await Navigation.PushAsync (new SignUpDispatchPage ());
			else if (((SegmentedControl)sender).SelectedValue == ((SegmentedControl)sender).Children [1].Text)
				await Navigation.PushAsync (new SignUpDriverPage ());
			else
				await Navigation.PushAsync (new SignUpOwnerPage ());
		}
	}
}



using System;
using Xamarin.Forms;

namespace Trukman
{
	public class SignUpTypePage : BasePage
	{
		Label lblSignUpAs;
		SegmentedControl segment;

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			NavigationPage.SetHasNavigationBar (this, false);
		}

		public SignUpTypePage ()
		{
			Image logoImage = new Image { Source = ImageSource.FromResource ("logo.png"), Aspect = Aspect.AspectFit };

			Image backgroundImage = new Image{ Source = ImageSource.FromResource("background.png"), Aspect = Aspect.Fill};
			//BackgroundImage = "background.png"; не работает

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
			updateText ();
		}

		void updateText(){
			Localization.language = Localization.Languages.ENGLISH;

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



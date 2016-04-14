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
			Image logoImage = new Image { Source = ImageSource.FromFile ("logo.png"), Aspect = Aspect.AspectFit };

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
					new SegmentedControlOption{ },
					new SegmentedControlOption{ },
					new SegmentedControlOption{ }
				}
			};
			segment.ValueChanged += Segment_ValueChanged;

			var segmentLan = new SegmentedControl {
				Children = {
					new SegmentedControlOption{ Text = "ENG" },
					new SegmentedControlOption{ Text = "ESP"}
				}
			};
			segmentLan.ValueChanged += SegmentLan_ValueChanged;

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

			relativeLayout.Children.Add (segmentLan, 
				Constraint.RelativeToParent (parent => parent.Width - segmentLan.Width),
				Constraint.RelativeToParent (parent => 0)
			);
			relativeLayout.Children.Add (stackLayout,
				Constraint.RelativeToParent (parent => parent.Width / 2 - stackLayout.Width / 2),
				Constraint.RelativeToParent (parent => parent.Height / 2 - stackLayout.Height / 2),
				Constraint.RelativeToParent (parent => parent.Width)
			);
			relativeLayout.Children.Add (logoImage,  
				Constraint.RelativeToParent (parent => parent.Width / 2 - logoImage.Width / 2),
				Constraint.RelativeToView (stackLayout, (parent, view) => ((parent.Height - view.Height) / 2 - logoImage.Height) / 2 + Constants.ViewsBottomPadding)
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



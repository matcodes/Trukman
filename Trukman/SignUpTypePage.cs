using System;
using Xamarin.Forms;

namespace Trukman
{
	public class SignUpTypePage : BasePage
	{
		public SignUpTypePage ()
		{
			Label lblTitle = new Label {
				HorizontalTextAlignment = TextAlignment.Center,
				Text = "TRUKMAN",
				FontSize = 33
			};
			Label lblSignUpAs = new Label {
				HorizontalTextAlignment = TextAlignment.Start,
				VerticalTextAlignment = TextAlignment.Center,
				Text = "Sign Up As",
				FontSize = 19,
				HeightRequest = 60,
			};

			TrukmanButton btnOwner = new TrukmanButton {
				Text = "OWNER/OPERATOR OR FLEET",

			};
			TrukmanButton btnDispatch = new TrukmanButton {
				Text = "DISPATCH"
			};
			TrukmanButton btnDriver = new TrukmanButton {
				Text = "DRIVER"
			};

			SegmentedControl segment = new SegmentedControl {
				Children = {
					new SegmentedControlOption {					
						Text = "English"
					},
					new SegmentedControlOption {					
						Text = "Espanol"
					}
				}
			};

			btnOwner.Clicked += buttonClicked;
			btnDispatch.Clicked += buttonClicked;
			btnDriver.Clicked += buttonClicked;

			StackLayout stackLayout = new StackLayout { 
				Spacing = Constants.StackLayoutDefaultSpacing,
				Padding = new Thickness(Constants.ViewsPadding),
				VerticalOptions = LayoutOptions.CenterAndExpand,
				Children = {
					lblTitle,
					lblSignUpAs,
					btnOwner,
					btnDispatch,
					btnDriver
				}
			};

			RelativeLayout relativeLayout = new RelativeLayout ();
			relativeLayout.Children.Add (segment,
				Constraint.RelativeToParent ((parent) => {
					return parent.Width / 2 - segment.Width / 2;
				}),
				Constraint.RelativeToParent ((parent) => {
					return parent.Height - segment.Height - Constants.ViewsBottomPadding;
				}));
			relativeLayout.Children.Add (stackLayout,
				Constraint.RelativeToParent ((parent) => {
					return parent.Width / 2 - stackLayout.Width / 2;
				}),
				Constraint.RelativeToParent ((parent) => {
					return parent.Height / 2 - stackLayout.Height / 2;
				}),
				Constraint.RelativeToParent ((parent) => {
					return parent.Width;
				}));

			Content = relativeLayout;					
		}

		async private void buttonClicked (object sender, EventArgs e) {
			await Navigation.PushAsync (new SignUpPage());
		}
	}
}



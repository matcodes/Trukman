using System;
using Xamarin.Forms;

namespace Trukman
{
	public class SignUpAssignDispatchPage : BasePage
	{
		Switch switchAssignDispatch;

		public SignUpAssignDispatchPage ()
		{
			Label lblTitle = new Label {
				HorizontalTextAlignment = TextAlignment.Center,
				Text = "TRUKMAN",
				FontSize = 33
			};

			Label lblWelcome = new Label {
				HorizontalTextAlignment = TextAlignment.Start,
				VerticalTextAlignment = TextAlignment.Center,
				Text = "Welcome to Trukman. Let's get you set-up",
				FontSize = 14,
				HeightRequest = 60
			};

			TrukmanButton btnNext = new TrukmanButton {
				Text = "NEXT"
			};

			switchAssignDispatch = new Switch ();

			RelativeLayout assignDispatchLayout = new RelativeLayout {
//				VerticalOptions = LayoutOptions.Fill
			};

			assignDispatchLayout.Children.Add (switchAssignDispatch, Constraint.RelativeToParent ((parent) => {
				return parent.Width - switchAssignDispatch.Width - 10;
			}), Constraint.RelativeToParent ((parent) => {
				return 5;
			}));
			assignDispatchLayout.Children.Add (new Label {Text = "ASSIGN DISPATCH"}, Constraint.RelativeToParent ((parent) => {
				return 5;
			}), Constraint.RelativeToParent ((parent) => {
				return 5;
			}));

			btnNext.Clicked += nextPressed;

			Content = new StackLayout {
				VerticalOptions = LayoutOptions.Center,
				Spacing = Constants.StackLayoutDefaultSpacing,
				Padding = new Thickness (Constants.ViewsPadding),
				Children = {
					lblTitle,
					lblWelcome,
					assignDispatchLayout,
					new BoxView {
						HeightRequest = 60
					},
					btnNext
				}
			};
		}

		async void nextPressed (object sender, EventArgs e) {
			await Navigation.PushAsync (new SignUpInvite ());
		}
	}
}


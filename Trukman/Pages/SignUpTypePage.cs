using System;
using Xamarin.Forms;

namespace Trukman
{
	public class SignUpTypePage : ContentPage
	{
		Label lblSignUpAs;
		TrukmanButton btnOwner;
		TrukmanButton btnDispatch;
		TrukmanButton btnDriver;

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			NavigationPage.SetHasNavigationBar (this, false);
		}

		public SignUpTypePage ()
		{
			Label lblTitle = new Label {
				HorizontalTextAlignment = TextAlignment.Center,
				Text = "TRUKMAN",
				FontSize = 33
			};
			lblSignUpAs = new Label {
				HorizontalTextAlignment = TextAlignment.Start,
				VerticalTextAlignment = TextAlignment.Center,
				FontSize = 19,
				HeightRequest = 60,
			};

			btnOwner = new TrukmanButton {
			};
			btnDispatch = new TrukmanButton {
				Tag = 1
			};
			btnDriver = new TrukmanButton {
				Tag = 2
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

			segment.ValueChanged += Segment_ValueChanged;
			btnOwner.Clicked += ownerClicked;
			btnDispatch.Clicked += driverClicked;
			btnDriver.Clicked += driverClicked;

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
			updateText ();
		}

		void updateText(){
			lblSignUpAs.Text = Localization.getString (Localization.LocalStrings.SIGN_UP_AS);
			btnOwner.Text = Localization.getString (Localization.LocalStrings.OWNER_or_OPERATOR_OR_FLEET);
			btnDispatch.Text = Localization.getString (Localization.LocalStrings.DISPATCH);
			btnDriver.Text = Localization.getString (Localization.LocalStrings.DRIVER);
		}

		void Segment_ValueChanged (object sender, EventArgs e)
		{
			if (((SegmentedControl)sender).SelectedValue == "English") {
				Localization.language = Localization.Languages.ENGLISH;
			}else if (((SegmentedControl)sender).SelectedValue == "Espanol") {
				Localization.language = Localization.Languages.ESPANIOL;
			}
			updateText();
		}

		async private void ownerClicked (object sender, EventArgs e) {
			await Navigation.PushAsync (new SignUpPage());
		}

		async private void driverClicked (object sender, EventArgs e) {
			await Navigation.PushAsync (new SignUpDriverPage {userRole = (UserRole)((TrukmanButton)sender).Tag});
		}
	}
}



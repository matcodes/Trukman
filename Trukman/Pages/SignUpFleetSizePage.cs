using System;
using Xamarin.Forms;

namespace Trukman
{
	public class SignUpFleetSizePage : BasePage
	{

		RelativeLayout relativeLayout = new RelativeLayout ();

		public SignUpFleetSizePage ()
		{
			NavigationPage.SetHasNavigationBar (this, false);

			Label lblTitle = new Label {
				HorizontalTextAlignment = TextAlignment.Center,
				Text = "TRUKMAN",
				FontSize = 33
			};

			Label lblWelcome = new Label {
				HorizontalTextAlignment = TextAlignment.Start,
				VerticalTextAlignment = TextAlignment.Center,
				Text = Localization.getString(Localization.LocalStrings.WELCOME),
				FontSize = 14,
				HeightRequest = 60,
			};
				
			Label lblFleet = new Label {
				HorizontalTextAlignment = TextAlignment.Start,
				VerticalTextAlignment = TextAlignment.Center,
				Text = "FLEET SIZE",
				FontSize = 25,
				TextColor = Color.FromRgb(219,219,219)

			};

			Label lblFirstNummer = new Label {
				HorizontalTextAlignment = TextAlignment.Start,
				VerticalTextAlignment = TextAlignment.Center,
				Text = "0",
				FontSize = 25,
				TextColor = Color.FromRgb(219,219,219)
			};

			Label lblSecondNummer = new Label {
				HorizontalTextAlignment = TextAlignment.Start,
				VerticalTextAlignment = TextAlignment.Center,
				Text = "0",
				FontSize = 25,
				TextColor = Color.FromRgb(219,219,219)
			};

			Label lblLastNummer = new Label {
				HorizontalTextAlignment = TextAlignment.Start,
				VerticalTextAlignment = TextAlignment.Center,
				Text = "0",
				FontSize = 25,
				TextColor = Color.FromRgb(219,219,219)
			};

			relativeLayout.Children.Add (lblFleet, Constraint.RelativeToParent((Parent) => {
				return 0;
			}));

			relativeLayout.Children.Add (lblFirstNummer, Constraint.RelativeToView(lblFleet, (parent, view) => {
				return parent.Width - 20;
			}));

			relativeLayout.Children.Add(lblSecondNummer, Constraint.RelativeToView(lblFirstNummer, (parent, view) => {
				return view.X - 40;
			}));

			relativeLayout.Children.Add (lblLastNummer, Constraint.RelativeToView (lblSecondNummer, (parent, view) => {
				return view.X - 40;
			}));

			TrukmanButton btnNext = new TrukmanButton {
				Text = Localization.getString(Localization.LocalStrings.NEXT)
			};

			btnNext.Clicked += invitePressed;

			Content = new StackLayout {
				VerticalOptions = LayoutOptions.CenterAndExpand,
				Spacing = Constants.StackLayoutDefaultSpacing,
				Padding = new Thickness (Constants.ViewsPadding),
				Children = {
					lblTitle,
					lblWelcome,
					relativeLayout,
					new BoxView {
						HeightRequest = 60
					},
					btnNext
				}
			};
		}

		async void invitePressed (object sender, EventArgs e) {
//			await Navigation.PushAsync (new  MenuPage ());
//			await DisplayAlert (null, "You're Signed Up Congratulations", "CLOSE");	
			App.ServerManager.StartTimerForRequest ();
//			ser
		}
	}
}


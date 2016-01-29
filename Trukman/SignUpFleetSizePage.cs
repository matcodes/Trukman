using System;
using Xamarin.Forms;

namespace Trukman
{
	public class SignUpFleetSizePage : BasePage
	{
		public SignUpFleetSizePage ()
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
				HeightRequest = 60,
			};

			TrukmanButton btnInvite = new TrukmanButton {
				Text = "INVITE DRIVERS"
			};

			btnInvite.Clicked += invitePressed;

			Content = new StackLayout {
				VerticalOptions = LayoutOptions.CenterAndExpand,
				Spacing = Constants.StackLayoutDefaultSpacing,
				Padding = new Thickness (Constants.ViewsPadding),
				Children = {
					lblTitle,
					lblWelcome,
					new BoxView {
						HeightRequest = 60
					},
					btnInvite
				}
			};
		}

		async void invitePressed (object sender, EventArgs e) {
			await Navigation.PushAsync (new SignUpInvite ());
		}
	}
}


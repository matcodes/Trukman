using System;

using Xamarin.Forms;
using Parse;

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
				Text = "OWNER/OPERATOR OR FLEET"
			};
			TrukmanButton btnDispatch = new TrukmanButton {
				Text = "DISPATCH"
			};
			TrukmanButton btnDriver = new TrukmanButton {
				Text = "DRIVER"
			};

			btnOwner.Clicked += buttonClicked;
			btnDispatch.Clicked += buttonClicked;
			btnDriver.Clicked += buttonClicked;

			Content = //new RelativeLayout {
//				Children = {
					new StackLayout { 
					Spacing = 10,
					Padding = new Thickness(40),
					VerticalOptions = LayoutOptions.CenterAndExpand,
					Children = {
						lblTitle,
						lblSignUpAs,
						btnOwner,
						btnDispatch,
						btnDriver
						}
//					}
//				}
			};
		}

		async private void buttonClicked (object sender, EventArgs e) {
			await Navigation.PushAsync (new SignUpPage());
		}
	}
}



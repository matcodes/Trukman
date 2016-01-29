using System;
using Xamarin.Forms;

namespace Trukman
{
	public class SignUpInvite : BasePage
	{
		TruckmanEditor edtName;
		TruckmanEditor edtPhone;

		public SignUpInvite ()
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

			edtName = new TruckmanEditor {
				Text = "DRIVER NAME",
				BackgroundColor = Color.FromRgb (230, 230, 230)
			};
			edtPhone = new TruckmanEditor {
				Text = "PHONE",
				BackgroundColor = Color.FromRgb (230, 230, 230)
			};

			TrukmanButton btnInvite = new TrukmanButton {
				Text = "INVITE ADDITIONAL DRIVERS"
			};

			TrukmanButton btnAssign = new TrukmanButton {
				Text = "ASSIGN DISPATCH"
			};
			btnAssign.Clicked += assignPressed;

			Content = new StackLayout {
				VerticalOptions = LayoutOptions.CenterAndExpand,
				Spacing = Constants.StackLayoutDefaultSpacing,
				Padding = new Thickness (Constants.ViewsPadding),
				Children = {
					lblTitle,
					lblWelcome,
					edtName,
					edtPhone,
					new BoxView {
						HeightRequest = 30
					},
					btnInvite,
					new BoxView {
						HeightRequest = 30
					},
					btnAssign
				}
			};
		}

		async void assignPressed (object sender, EventArgs e) {
			await Navigation.PushAsync (new SignUpAssignDispatchPage ());
		}
	}
}


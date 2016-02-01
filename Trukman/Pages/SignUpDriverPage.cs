using System;
using Xamarin.Forms;

namespace Trukman
{
	public class SignUpDriverPage : BasePage
	{
		public SignUpDriverPage ()
		{
			NavigationPage.SetHasNavigationBar (this, false);


			Label lblTitle = new Label {
				HorizontalTextAlignment = TextAlignment.Center,
				Text = "TRUKMAN",
				FontSize = 33
			};

			TrukmanButton btnNext = new TrukmanButton {
				Text = "SEND"
			};

			TrukmanEditor edtName = new TrukmanEditor {
				Text = "FULL NAME"
			};

			TrukmanEditor edtPhone = new TrukmanEditor {
				Text = "PHONE"
			};

			TrukmanEditor edtCompany = new TrukmanEditor {
				Text = "COMPANY YOU WORK FOR"
			};

			Content = new StackLayout {
				VerticalOptions = LayoutOptions.Center,
				Spacing = Constants.StackLayoutDefaultSpacing,
				Padding = new Thickness (Constants.ViewsPadding),
				Children = {
					lblTitle,
					new BoxView {
						HeightRequest = 60
					},
					edtName,
					edtPhone,
					new BoxView {
						HeightRequest = 60
					},
					edtCompany,
					new BoxView {
						HeightRequest = 30
					},
					btnNext
				}
			};
		}
	}
}


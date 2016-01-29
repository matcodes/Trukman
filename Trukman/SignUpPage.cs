using System;

using Xamarin.Forms;

namespace Trukman
{
	public class SignUpPage : BasePage
	{
		TruckmanEditor edtName;
		TruckmanEditor edtPhone;
		TruckmanEditor edtMC;

		public SignUpPage ()
		{
			Label lblTitle = new Label {
				HorizontalTextAlignment = TextAlignment.Center,
				Text = "TRUKMAN",
				FontSize = 33
			};
			Label lblSignUpAs = new Label {
				HorizontalTextAlignment = TextAlignment.Start,
				VerticalTextAlignment = TextAlignment.Center,
				Text = "Sign Up",
				FontSize = 19,
				HeightRequest = 60,
			};

			edtName = new TruckmanEditor {
				Text = "Full Name",
				BackgroundColor = Color.FromRgb (230, 230, 230)
			};
			edtPhone = new TruckmanEditor {
				Text = "Phone",
				BackgroundColor = Color.FromRgb (230, 230, 230)
			};
			edtMC = new TruckmanEditor {
				Text = "MC #",
				BackgroundColor = Color.FromRgb (230, 230, 230)
			};

			TrukmanButton btnEnter = new TrukmanButton {
				Text = "ENTER"
			};

			Label lblTerms = new Label {
				Text = "By clicking Enter you agree to the ",
				FontSize = 11,
				VerticalTextAlignment = TextAlignment.Center
			};

			Button btnTerms = new Button {
				Text = "Terms and Conditions",
				BackgroundColor = Color.FromRgba (0, 0, 0, 0),
				FontSize = 11
			};

			RelativeLayout relativeTermsLayout = new RelativeLayout {};
			relativeTermsLayout.Children.Add (lblTerms, Constraint.RelativeToParent ((parent) => {
				return 0;
			}), Constraint.RelativeToParent ((parent) => {
				return 0;
			}), null, 
				Constraint.RelativeToParent ((parent) => {
					return 50;
				}));

			relativeTermsLayout.Children.Add (btnTerms, 
				Constraint.RelativeToView (lblTerms, (parent, sibling) => {
					return lblTerms.Width;
			}), null, null, 
				Constraint.RelativeToView (lblTerms, (parent, sibling) => {
					return lblTerms.Height;
			}));

			btnEnter.Clicked += buttonClicked;

			Content = new StackLayout { 
				Spacing = Constants.StackLayoutDefaultSpacing,
				Padding = new Thickness (Constants.ViewsPadding),
				VerticalOptions = LayoutOptions.CenterAndExpand,
				Children = {
					lblTitle,
					lblSignUpAs,
					edtName,
					edtPhone,
					edtMC,
					relativeTermsLayout,
					btnEnter
				}
			};
		}

		async void buttonClicked (object sender, EventArgs e) {
			await Navigation.PushAsync(new SignUpCompanyPage());
			//App.ServerManager.Register(edtName.Text, edtMC.Text);
		}
	}
}



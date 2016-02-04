using System;

using Xamarin.Forms;

namespace Trukman
{
	public class SignUpPage : BasePage
	{
		TrukmanEditor edtName;
		TrukmanEditor edtPhone;
		TrukmanEditor edtMC;

		public SignUpPage ()
		{
			NavigationPage.SetHasNavigationBar (this, false);

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

			edtName = new TrukmanEditor {
				Placeholder = Localization.getString(Localization.LocalStrings.FULL_NAME)
			};
			edtPhone = new TrukmanEditor {
				Placeholder = Localization.getString(Localization.LocalStrings.PHONE)
			};
			edtMC = new TrukmanEditor {
				Placeholder = Localization.getString(Localization.LocalStrings.MC)
			};

			TrukmanButton btnEnter = new TrukmanButton {
				Text = Localization.getString(Localization.LocalStrings.ENTER)
			};

			Label lblTerms = new Label {
				Text = Localization.getString(Localization.LocalStrings.BY_CLICKING_ENTER_YOU_AGREE_TO_THE),
				FontSize = 11,
				VerticalTextAlignment = TextAlignment.Center
			};

			Button btnTerms = new Button {
				Text = Localization.getString(Localization.LocalStrings.TERMS_AND_CONDITIONS),
				BackgroundColor = Color.FromRgba (0, 0, 0, 0),
				FontSize = 11
			};

			RelativeLayout relativeTermsLayout = new RelativeLayout {
				HorizontalOptions = LayoutOptions.Fill
			};
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
			App.ServerManager.Register(edtName.Text, edtMC.Text, UserRole.UserRoleOwner);
		}
	}
}



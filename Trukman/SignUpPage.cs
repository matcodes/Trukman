using System;

using Xamarin.Forms;

namespace Trukman
{
	public class SignUpPage : BasePage
	{
		private Editor edtName;
		private Editor edtPass;

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

			edtName = new Editor {
				BackgroundColor = Color.FromRgb (230, 230, 230)
			};
			edtPass = new Editor {
				BackgroundColor = Color.FromRgb (230, 230, 230)
			};

			TrukmanButton btnRegister = new TrukmanButton {
				Text = "REGISTER"
			};

			btnRegister.Clicked += buttonClicked;

			Content = //new RelativeLayout {
			//				Children = {
				new StackLayout { 
				Spacing = 10,
				Padding = new Thickness (40),
				VerticalOptions = LayoutOptions.CenterAndExpand,
				Children = {
					lblTitle,
					lblSignUpAs,
					edtName,
					edtPass,
					btnRegister
				}
				//					}
				//				}
			};
		}

		private void buttonClicked (object sender, EventArgs e) {
			App.ServerManager.Register(edtName.Text, edtPass.Text);
		}
	}
}



using System;
using Xamarin.Forms;

namespace Trukman
{
	public class SignUpFleetSizePage : BasePage
	{

		RelativeLayout relativeLayout = new RelativeLayout ();
		Picker picker;
		Button btnPicker;
		Label lblNummer1;
		Label lblNummer2;
		Label lblNummer3;

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

			lblNummer1 = new Label {
				HorizontalTextAlignment = TextAlignment.Start,
				VerticalTextAlignment = TextAlignment.Center,
				Text = "0",
				FontSize = 25,
				TextColor = Color.FromRgb(219,219,219)
			};

			lblNummer2 = new Label {
				HorizontalTextAlignment = TextAlignment.Start,
				VerticalTextAlignment = TextAlignment.Center,
				Text = "0",
				FontSize = 25,
				TextColor = Color.FromRgb(219,219,219)
			};

			lblNummer3 = new Label {
				HorizontalTextAlignment = TextAlignment.Start,
				VerticalTextAlignment = TextAlignment.Center,
				Text = "0",
				FontSize = 25,
				TextColor = Color.FromRgb(219,219,219)
			};

			btnPicker = new Button {
				Text = "",
				BackgroundColor = Color.Transparent
			};

			picker = new Picker {
				Title = "fleet",
				IsVisible = false,
			};
			picker.SelectedIndexChanged += pickerColorPicker_SelectedIndexChanged;
				
			for(int i = 1; i < 1000; i++){
				picker.Items.Add (Convert.ToString (i));
			}

			relativeLayout.Children.Add (lblFleet, 
				Constraint.RelativeToParent((Parent) => {
					return 0;
				}));
			relativeLayout.Children.Add (lblNummer1, 
				Constraint.RelativeToView (lblFleet, (parent, view) => {
					return parent.Width - 120;
				}),
				Constraint.RelativeToView (lblFleet, (parent, view) => {
					return view.Y;
				}));
			relativeLayout.Children.Add (lblNummer2, 
				Constraint.RelativeToView (lblNummer1, (parent, view) => {
					return view.X + 35;
				}),
				Constraint.RelativeToView (lblFleet, (parent, view) => {
					return view.Y;
				}));
			relativeLayout.Children.Add (lblNummer3, 
				Constraint.RelativeToView (lblNummer2, (parent, view) => {
					return view.X + 35;
				}),
				Constraint.RelativeToView (lblFleet, (parent, view) => {
					return view.Y;
				}));
			relativeLayout.Children.Add (btnPicker,
				Constraint.RelativeToView (lblFleet, (parent, view) => {
					return 0;
				}),
				Constraint.RelativeToView (lblFleet, (parent, view) => {
					return 0;
				}),
				Constraint.RelativeToView (lblFleet, (parent, view) => {
					return parent.Width;
				}));

			TrukmanButton btnNext = new TrukmanButton {
				Text = Localization.getString(Localization.LocalStrings.NEXT)
			};

			btnPicker.Clicked += TapImgColorPicker_Tapped;
			btnNext.Clicked += invitePressed;

			Content = new StackLayout {
				VerticalOptions = LayoutOptions.CenterAndExpand,
				Spacing = Constants.StackLayoutDefaultSpacing,
				Padding = new Thickness (Constants.ViewsPadding),
				Children = {
					lblTitle,
					lblWelcome,
					relativeLayout,
					picker,
					new BoxView {
						HeightRequest = 60
					},
					btnNext
				}
			};
		}

		async void invitePressed (object sender, EventArgs e) {
			//App.ServerManager.StartTimerForRequest ();
			await Navigation.PushModalAsync (new RootPage ());
		}
		public void TapImgColorPicker_Tapped(object sender, EventArgs e)
		{
			picker.Focus ();
		}

		public void pickerColorPicker_SelectedIndexChanged(object sender, EventArgs e)
		{
			int x = Convert.ToInt32(picker.Items[picker.SelectedIndex]) / 100;
			int y = (Convert.ToInt32 (picker.Items [picker.SelectedIndex]) - x * 100) / 10;
			int z = Convert.ToInt32(picker.Items[picker.SelectedIndex]) - x * 100 - y * 10;
			lblNummer1.Text = Convert.ToString(x);
			lblNummer2.Text = Convert.ToString (y);
			lblNummer3.Text = Convert.ToString (z);
		}
	}
}


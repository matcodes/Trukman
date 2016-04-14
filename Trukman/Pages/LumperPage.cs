using System;

using Xamarin.Forms;

namespace Trukman
{
	public class LumperPage : BasePage
	{
		public LumperPage ()
		{
			NavigationPage.SetHasNavigationBar (this, false);

			Image leftImage = new Image{ Source = ImageSource.FromFile ("left.png"), Aspect = Aspect.Fill };
			Label lblLumper = new Label {
				HorizontalTextAlignment = TextAlignment.Center,
				TextColor = Color.FromHex (Constants.TitleFontColor),
				FontSize = 33,
				Text = Localization.getString (Localization.LocalStrings.LUMPER)
			};

			Image lumperImage = new Image{ Source = ImageSource.FromFile ("lumper.png"), Aspect = Aspect.Fill };

			Label lblLumperState = new TrukmanLabel {
				Text = Localization.getString (Localization.LocalStrings.NO_LUMPER),
				FontSize = 18
			};

			Button btnHome = new Button{Text = "Home" };
			btnHome.Clicked += BtnHome_Clicked;

			TrukmanButton btnRequest = new TrukmanButton ();
			btnRequest.Clicked += BtnRequest_Clicked;

			var relativeLayout = new RelativeLayout ();

			relativeLayout.Children.Add (lblLumper, 
				Constraint.RelativeToParent (parent => 0), //parent.Width / 2 - lblMain.Width / 2),
				Constraint.RelativeToParent (parent => Constants.ViewsBottomPadding),
				Constraint.RelativeToParent (Parent => Parent.Width)
			);
			relativeLayout.Children.Add (leftImage, 
				Constraint.RelativeToParent (parent => Constants.ViewsBottomPadding),
				Constraint.RelativeToParent (parent => Constants.ViewsBottomPadding),
				Constraint.RelativeToView (lblLumper, (parent, view) => view.Height / 2),
				Constraint.RelativeToView (lblLumper, (parent, view) => view.Height / 2)
			);
			relativeLayout.Children.Add (lumperImage,
				Constraint.RelativeToParent (parent => parent.Width / 2 - lumperImage.Width / 2),
				Constraint.RelativeToView (lblLumper, (parent, view) => view.Y + view.Height + Constants.ViewsPadding)
			);
			relativeLayout.Children.Add (lblLumperState,
				Constraint.RelativeToParent (parent => parent.Width / 2 - lblLumperState.Width / 2),
				Constraint.RelativeToView (lumperImage, (parent, view) => view.Y + view.Height + Constants.ViewsPadding)
				//Constraint.RelativeToParent(parent => parent.Width - 
			);

			Content = relativeLayout;
		}

		void BtnRequest_Clicked (object sender, EventArgs e)
		{
			
		}

		void BtnHome_Clicked (object sender, EventArgs e)
		{
			Navigation.PopAsync ();
		}
	}
}



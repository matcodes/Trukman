using System;

using Xamarin.Forms;
using Trukman.ViewModels.Pages;
using KAS.Trukman.Views.Pages;
using KAS.Trukman.Controls;
using KAS.Trukman.Helpers;
using KAS.Trukman;
using Trukman.Helpers;

namespace Trukman
{
	public class SignUpOwnerPage : TrukmanPage
	{
		Label lblSignup;
		Label lblUserRole;
		AppEntry edtMC;
        ContentView failedMCContent;
		AppLabel failedMCLabel;
		AppButton btnSubmit;
		Button btnEng;
		Button btnEsp;

		StackLayout stackLayout;
		RelativeLayout failedMCLayout;

		int failedAttempts;
		int maxFailAttempts = 3;
		ActivityIndicator busyIndicator;

		public SignUpOwnerPage ()
		{
			this.BindingContext = new SignUpOwnerViewModel ();
		}

        //      protected override View CreateContent ()
        //{
        //	//Localization.language = Localization.Languages.ENGLISH;
        //	/*var titleBar = new TitleBar();
        //	titleBar.SetBinding (TitleBar.TitleProperty, "Title", BindingMode.OneWay);
        //	titleBar.SetBinding (TitleBar.LeftCommandProperty, "LeftCommand", BindingMode.OneWay);*/

        //	failedAttempts = 0;
        //	var btnLeft = new ToolButton ();
        //	btnLeft.ImageSourceName = PlatformHelper.LeftImageSource;
        //	btnLeft.SetBinding(ToolButton.CommandProperty, "ShowPrevPageCommand", BindingMode.OneWay);
        //	//btnLeft.SetBinding(ToolButton.ImageSourceNameProperty, new Binding("LeftIcon", BindingMode.OneWay, null, null, null, this));
        //	//Image leftImage = new Image{ Source = ImageSource.FromFile ("left.png"), Aspect = Aspect.Fill };

        //	Image logoImage = new Image 
        //	{ 
        //		Source = ImageSource.FromFile ("logo.png"), 
        //		Aspect = Aspect.AspectFit,
        //		HorizontalOptions = LayoutOptions.Center
        //	};

        //	btnEng = new Button {
        //		Text = "ENG",
        //		TextColor = Color.FromHex (Constants.SelectedFontColor), 
        //		BackgroundColor = Color.Transparent,
        //		FontSize = 12,
        //	};
        //	btnEng.Text = "ENG";
        //	btnEsp = new Button {
        //		Text = "ESP",
        //		TextColor = Color.FromHex (Constants.RegularFontColor), 
        //		BackgroundColor = Color.Transparent,
        //		FontSize = 12
        //	};
        //	btnEng.Clicked += btnLan_Clicked;
        //	btnEsp.Clicked += btnLan_Clicked;

        //	lblSignup = new Label {
        //		HorizontalTextAlignment = TextAlignment.Center,
        //		TextColor = Color.FromHex (Constants.TitleFontColor),
        //		FontSize = 33
        //	};
        //	lblSignup.SetBinding (Label.TextProperty, "Title", BindingMode.OneWay);

        //	lblUserRole = new Label {
        //		HorizontalTextAlignment = TextAlignment.Center,
        //		FontSize = 18,
        //		TextColor = Color.FromHex (Constants.RegularFontColor)
        //	};
        //	failedMCLabel = new TrukmanButton {
        //		Text = "MC# not found",
        //		FontSize = 18,
        //		BackgroundColor = Color.Transparent,
        //		TextColor = Color.White,
        //		Style = (Style)App.Current.Resources ["buttonTransparentEntry"]
        //	};

        //	Button btnEditMC = new Button { Style = (Style)App.Current.Resources ["buttonForEntryRadiusStyle"] };
        //	edtMC = new TrukmanEditor { 
        //		Style = (Style)App.Current.Resources ["entryRadiusStyle"]
        //	};

        //	btnSubmit = new TrukmanButton();

        //	RelativeLayout userInfoLayout = new RelativeLayout ();
        //	userInfoLayout.HorizontalOptions = LayoutOptions.FillAndExpand;
        //	userInfoLayout.Children.Add (btnEditMC, 
        //		Constraint.RelativeToParent (parent => 0),
        //		Constraint.RelativeToParent (parent => 0),
        //		Constraint.RelativeToParent (parent => parent.Width)
        //	);
        //	userInfoLayout.Children.Add (edtMC, 
        //		Constraint.RelativeToView (btnEditMC, (parent, View) => View.X + Constants.ViewsPadding / 2),
        //		Constraint.RelativeToView (btnEditMC, (parent, View) => View.Y),
        //		Constraint.RelativeToView (btnEditMC, (parent, View) => View.Width - Constants.ViewsPadding),
        //		Constraint.RelativeToView (btnEditMC, (parent, View) => View.Height)
        //	);

        //	failedMCLayout = new RelativeLayout();
        //	failedMCLayout.Children.Add(failedMCLabel,
        //		Constraint.RelativeToParent (parent => 0),
        //		Constraint.RelativeToParent (parent => 0),
        //		Constraint.RelativeToParent (parent => parent.Width)
        //	);

        //	btnSubmit.Clicked += buttonClicked;

        //	//busyIndicator.SetBinding (ActivityIndicator.IsRunningProperty, "IsBusy", BindingMode.TwoWay);

        //	stackLayout = new StackLayout {
        //		Spacing = Constants.StackLayoutDefaultSpacing,
        //		Padding = new Thickness(Constants.ViewsPadding),
        //		VerticalOptions = LayoutOptions.CenterAndExpand,
        //		HorizontalOptions = LayoutOptions.CenterAndExpand,
        //		Children = {
        //			userInfoLayout,
        //			btnSubmit
        //			//busyIndicator
        //		}
        //	};

        //	RelativeLayout relativeLayout = new RelativeLayout ();

        //	/*relativeLayout.Children.Add (backgroundImage, 
        //		Constraint.RelativeToParent (parent => 0),
        //		Constraint.RelativeToParent (parent => 0),
        //		Constraint.RelativeToParent (parent => parent.Width),
        //		Constraint.RelativeToParent (parent => parent.Height)
        //	);*/
        //	relativeLayout.Children.Add (lblSignup, 
        //		Constraint.RelativeToParent (parent => parent.Width / 2 - lblSignup.Width / 2),
        //		Constraint.RelativeToParent (parent => Constants.ViewsBottomPadding)
        //	);
        //	relativeLayout.Children.Add (btnLeft, 
        //		Constraint.RelativeToParent (parent => Constants.ViewsBottomPadding),
        //		Constraint.RelativeToParent (parent => Constants.ViewsBottomPadding),
        //		Constraint.RelativeToView (lblSignup, (parent, lblSignup) => lblSignup.Height / 2),
        //		Constraint.RelativeToView (lblSignup, (parent, lblSignup) => lblSignup.Height / 2)
        //	);
        //	relativeLayout.Children.Add (btnEsp,
        //		Constraint.RelativeToParent (parent => parent.Width - btnEsp.Width),
        //		Constraint.RelativeToParent (parent => 0),
        //		Constraint.RelativeToParent (parent => 50)
        //	);
        //	relativeLayout.Children.Add (btnEng, 
        //		Constraint.RelativeToView (btnEsp, (parent, view) => parent.Width - view.Width - btnEng.Width),
        //		Constraint.RelativeToParent (parent => 0),
        //		Constraint.RelativeToParent (parent => 50)
        //	);
        //	relativeLayout.Children.Add (logoImage,
        //		Constraint.RelativeToParent (parent => parent.Width / 2 - logoImage.Width / 2),
        //		Constraint.RelativeToView (lblSignup, (parent, view) => view.Y + view.Height + Constants.ViewsBottomPadding)
        //	);
        //	relativeLayout.Children.Add (lblUserRole,
        //		Constraint.RelativeToParent (parent => 0),
        //		Constraint.RelativeToView (logoImage, (parent, view) => view.Y + view.Height + Constants.ViewsBottomPadding),
        //		Constraint.RelativeToParent(parent => parent.Width)
        //	);
        //	relativeLayout.Children.Add (stackLayout,
        //		Constraint.RelativeToParent (parent => parent.Width / 2 - stackLayout.Width / 2),
        //		Constraint.RelativeToView (lblUserRole, (parent, view) => view.Y + view.Height),
        //		Constraint.RelativeToParent (parent => parent.Width)
        //	);
        //	/*relativeLayout.Children.Add (busyIndicator, 
        //		Constraint.RelativeToParent (parent => parent.Width / 2 - busyIndicator.Width / 2),
        //		Constraint.RelativeToView (stackLayout, (Parent, View) => View.Y + View.Height + Constants.ViewsBottomPadding)
        //	);*/

        //	/*relativeLayout.HorizontalOptions = LayoutOptions.CenterAndExpand;
        //	relativeLayout.VerticalOptions = LayoutOptions.CenterAndExpand;*/

        //	busyIndicator = new ActivityIndicator {
        //		HorizontalOptions = LayoutOptions.Center,
        //		VerticalOptions = LayoutOptions.Center
        //	};

        //	var pageContent = new Grid {
        //		HorizontalOptions = LayoutOptions.Fill,
        //		VerticalOptions = LayoutOptions.Fill,
        //		RowSpacing = 0,
        //		ColumnSpacing = 0
        //	};
        //	pageContent.Children.Add (relativeLayout);
        //	pageContent.Children.Add (busyIndicator);

        //	UpdateText ();

        //	//relativeLayout.ForceLayout ();
        //	return pageContent;
        //}

        protected override View CreateContent()
        {
            var titleBar = new TitleBar
            {
                LeftIcon = PlatformHelper.LeftImageSource
            };
            titleBar.SetBinding(TitleBar.TitleProperty, "Title", BindingMode.OneWay);
            titleBar.SetBinding(TitleBar.LeftCommandProperty, "PopPageCommand", BindingMode.OneWay);

            var image = new Image
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                HeightRequest = 100,
                WidthRequest = 100,
                Source = PlatformHelper.LogoImageSource
            };

            var imageContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Padding = new Thickness(10),
                Content = image
            };

            lblUserRole = new Label {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
            	HorizontalTextAlignment = TextAlignment.Center,
            	FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
            	TextColor = Color.FromHex (Constants.RegularFontColor)
            };

            var userRoleContent = new ContentView {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Padding = new Thickness(20, 0, 20, 10),
                Content = lblUserRole
            };

            edtMC = new AppEntry {
                PlaceholderColor = PlatformHelper.EntryPlaceholderColor
            };

            failedMCLabel = new AppLabel {
                Text = "MC# not found",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                TextColor = Color.White
            };

            failedMCContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Content = failedMCLabel,
                IsVisible = false
            };

            btnSubmit = new AppButton {
            };
            btnSubmit.Clicked += buttonClicked; // To do: Create and bind VisualCommand

            var infoContent = new Grid {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(20, 0, 20, 0),
                RowSpacing = 5,
                ColumnSpacing = 0,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                }
            };
            infoContent.Children.Add(edtMC, 0, 0);
            infoContent.Children.Add(failedMCContent, 0, 1);
            infoContent.Children.Add(btnSubmit, 0, 2);

            var contentGrid = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 0,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                }
            };
            contentGrid.Children.Add(imageContent, 0, 0);
            contentGrid.Children.Add(userRoleContent, 0, 1);
            contentGrid.Children.Add(infoContent, 0, 2);

            var scrollView = new ScrollView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Content = contentGrid
            };

            var pageContent = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 0,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                }
            };
            pageContent.Children.Add(titleBar, 0, 0);
            pageContent.Children.Add(scrollView, 0, 1);

            busyIndicator = new ActivityIndicator
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };
            busyIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy", BindingMode.TwoWay);

            var content = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 0
            };
            content.Children.Add(pageContent);
            content.Children.Add(busyIndicator);

            this.UpdateText();

            return content;
        }

        void btnLan_Clicked (object sender, EventArgs e)
		{
			if ((Button)sender == btnEng) {
				Localization.language = Localization.Languages.ENGLISH;
				btnEng.TextColor = Color.FromHex (Constants.SelectedFontColor);
				btnEsp.TextColor = Color.FromHex (Constants.RegularFontColor);
			} else if ((Button)sender == btnEsp) {
				Localization.language = Localization.Languages.ESPANIOL;
				btnEng.TextColor = Color.FromHex (Constants.RegularFontColor);
				btnEsp.TextColor = Color.FromHex (Constants.SelectedFontColor);;
			}

			UpdateText ();
		}

		void UpdateText()
		{
			lblUserRole.Text = Localization.getString(Localization.LocalStrings.OWNER_or_OPERATOR).ToUpper();
			//lblSignup.Text = Localization.getString (Localization.LocalStrings.SIGN_UP).ToUpper();
			edtMC.Placeholder = Localization.getString (Localization.LocalStrings.MC);
			btnSubmit.Text = Localization.getString (Localization.LocalStrings.BTN_SUBMIT);
		}

		async void buttonClicked (object sender, EventArgs e) 
		{
			try
			{
                failedMCContent.IsVisible = false;
				/*
				MCResponse data = new MCResponse{
					mcCode = edtMC.Text,
					success = true
				};*/

				this.IsBusy = true;
				MCInfo data = await MCQuery.VerifyMC(edtMC.Text);

				this.IsBusy = false;
				if(data.Success) {
					await Navigation.PushAsync(new SignUpCompanyPage(data));
				} else {
					throw new Exception("Didn't found company with this MC");
				}
			}
			catch(Exception exc) {
				failedAttempts++;
				if (failedAttempts >= maxFailAttempts)
				{
					await DisplayAlert("", 
						Localization.getString(Localization.LocalStrings.FAILED_OWNER_MC), 
						Localization.getString(Localization.LocalStrings.CONTINUE));
					await Navigation.PushAsync(new SignUpOwnerPage());
				}
				else if(failedAttempts == 1)
				{
                    // Insert before submit button
                    //stackLayout.Children.Insert(1, failedMCLayout);
                    failedMCContent.IsVisible = true;
				}
			}
		}
	}
}



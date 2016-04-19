﻿using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Trukman.ViewModels.Pages;
using KAS.Trukman.Views.Pages;
using Trukman.Helpers;
using KAS.Trukman.Controls;
using KAS.Trukman.Helpers;
using KAS.Trukman;
using Trukman.Interfaces;
using KAS.Trukman.Messages;

namespace Trukman
{
	public class SignUpCompanyPage : TrukmanPage
	{
		StackLayout stackLayout;

		AppEntry edtCompName;
		AppEntry edtDBA;
		AppEntry edtCompAddress;
		AppEntry edtPhone;
		AppEntry edtEmail;
		AppEntry edtFleetSize;

		Label lblSignup;
		Label lblUserRole;

		AppButton btnSubmit;
		AppLabel errorLabel;
        ContentView errorContent;

		Button btnEng;
		Button btnEsp;

		MCResponse companyData;
		ActivityIndicator busyIndicator;

		public SignUpCompanyPage ()
		{
			//SetupUI();

			this.BindingContext = new SignUpCompanyViewModel ();
		}

		public SignUpCompanyPage (MCResponse response) : this()
		{
			companyData = response;
			//SetupUI();
		}

        //void SetupUI() 

        //      protected override View CreateContent ()
        //{
        //	//Image backgroundImage = new Image{ Source = ImageSource.FromResource ("background.png"), Aspect = Aspect.Fill };
        //	var btnLeft = new ToolButton();
        //	btnLeft.ImageSourceName = PlatformHelper.LeftImageSource;
        //	btnLeft.SetBinding (ToolButton.CommandProperty, "ShowPrevPageCommand", BindingMode.OneWay);
        //	//Image leftImage = new Image{ Source = ImageSource.FromFile ("left.png"), Aspect = Aspect.Fill };
        //	//            Image logoImage = new Image{ Source = ImageSource.FromResource ("logo.png"), Aspect = Aspect.AspectFit };

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
        //		VerticalTextAlignment = TextAlignment.Start,
        //		HorizontalTextAlignment = TextAlignment.Center,
        //		TextColor = Color.FromHex (Constants.TitleFontColor),
        //		FontSize = 33
        //	};
        //	lblUserRole = new Label {
        //		HorizontalTextAlignment = TextAlignment.Center,
        //		FontSize = 18,
        //		TextColor = Color.FromHex (Constants.RegularFontColor)
        //	};

        //	edtCompName = new TrukmanEditor {
        //		//IsEnabled = false,
        //		Style = (Style)App.Current.Resources ["disabledEntryStyle"]
        //	};
        //	Button btnEdtCompName = new Button { Style = (Style)App.Current.Resources ["disabledButtonForEntryRadiusStyle"] };

        //	edtDBA = new TrukmanEditor { 
        //		//IsEnabled = false,
        //		TextColor = Color.Black,
        //		Style = (Style)App.Current.Resources ["disabledEntryStyle"]
        //	};
        //	Button btnEdtDBA = new Button { Style = (Style)App.Current.Resources ["disabledButtonForEntryRadiusStyle"] };

        //	edtCompAddress = new TrukmanEditor { Style = (Style)App.Current.Resources ["entryRadiusStyle"] };
        //	Button btnEdtCompAddress = new Button { Style = (Style)App.Current.Resources ["buttonForEntryRadiusStyle"] };

        //	edtPhone = new TrukmanEditor { Style = (Style)App.Current.Resources ["entryRadiusStyle"] };
        //	Button btnEdtPhone = new Button { Style = (Style)App.Current.Resources ["buttonForEntryRadiusStyle"] };

        //	edtEmail = new TrukmanEditor { Style = (Style)App.Current.Resources ["entryRadiusStyle"], Text = ""};
        //	Button btnEdtEmail = new Button { Style = (Style)App.Current.Resources ["buttonForEntryRadiusStyle"] };

        //	edtFleetSize = new TrukmanEditor { Style = (Style)App.Current.Resources ["entryRadiusStyle"], Text = "" };
        //	Button btnEdtFleetSize = new Button { Style = (Style)App.Current.Resources ["buttonForEntryRadiusStyle"] };

        //	errorLabel = new TrukmanButton {
        //		FontSize = 18,
        //		BackgroundColor = Color.Transparent,
        //		TextColor = Color.White,
        //		Style = (Style)App.Current.Resources["buttonTransparentEntry"] 
        //	};
        //	if (companyData.success)
        //	{
        //		edtCompName.Text = companyData.name;
        //		edtDBA.Text = companyData.DBA;
        //		edtCompAddress.Text = companyData.address;
        //		edtPhone.Text = companyData.phone;
        //	}

        //	RelativeLayout userInfoLayout = new RelativeLayout ();
        //	userInfoLayout.HorizontalOptions = LayoutOptions.FillAndExpand;
        //	userInfoLayout.Children.Add (btnEdtCompName, 
        //		Constraint.RelativeToParent (parent => 0),
        //		Constraint.RelativeToParent (parent => 0),
        //		Constraint.RelativeToParent (parent => parent.Width)
        //	);
        //	userInfoLayout.Children.Add (edtCompName, 
        //		Constraint.RelativeToView (btnEdtCompName, (parent, View) => View.X + Constants.ViewsPadding / 2),
        //		Constraint.RelativeToView (btnEdtCompName, (parent, View) => View.Y),
        //		Constraint.RelativeToView (btnEdtCompName, (parent, View) => View.Width - Constants.ViewsPadding),
        //		Constraint.RelativeToView (btnEdtCompName, (parent, View) => View.Height)
        //	);
        //	userInfoLayout.Children.Add (btnEdtDBA, 
        //		Constraint.RelativeToParent (parent => 0),
        //		Constraint.RelativeToView(btnEdtCompName, (parent, View) => View.Y + View.Height + Constants.ViewsBottomPadding),
        //		Constraint.RelativeToParent (parent => parent.Width)
        //	);
        //	userInfoLayout.Children.Add (edtDBA, 
        //		Constraint.RelativeToView (btnEdtDBA, (parent, View) => View.X + Constants.ViewsPadding / 2),
        //		Constraint.RelativeToView (btnEdtDBA, (parent, View) => View.Y),
        //		Constraint.RelativeToView (btnEdtDBA, (parent, View) => View.Width - Constants.ViewsPadding),
        //		Constraint.RelativeToView (btnEdtDBA, (parent, View) => View.Height)
        //	);
        //	userInfoLayout.Children.Add (btnEdtCompAddress, 
        //		Constraint.RelativeToParent (parent => 0),
        //		Constraint.RelativeToView(edtDBA, (parent, View) => View.Y + View.Height + Constants.ViewsBottomPadding),
        //		Constraint.RelativeToParent (parent => parent.Width)
        //	);
        //	userInfoLayout.Children.Add (edtCompAddress, 
        //		Constraint.RelativeToView (btnEdtCompAddress, (parent, View) => View.X + Constants.ViewsPadding / 2),
        //		Constraint.RelativeToView (btnEdtCompAddress, (parent, View) => View.Y),
        //		Constraint.RelativeToView (btnEdtCompAddress, (parent, View) => View.Width - Constants.ViewsPadding),
        //		Constraint.RelativeToView (btnEdtCompAddress, (parent, View) => View.Height)
        //	);
        //	userInfoLayout.Children.Add (btnEdtPhone, 
        //		Constraint.RelativeToParent (parent => 0),
        //		Constraint.RelativeToView(edtCompAddress, (parent, View) => View.Y + View.Height + Constants.ViewsBottomPadding),
        //		Constraint.RelativeToParent (parent => parent.Width)
        //	);
        //	userInfoLayout.Children.Add (edtPhone, 
        //		Constraint.RelativeToView (btnEdtPhone, (parent, View) => View.X + Constants.ViewsPadding / 2),
        //		Constraint.RelativeToView (btnEdtPhone, (parent, View) => View.Y),
        //		Constraint.RelativeToView (btnEdtPhone, (parent, View) => View.Width - Constants.ViewsPadding),
        //		Constraint.RelativeToView (btnEdtPhone, (parent, View) => View.Height)
        //	);
        //	userInfoLayout.Children.Add (btnEdtEmail, 
        //		Constraint.RelativeToParent (parent => 0),
        //		Constraint.RelativeToView(btnEdtPhone, (parent, View) => View.Y + View.Height + Constants.ViewsBottomPadding),
        //		Constraint.RelativeToParent (parent => parent.Width)
        //	);
        //	userInfoLayout.Children.Add (edtEmail, 
        //		Constraint.RelativeToView (btnEdtEmail, (parent, View) => View.X + Constants.ViewsPadding / 2),
        //		Constraint.RelativeToView (btnEdtEmail, (parent, View) => View.Y),
        //		Constraint.RelativeToView (btnEdtEmail, (parent, View) => View.Width - Constants.ViewsPadding),
        //		Constraint.RelativeToView (btnEdtEmail, (parent, View) => View.Height)
        //	);
        //	userInfoLayout.Children.Add (btnEdtFleetSize, 
        //		Constraint.RelativeToParent (parent => 0),
        //		Constraint.RelativeToView(edtEmail, (parent, View) => View.Y + View.Height + Constants.ViewsBottomPadding),
        //		Constraint.RelativeToParent (parent => parent.Width)
        //	);
        //	userInfoLayout.Children.Add (edtFleetSize, 
        //		Constraint.RelativeToView (btnEdtFleetSize, (parent, View) => View.X + Constants.ViewsPadding / 2),
        //		Constraint.RelativeToView (btnEdtFleetSize, (parent, View) => View.Y),
        //		Constraint.RelativeToView (btnEdtFleetSize, (parent, View) => View.Width - Constants.ViewsPadding),
        //		Constraint.RelativeToView (btnEdtFleetSize, (parent, View) => View.Height)
        //	);

        //	btnSubmit = new TrukmanButton {  };

        //	btnSubmit.Clicked += proceedPressed;

        //	stackLayout = new StackLayout {
        //		VerticalOptions = LayoutOptions.CenterAndExpand,
        //		Spacing = Constants.StackLayoutDefaultSpacing,
        //		Padding = new Thickness (Constants.ViewsPadding),
        //		Children = {
        //			userInfoLayout,
        //			btnSubmit
        //		}
        //	};

        //	RelativeLayout relativeLayout = new RelativeLayout ();

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
        //	relativeLayout.Children.Add (lblUserRole,
        //		Constraint.RelativeToParent (parent => 0),
        //		Constraint.RelativeToView (lblSignup, (parent, view) => view.Y + view.Height + Constants.ViewsBottomPadding),
        //		Constraint.RelativeToParent(parent => parent.Width)
        //	);
        //	relativeLayout.Children.Add (stackLayout,
        //		Constraint.RelativeToParent (parent => parent.Width / 2 - stackLayout.Width / 2),
        //		Constraint.RelativeToView (lblUserRole, (parent, view) => view.Y + view.Height - 20), // Get some space for error label
        //		Constraint.RelativeToParent (parent => parent.Width)
        //	);

        //	UpdateText();

        //	busyIndicator = new ActivityIndicator
        //	{
        //		VerticalOptions = LayoutOptions.Center,
        //		HorizontalOptions = LayoutOptions.Center,
        //	};

        //	var pageContent = new Grid {
        //		HorizontalOptions = LayoutOptions.Fill,
        //		VerticalOptions = LayoutOptions.Fill,
        //		RowSpacing = 0,
        //		ColumnSpacing = 0
        //	};

        //	pageContent.Children.Add(relativeLayout);
        //	pageContent.Children.Add(busyIndicator);

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

            lblUserRole = new Label
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                TextColor = Color.FromHex(Constants.RegularFontColor)
            };

            var userRoleContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Padding = new Thickness(20, 0, 20, 10),
                Content = lblUserRole
            };

            edtCompName = new AppEntry {
                PlaceholderColor = PlatformHelper.EntryPlaceholderColor
            };

            edtDBA = new AppEntry {
                PlaceholderColor = PlatformHelper.EntryPlaceholderColor
            };

            edtCompAddress = new AppEntry {
                PlaceholderColor = PlatformHelper.EntryPlaceholderColor
            };

            edtPhone = new AppEntry {
                PlaceholderColor = PlatformHelper.EntryPlaceholderColor
            };

            edtEmail = new AppEntry {
                PlaceholderColor = PlatformHelper.EntryPlaceholderColor
            };

            edtFleetSize = new AppEntry {
                PlaceholderColor = PlatformHelper.EntryPlaceholderColor
            };

            errorLabel = new AppLabel {
                Text = "MC# not found",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.White
            };

            errorContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Content = errorLabel,
                IsVisible = false
            };

            btnSubmit = new AppButton {
            };
            btnSubmit.Clicked += proceedPressed;

            var infoContent = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(20, 0, 20, 0),
                RowSpacing = 2,
                ColumnSpacing = 0,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                }
            };
            infoContent.Children.Add(edtCompName, 0, 0);
            infoContent.Children.Add(edtDBA, 0, 1);
            infoContent.Children.Add(edtCompAddress, 0, 2);
            infoContent.Children.Add(edtPhone, 0, 3);
            infoContent.Children.Add(edtEmail, 0, 4);
            infoContent.Children.Add(edtFleetSize, 0, 5);
            infoContent.Children.Add(errorContent, 0, 6);
            infoContent.Children.Add(btnSubmit, 0, 7);

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

            if (companyData.success)
            {
                edtCompName.Text = companyData.name;
                edtDBA.Text = companyData.DBA;
                edtCompAddress.Text = companyData.address;
                edtPhone.Text = companyData.phone;
            }

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

		bool isFieldsValid() 
		{
            errorLabel.Text = "";

			Regex emailTester = new Regex("^\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$");
			Regex fleetTester = new Regex("^+([0-9])+$");

			if (String.IsNullOrEmpty(edtPhone.Text))
			{
				errorLabel.Text = "Phone# cannot be blank";
			}
			else if ((String.IsNullOrEmpty(edtEmail.Text)) || (!emailTester.IsMatch(edtEmail.Text)))
			{
				errorLabel.Text = "Email is incorrect";
			}
			else if ((String.IsNullOrEmpty(edtFleetSize.Text)) || (!fleetTester.IsMatch(edtFleetSize.Text)))
			{
				errorLabel.Text = "Fleet should be a number";
			}

			return String.IsNullOrEmpty(errorLabel.Text);
		}

		void UpdateText()
		{
			lblUserRole.Text = Localization.getString(Localization.LocalStrings.OWNER_or_OPERATOR).ToUpper();
//			lblSignup.Text = Localization.getString (Localization.LocalStrings.SIGN_UP).ToUpper();
			edtDBA.Placeholder = Localization.getString(Localization.LocalStrings.DBA);
			edtCompAddress.Placeholder = Localization.getString(Localization.LocalStrings.PHYS_ADDRESS);
			edtCompName.Placeholder = Localization.getString(Localization.LocalStrings.NAME);
			edtPhone.Placeholder = Localization.getString(Localization.LocalStrings.PHONE);
			edtEmail.Placeholder = Localization.getString(Localization.LocalStrings.EMAIL);
			edtFleetSize.Placeholder = Localization.getString(Localization.LocalStrings.FLEET_SIZE);
			btnSubmit.Text = Localization.getString (Localization.LocalStrings.SIGN_UP);
		}

		async void proceedPressed (object sender, EventArgs e) 
		{
            errorContent.IsVisible = false;
			if (isFieldsValid())
			{
				busyIndicator.IsRunning = true;
				try
				{
					SettingsServiceHelper.SaveCompany(edtCompName.Text);

					await App.ServerManager.Register (companyData.mcCode, edtPhone.Text, UserRole.UserRoleOwner);
					await App.ServerManager.AddCompany(edtCompName.Text, edtDBA.Text, 
						edtCompAddress.Text, edtPhone.Text, edtEmail.Text, edtFleetSize.Text);
					App.ServerManager.StartTimerForRequest();

                    //					PopToRootPageMessage.Send();
                    ShowOwnerSignUpWelcomePageMessage.Send(edtCompName.Text);
                } catch (Exception exc) {
					ShowToastMessage.Send (exc.Message);
				}
				finally
				{
					busyIndicator.IsRunning = false;
				}

//				await Navigation.PushAsync(new HomePage());
			}
			else
			{
//				if (stackLayout.Children.Count == 2)
	//			{
                    // Inserting before submit button
                    //stackLayout.Children.Insert(1, errorLabel);
                    errorContent.IsVisible = true;
		//		}
			}
		}
	}
}

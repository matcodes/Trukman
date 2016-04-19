using KAS.Trukman.Controls;
using KAS.Trukman.Helpers;
using KAS.Trukman.Languages;
using KAS.Trukman.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using Trukman;
using Xamarin.Forms;

namespace KAS.Trukman.Views.Pages
{
    #region OwnerSignUpWelcomePage
    public class OwnerSignUpWelcomePage : TrukmanPage
    {
        public OwnerSignUpWelcomePage()
            : base()
        {
            this.BindingContext = new OwnerSignUpWelcomeViewModel();
        }

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

            var userRole = new Label
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                TextColor = Color.FromHex(Constants.RegularFontColor) // To do: declare color in PlatformHelper
            };
            userRole.SetBinding(Label.TextProperty, new Binding("SignUpUserRoleOwnerOperator", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var userRoleContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Padding = new Thickness(20, 0, 20, 10),
                Content = userRole
            };

            var welcome = new Label {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.White, // To do: declare color in PlatformHelper
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            };
            welcome.SetBinding(Label.TextProperty, "WelcomeText", BindingMode.TwoWay);

            var welcomeContent = new ContentView {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Padding = new Thickness(20, 0, 20, 10),
                Content = welcome
            };

            var continueButton = new AppButton {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center
            };
            continueButton.SetBinding(AppButton.TextProperty, new Binding("SignUpOwnerWelcomeContinueButtonText", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            continueButton.SetBinding(AppButton.CommandProperty, "ContinueCommand");

            var continueContent = new ContentView {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Padding = new Thickness(20, 0, 20, 0),
                Content = continueButton
            };

            var contentGrid = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 0,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                }
            };
            contentGrid.Children.Add(imageContent, 0, 0);
            contentGrid.Children.Add(userRoleContent, 0, 1);
            contentGrid.Children.Add(welcomeContent, 0, 2);
            contentGrid.Children.Add(continueContent, 0, 3);

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

            var busyIndicator = new ActivityIndicator
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

            return content;
        }

        public new OwnerSignUpWelcomeViewModel ViewModel
        {
            get { return (this.BindingContext as OwnerSignUpWelcomeViewModel); }
        }
    }
    #endregion
}

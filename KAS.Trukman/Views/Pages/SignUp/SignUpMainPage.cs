using KAS.Trukman.Controls;
using KAS.Trukman.Converters;
using KAS.Trukman.Data.Enums;
using KAS.Trukman.Helpers;
using KAS.Trukman.Languages;
using KAS.Trukman.ViewModels.Pages.SignUp;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Views.Pages.SignUp
{
    #region SignUpMainPage
    public class SignUpMainPage : TrukmanPage
    {
        public SignUpMainPage()
            : base()
        {
            this.BindingContext = new SignUpMainViewModel();
        }

        protected override View CreateContent()
        {
            var signUpLanguageToColorConverter = new SignUpLanguageToColorConverter();

            var title = new SignUpTitleBar
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                EnglishCommandParameter = SignUpLanguage.English,
                EspanolCommandParameter = SignUpLanguage.Espanol
            };
            title.SetBinding(SignUpTitleBar.EnglishLabelTextProperty, new Binding("SignUpEnglishLanguageLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            title.SetBinding(SignUpTitleBar.EnglishLabelTextColorProperty, new Binding("SelectedLanguage", BindingMode.OneWay, signUpLanguageToColorConverter, 0));
            title.SetBinding(SignUpTitleBar.EnglishCommandProperty, "EnglishLanguageCommand", BindingMode.OneWay);
            title.SetBinding(SignUpTitleBar.EspanolLabelTextProperty, new Binding("SignUpEspanolLanguageLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            title.SetBinding(SignUpTitleBar.EspanolLabelTextColorProperty, new Binding("SelectedLanguage", BindingMode.OneWay, signUpLanguageToColorConverter, 1));
            title.SetBinding(SignUpTitleBar.EspanolCommandProperty, "EspanolLanguageCommand", BindingMode.OneWay);

            var logo = new Image
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                HeightRequest = 180,
                WidthRequest = 180,
                Source = PlatformHelper.LogoImageSource
            };

            var logoContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(10, 10, 10, 10),
                Content = logo
            };

            var signUpMain = new Label
            {
                HorizontalOptions = LayoutOptions.Fill,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.Center,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                FontAttributes = FontAttributes.Bold,
                TextColor = PlatformHelper.SignUpTextColor
            };
            signUpMain.SetBinding(Label.TextProperty, new Binding("SignUpMainLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var signUpMainContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(20, 20, 20, 10),
                Content = signUpMain
            };

            var driver = new TappedLabel
            {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))
            };
            driver.SetBinding(TappedLabel.TextProperty, new Binding("SignUpDriverLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            driver.SetBinding(TappedLabel.TextColorProperty, new Binding("SelectedContext", BindingMode.OneWay, signUpLanguageToColorConverter, 1));
            driver.SetBinding(TappedLabel.TapCommandProperty, "DriverSignUpCommand");

            var dispatcher = new TappedLabel
            {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))
            };
            dispatcher.SetBinding(TappedLabel.TextProperty, new Binding("SignUpDispatcherLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            dispatcher.SetBinding(TappedLabel.TextColorProperty, new Binding("SelectedContext", BindingMode.OneWay, signUpLanguageToColorConverter, 2));
            dispatcher.SetBinding(TappedLabel.TapCommandProperty, "DispatcherSignUpCommand");

            var owner = new TappedLabel
            {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))
            };
            owner.SetBinding(TappedLabel.TextProperty, new Binding("SignUpOwnerLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            owner.SetBinding(TappedLabel.TextColorProperty, new Binding("SelectedContext", BindingMode.OneWay, signUpLanguageToColorConverter, 3));
            owner.SetBinding(TappedLabel.TapCommandProperty, "OwnerSignUpCommand");

            var contextContent = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(20, 10, 20, 10),
                Spacing = 10
            };
            contextContent.Children.Add(driver);
            contextContent.Children.Add(dispatcher);
            contextContent.Children.Add(owner);

            var content = new Grid
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 0,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                }
            };
            content.Children.Add(title, 0, 0);
            content.Children.Add(logoContent, 0, 1);
            content.Children.Add(signUpMainContent, 0, 2);
            content.Children.Add(contextContent, 0, 3);

            var busyIndicator = new ActivityIndicator
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };
            busyIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy", BindingMode.TwoWay);

            var pageContent = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 0
            };
            pageContent.Children.Add(content);
            pageContent.Children.Add(busyIndicator);

            return pageContent;
        }

        public new SignUpMainViewModel ViewModel
        {
            get { return (this.BindingContext as SignUpMainViewModel); }
        }
    }
    #endregion
}

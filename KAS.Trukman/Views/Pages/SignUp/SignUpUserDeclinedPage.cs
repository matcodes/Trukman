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
    #region SignUpUserDeclinedPage
    public class SignUpUserDeclinedPage : TrukmanPage
    {
        public SignUpUserDeclinedPage() : base()
        {
            this.BindingContext = new SignUpUserDeclinedViewModel();
        }

        protected override View CreateContent()
        {
            var signUpLanguageToColorConverter = new SignUpLanguageToColorConverter();

            var title = new SignUpTitleBar
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                LeftIcon = PlatformHelper.LeftImageSource,
                EnglishCommandParameter = SignUpLanguage.English,
                EspanolCommandParameter = SignUpLanguage.Espanol
            };
            title.SetBinding(SignUpTitleBar.LeftCommandProperty, "ShowPrevPageCommand", BindingMode.OneWay);
            title.SetBinding(SignUpTitleBar.TitleProperty, "Title", BindingMode.OneWay);
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
                HeightRequest = 100,
                WidthRequest = 100,
                Source = PlatformHelper.LogoImageSource
            };

            var logoContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(10, 10, 10, 10),
                Content = logo
            };

            var signUpUser = new Label
            {
                HorizontalOptions = LayoutOptions.Fill,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.Center,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                FontAttributes = FontAttributes.Bold,
                TextColor = PlatformHelper.SignUpTextColor
            };
            signUpUser.SetBinding(Label.TextProperty, "UserRole");

            var signUpUserContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(20, 20, 20, 10),
                Content = signUpUser
            };

            var lockImage = new Image
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                HeightRequest = 100,
                WidthRequest = 100,
                Source = PlatformHelper.LockRedImageSource
            };

            var lockContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(10, 10, 10, 10),
                Content = lockImage
            };

            var message = new Label
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                TextColor = PlatformHelper.SignUpTextColor
            };
            message.SetBinding(Label.TextProperty, "MessageText", BindingMode.OneWay);

            var welcomeContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(20, 10, 20, 10),
                Content = message
            };

            var cancelButton = new AppRoundButton
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center
            };
            cancelButton.SetBinding(AppRoundButton.TextProperty, new Binding("SignUpContinueButtonText", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            cancelButton.SetBinding(AppRoundButton.CommandProperty, "ContinueCommand");

            var continueContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(20, 0, 20, 10),
                Content = cancelButton
            };

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
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                }
            };
            content.Children.Add(title, 0, 0);
            content.Children.Add(logoContent, 0, 1);
            content.Children.Add(signUpUserContent, 0, 2);
            content.Children.Add(lockContent, 0, 3);
            content.Children.Add(welcomeContent, 0, 4);
            content.Children.Add(continueContent, 0, 5);

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

        public new SignUpUserDeclinedViewModel ViewModel
        {
            get { return (this.BindingContext as SignUpUserDeclinedViewModel); }
        }
    }
    #endregion
}

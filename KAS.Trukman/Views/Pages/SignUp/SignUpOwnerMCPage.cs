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
    #region SignUpOwnerMCPage
    public class SignUpOwnerMCPage : TrukmanPage
    {
        public SignUpOwnerMCPage()
            : base()
        {
            this.BindingContext = new SignUpOwnerMCViewModel();
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

            var signUpOwner = new Label
            {
                HorizontalOptions = LayoutOptions.Fill,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.Center,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                FontAttributes = FontAttributes.Bold,
                TextColor = PlatformHelper.SignUpTextColor
            };
            signUpOwner.SetBinding(Label.TextProperty, new Binding("SignUpOwnerLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var signUpOwnerContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(20, 20, 20, 10),
                Content = signUpOwner
            };

            var mcCode = new AppEntry
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                PlaceholderColor = PlatformHelper.EntryPlaceholderColor
            };
            mcCode.SetBinding(AppEntry.TextProperty, "MCCode", BindingMode.TwoWay);
            mcCode.SetBinding(AppEntry.PlaceholderProperty, new Binding("SignUpMCCodePlaceholder", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var mcCodeContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(20, 0, 20, 10),
                Content = mcCode
            };

            var submit = new AppButton
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center
            };
            submit.SetBinding(AppButton.TextProperty, new Binding("SignUpSubmitButtonText", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            submit.SetBinding(AppButton.CommandProperty, "SubmitCommand");

            var submitContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(20, 0, 20, 10),
                Content = submit
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
            content.Children.Add(signUpOwnerContent, 0, 2);
            content.Children.Add(mcCodeContent, 0, 3);
            content.Children.Add(submitContent, 0, 4);

            var busyIndicator = new ActivityIndicator
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };
            busyIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy", BindingMode.TwoWay);

            var popupBackground = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                BackgroundColor = Color.FromRgba(0, 0, 0, 120)
            };
            popupBackground.SetBinding(ContentView.IsVisibleProperty, "PopupVisible", BindingMode.OneWay);

            var pageContent = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 0
            };
            pageContent.Children.Add(content);
            pageContent.Children.Add(busyIndicator);
            pageContent.Children.Add(popupBackground);
            pageContent.Children.Add(this.CreatePopup());

            return pageContent;
        }

        private View CreatePopup()
        {
            var appBoxView = new AppBoxView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill
            };

            var message = new Label
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TextColor = Color.Black
            };
            message.SetBinding(Label.TextProperty, new Binding("SignUpMCExceededMessageText", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var messageContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Padding = new Thickness(20, 20, 20, 20),
                Content = message
            };

            var continueButton = new AppPopupButton
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Fill,
                AppStyle = AppButtonStyle.Normal
            };
            continueButton.SetBinding(AppPopupButton.TextProperty, new Binding("SignUpContinueButtonText", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            continueButton.SetBinding(AppPopupButton.CommandProperty, "ContinueCommand");

            var popupContent = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                HeightRequest = this.Height / 2,
                ColumnSpacing = 0,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) }
                },
            };
            popupContent.Children.Add(messageContent, 0, 0);
            popupContent.Children.Add(continueButton, 0, 1);

            var content = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                RowSpacing = 0,
                ColumnSpacing = 0,
                Padding = new Thickness(40, 40, 40, 40)
            };
            content.SetBinding(Grid.IsVisibleProperty, "PopupVisible", BindingMode.TwoWay);

            content.Children.Add(appBoxView);
            content.Children.Add(popupContent);

            return content;
        }

        public new SignUpOwnerMCViewModel ViewModel
        {
            get { return (this.BindingContext as SignUpOwnerMCViewModel); }
        }
    }
    #endregion
}

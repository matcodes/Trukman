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
        private Color lineColor = Color.FromHex("#808080");

        public SignUpOwnerMCPage(): base()
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

            var submit = new AppRoundButton
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center
            };
            submit.SetBinding(AppRoundButton.TextProperty, new Binding("SignUpSubmitButtonText", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            submit.SetBinding(AppRoundButton.CommandProperty, "SubmitCommand");

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

            var pageContent = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 0
            };
            pageContent.Children.Add(content);
            pageContent.Children.Add(busyIndicator);
            pageContent.Children.Add(this.CreatePopup());

            return pageContent;
        }

        private View CreatePopup()
        {
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
            };
            continueButton.SetBinding(AppPopupButton.TextProperty, new Binding("SignUpContinueButtonText", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            continueButton.SetBinding(AppPopupButton.CommandProperty, "ContinueCommand");

            var popupContent = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                HeightRequest = this.Height / 2,
                ColumnSpacing = 0,
                RowSpacing = 1,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) }
                }
            };
            popupContent.Children.Add(messageContent, 0, 0);
            popupContent.Children.Add(this.CreateLine(), 0, 1);
            popupContent.Children.Add(continueButton, 0, 2);

            var frameBackground = Color.FromHex("#F2F2F2");

            var frame = new Frame
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(0),
                BackgroundColor = frameBackground,
                CornerRadius = 8,
                OutlineColor = frameBackground,
                HasShadow = false,
                Content = popupContent
            };

            var background = Color.FromRgba(0, 0, 0, 120);

            var content = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                BackgroundColor = background,
                Padding = new Thickness(40, 40, 40, 40),
                Content = frame
            };
            content.SetBinding(ContentView.IsVisibleProperty, "PopupVisible", BindingMode.TwoWay);

            return content;
        }

        private View CreateLine()
        {
            var line = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Start,
                HeightRequest = 1,
                BackgroundColor = lineColor
            };

            return line;
        }

        public new SignUpOwnerMCViewModel ViewModel
        {
            get { return (this.BindingContext as SignUpOwnerMCViewModel); }
        }
    }
    #endregion
}

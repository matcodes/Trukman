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
    #region SignUpOwnerCompanyPage
    public class SignUpOwnerCompanyPage : TrukmanPage
    {
        private Color lineColor = Color.FromHex("#808080");
        private IndexToBoolConverter _indexToBoolConverter = new IndexToBoolConverter();

        public SignUpOwnerCompanyPage() : base()
        {
            this.BindingContext = new SignUpOwnerCompanyViewModel();
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

            var name = new AppEntry
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                PlaceholderColor = PlatformHelper.EntryPlaceholderColor
            };
            name.SetBinding(AppEntry.TextProperty, "Name", BindingMode.TwoWay);
            name.SetBinding(AppEntry.PlaceholderProperty, new Binding("SignUpCompanyNamePlaceholder", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var nameContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(20, 0, 20, 10),
                Content = name
            };

            var dba = new AppEntry
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                PlaceholderColor = PlatformHelper.EntryPlaceholderColor
            };
            dba.SetBinding(AppEntry.TextProperty, "DBA", BindingMode.TwoWay);
            dba.SetBinding(AppEntry.PlaceholderProperty, new Binding("SignUpCompanyDBAPlaceholder", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var dbaContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(20, 0, 20, 10),
                Content = dba
            };

            var address = new AppEntry
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                PlaceholderColor = PlatformHelper.EntryPlaceholderColor
            };
            address.SetBinding(AppEntry.TextProperty, "Address", BindingMode.TwoWay);
            address.SetBinding(AppEntry.PlaceholderProperty, new Binding("SignUpCompanyAddressPlaceholder", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var addressContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(20, 0, 20, 10),
                Content = address
            };

            var phone = new AppEntry
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                PlaceholderColor = PlatformHelper.EntryPlaceholderColor
            };
            phone.SetBinding(AppEntry.TextProperty, "Phone", BindingMode.TwoWay);
            phone.SetBinding(AppEntry.PlaceholderProperty, new Binding("SignUpCompanyPhonePlaceholder", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var phoneContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(20, 0, 20, 10),
                Content = phone
            };

            var email = new AppEntry
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                PlaceholderColor = PlatformHelper.EntryPlaceholderColor
            };
            email.SetBinding(AppEntry.TextProperty, "EMail", BindingMode.TwoWay);
            email.SetBinding(AppEntry.PlaceholderProperty, new Binding("SignUpCompanyEMailPlaceholder", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var emailContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(20, 0, 20, 10),
                Content = email
            };

            var fleetSize = new AppEntry
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                PlaceholderColor = PlatformHelper.EntryPlaceholderColor
            };
            fleetSize.SetBinding(AppEntry.TextProperty, "FleetSize", BindingMode.TwoWay);
            fleetSize.SetBinding(AppEntry.PlaceholderProperty, new Binding("SignUpCompanyFleetSizePlaceholder", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var fleetSizeContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(20, 0, 20, 10),
                Content = fleetSize
            };

            var submit = new AppRoundButton
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.End
            };
            submit.SetBinding(AppRoundButton.TextProperty, new Binding("SignUpSubmitButtonText", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            submit.SetBinding(AppRoundButton.CommandProperty, "SubmitCommand");
            submit.SetBinding(AppRoundButton.IsEnabledProperty, "IsSubmitEnabled");

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
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                }
            };
            content.Children.Add(logoContent, 0, 0);
            content.Children.Add(signUpOwnerContent, 0, 1);
            content.Children.Add(nameContent, 0, 2);
            content.Children.Add(dbaContent, 0, 3);
            content.Children.Add(addressContent, 0, 4);
            content.Children.Add(phoneContent, 0, 5);
            content.Children.Add(emailContent, 0, 6);
            content.Children.Add(fleetSizeContent, 0, 7);
            content.Children.Add(submitContent, 0, 8);

            var scroll = new ScrollView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill
            };
            scroll.Content = content;

            var scrollableContent = new Grid
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 0,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                }
            };
            scrollableContent.Children.Add(title, 0, 0);
            scrollableContent.Children.Add(scroll, 0, 1);

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
            pageContent.Children.Add(scrollableContent);
            pageContent.Children.Add(this.CreateEnterConfirmationCodePopup());
            pageContent.Children.Add(this.CreateConfirmationCodeAcceptedPopup());
            pageContent.Children.Add(busyIndicator);

            name.Completed += (sender, args) => {
                dba.Focus();
            };

            dba.Completed += (sender, args) => {
                address.Focus();
            };

            address.Completed += (sender, args) => {
                phone.Focus();
            };

            phone.Completed += (sender, args) => {
                email.Focus();
            };

            email.Completed += (sender, args) => {
                fleetSize.Focus();
            };

            return pageContent;
        }

        private View CreateHorizontalLine()
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

        private View CreateVerticalLine()
        {
            var line = new ContentView
            {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Fill,
                WidthRequest = 1,
                BackgroundColor = lineColor
            };

            return line;
        }

        private View CreateEnterConfirmationCodePopup()
        {
            var infoLabel = new Label
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TextColor = Color.Black
            };
            infoLabel.SetBinding(Label.TextProperty, new Binding("SignUpConfirmationCodeInfoLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var infoContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(10, 20, 10, 5),
                Content = infoLabel
            };

            var confirmationCode = new AppEntry
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(AppEntry)),
                PlaceholderColor = PlatformHelper.EntryPlaceholderColor
            };
            confirmationCode.SetBinding(AppEntry.TextProperty, "ConfirmationCode", BindingMode.TwoWay);
            confirmationCode.SetBinding(AppEntry.PlaceholderProperty, new Binding("SignUpConfirmationCodePlaceholder", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var confirmationCodeContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(20, 0, 20, 10),
                Content = confirmationCode
            };

            var sentLabel = new Label
            {
                HorizontalOptions = LayoutOptions.Fill,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = PlatformHelper.HomeLabelTextColor,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))
            };
            sentLabel.SetBinding(Label.TextProperty, new Binding("SignUpConfirmationCodeSentLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var sentFrame = new Frame
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Color.Transparent,
                CornerRadius = 25,
                OutlineColor = Color.Gray,
                HasShadow = false,
                IsClippedToBounds = true,
                Content = sentLabel,
                Padding = new Thickness(0, 10)
            };

            var sentLabelContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Start,
                Padding = new Thickness(20, 0, 20, 10),
                Content = sentFrame
            };
            sentLabelContent.SetBinding(ContentView.IsVisibleProperty, new Binding("ConfirmationState", BindingMode.OneWay, _indexToBoolConverter, 1, null));

            var invalidCodeLabel = new AppLabel
            {
                HorizontalOptions = LayoutOptions.Fill,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = PlatformHelper.HomeLabelTextColor,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))
            };
            invalidCodeLabel.SetBinding(Label.TextProperty, new Binding("SignUpConfirmationCodeInvalidCodeLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var invalidCodeFrame = new Frame
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Color.Transparent,
                CornerRadius = 25,
                OutlineColor = Color.IndianRed,
                HasShadow = false,
                IsClippedToBounds = true,
                Content = invalidCodeLabel,
                Padding = new Thickness(0, 10)
            };

            var invalidCodeContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Start,
                Padding = new Thickness(20, 0, 20, 10),
                Content = invalidCodeFrame
            };
            invalidCodeContent.SetBinding(ContentView.IsVisibleProperty, new Binding("ConfirmationState", BindingMode.OneWay, _indexToBoolConverter, 2, null));

            var submit = new AppRoundButton
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.End
            };
            submit.SetBinding(AppRoundButton.TextProperty, new Binding("SignUpSubmitButtonText", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            submit.SetBinding(AppRoundButton.CommandProperty, "SubmitCodeCommand");

            var submitContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(20, 0, 20, 10),
                Content = submit
            };

            var cancelButton = new AppPopupButton
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Fill,
            };
            cancelButton.SetBinding(AppPopupButton.TextProperty, new Binding("SignUpSelectCompanyCancelButtonText", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            cancelButton.SetBinding(AppPopupButton.CommandProperty, "CancelConfirmationCodeCommand");

            var resendButton = new AppPopupButton
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Fill,
            };
            resendButton.SetBinding(AppPopupButton.TextProperty, new Binding("SignUpConfirmationCodeResentButtonText", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            resendButton.SetBinding(AppPopupButton.CommandProperty, "ResendConfirmationCodeCommand");

            var buttons = new Grid
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 0,
                Padding = new Thickness(0, 1, 0, 0),
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                },
                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                }
            };
            buttons.Children.Add(this.CreateHorizontalLine(), 0, 3, 0, 1);
            buttons.Children.Add(cancelButton, 0, 1);
            buttons.Children.Add(this.CreateVerticalLine(), 1, 1);
            buttons.Children.Add(resendButton, 2, 1);

            var popupContent = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                RowSpacing = 0,
                ColumnSpacing = 0,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) }
                },
            };
            popupContent.Children.Add(infoContent, 0, 0);
            popupContent.Children.Add(confirmationCodeContent, 0, 1);
            popupContent.Children.Add(submitContent, 0, 2);
            popupContent.Children.Add(sentLabelContent, 0, 3);
            popupContent.Children.Add(invalidCodeContent, 0, 3);
            popupContent.Children.Add(buttons, 0, 4);

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
                Padding = new Thickness(40),
                Content = frame
            };
            content.SetBinding(Grid.IsVisibleProperty, "EnterConfirmationCodePopupVisible", BindingMode.TwoWay);
            content.PropertyChanged += (sender, e) =>
            {
                //if (this.ViewModel.EnterConfirmationCodePopupVisible)
                //    confirmationCode.SetFocus();
            };

            return content;
        }

        private View CreateConfirmationCodeAcceptedPopup()
        {
            var infoLabel = new AppLabel
            {
                HorizontalOptions = LayoutOptions.Fill,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = PlatformHelper.HomeLabelTextColor,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))
            };
            infoLabel.SetBinding(AppLabel.TextProperty, new Binding("SignUpConfirmationCodeSuccessLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var infoFrame = new Frame
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Color.Transparent,
                CornerRadius = 25,
                OutlineColor = Color.ForestGreen,
                HasShadow = false,
                IsClippedToBounds = true,
                Content = infoLabel,
                Padding = new Thickness(0, 10)
            };

            var infoContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Start,
                Padding = new Thickness(10, 20, 10, 20),
                Content = infoFrame
            };

            var continueButton = new AppPopupButton
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Fill
            };
            continueButton.SetBinding(AppPopupButton.TextProperty, new Binding("SignUpContinueButtonText", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            continueButton.SetBinding(AppPopupButton.CommandProperty, "ContinueCommand");

            var popupContent = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 0,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) }
                },
            };
            popupContent.Children.Add(infoContent, 0, 0);
            popupContent.Children.Add(this.CreateHorizontalLine(), 0, 1);
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
                Padding = new Thickness(40),
                Content = frame
            };
            content.SetBinding(Grid.IsVisibleProperty, "ConfirmationCodeAcceptedPopupVisible", BindingMode.TwoWay);

            return content;
        }

        public new SignUpOwnerCompanyViewModel ViewModel
        {
            get { return (this.BindingContext as SignUpOwnerCompanyViewModel); }
        }
    }
    #endregion
}

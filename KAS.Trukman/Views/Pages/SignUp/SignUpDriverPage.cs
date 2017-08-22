using KAS.Trukman.Controls;
using KAS.Trukman.Converters;
using KAS.Trukman.Data.Enums;
using KAS.Trukman.Helpers;
using KAS.Trukman.Languages;
using KAS.Trukman.ViewModels.Pages.SignUp;
using KAS.Trukman.Views.Lists;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Views.Pages.SignUp
{
    #region SignUpDriverPage
    public class SignUpDriverPage : TrukmanPage
    {
        private AppEntry _filter = null;

        public SignUpDriverPage()
            : base()
        {
            this.BindingContext = new SignUpDriverViewModel();
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
            signUpOwner.SetBinding(Label.TextProperty, new Binding("SignUpDriverLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var signUpOwnerContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(20, 20, 20, 10),
                Content = signUpOwner
            };

            var firstName = new AppEntry
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                PlaceholderColor = PlatformHelper.EntryPlaceholderColor
            };
            firstName.SetBinding(AppEntry.TextProperty, "FirstName", BindingMode.TwoWay);
            firstName.SetBinding(AppEntry.PlaceholderProperty, new Binding("SignUpDriverFirstNamePlaceholder", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var firstNameContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(20, 0, 20, 10),
                Content = firstName
            };

            var lastName = new AppEntry
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                PlaceholderColor = PlatformHelper.EntryPlaceholderColor
            };
            lastName.SetBinding(AppEntry.TextProperty, "LastName", BindingMode.TwoWay);
            lastName.SetBinding(AppEntry.PlaceholderProperty, new Binding("SignUpDriverLastNamePlaceholder", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var lastNameContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(20, 0, 20, 10),
                Content = lastName
            };

            var phone = new AppEntry
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                PlaceholderColor = PlatformHelper.EntryPlaceholderColor
            };
            phone.SetBinding(AppEntry.TextProperty, "Phone", BindingMode.TwoWay);
            phone.SetBinding(AppEntry.PlaceholderProperty, new Binding("SignUpDriverPhonePlaceholder", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

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
            email.SetBinding(AppEntry.PlaceholderProperty, new Binding("SignUpDriverEMailPlaceholder", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var emailContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(20, 0, 20, 10),
                Content = email
            };

            var company = new AppEntry
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                PlaceholderColor = PlatformHelper.EntryPlaceholderColor
            };
            company.SetBinding(AppEntry.TapCommandProperty, "SelectCompanyCommand");
            company.SetBinding(AppEntry.TextProperty, "CompanyName", BindingMode.TwoWay);
            company.SetBinding(AppEntry.PlaceholderProperty, new Binding("SignUpDriverCompanyNamePlaceholder", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var companyContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(20, 0, 20, 10),
                Content = company
            };

            var submit = new AppButton
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.End
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
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                }
            };
            content.Children.Add(logoContent, 0, 0);
            content.Children.Add(signUpOwnerContent, 0, 1);
            content.Children.Add(firstNameContent, 0, 2);
            content.Children.Add(lastNameContent, 0, 3);
            content.Children.Add(phoneContent, 0, 4);
            content.Children.Add(emailContent, 0, 5);
            content.Children.Add(companyContent, 0, 6);
            content.Children.Add(submitContent, 0, 7);

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

            var selectCompanyPopupBackground = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                BackgroundColor = Color.FromRgba(0, 0, 0, 120)
            };
            selectCompanyPopupBackground.SetBinding(ContentView.IsVisibleProperty, "SelectCompanyPopupVisible", BindingMode.OneWay);

            var enterConfirmationCodePopupBackground = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                BackgroundColor = Color.FromRgba(0, 0, 0, 120)
            };
            enterConfirmationCodePopupBackground.SetBinding(ContentView.IsVisibleProperty, "EnterConfirmationCodePopupVisible", BindingMode.OneWay);

            var confirmationCodeAcceptedPopupBackground = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                BackgroundColor = Color.FromRgba(0, 0, 0, 120)
            };
            confirmationCodeAcceptedPopupBackground.SetBinding(ContentView.IsVisibleProperty, "ConfirmationCodeAcceptedPopupVisible", BindingMode.OneWay);

            var pageContent = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 0
            };
            pageContent.Children.Add(scrollableContent);
            pageContent.Children.Add(selectCompanyPopupBackground);
            pageContent.Children.Add(this.CreateSelectCompanyPopup());
            pageContent.Children.Add(enterConfirmationCodePopupBackground);
            pageContent.Children.Add(this.CreateEnterConfirmationCodePopup());
            pageContent.Children.Add(confirmationCodeAcceptedPopupBackground);
            pageContent.Children.Add(this.CreateConfirmationCodeAcceptedPopup());
            pageContent.Children.Add(busyIndicator);

            firstName.Completed += (sender, args) =>
            {
                lastName.Focus();
            };

            lastName.Completed += (sender, args) =>
            {
                phone.Focus();
            };

            phone.Completed += (sender, args) =>
            {
                email.Focus();
            };

            email.Completed += (sender, args) =>
            {
                company.Focus();
            };

            return pageContent;
        }

        private View CreateSelectCompanyPopup()
        {
            var appBoxView = new AppBoxView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Color = Color.White
            };

            _filter = new AppEntry
            {
                HorizontalOptions = LayoutOptions.Fill,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                TextColor = Color.Black,
                PlaceholderColor = Color.Gray
            };
            _filter.SetBinding(Entry.TextProperty, "CompanyFilter", BindingMode.TwoWay);
            _filter.SetBinding(Entry.PlaceholderProperty, new Binding("SignUpSelectCompanySearchPlaceholder", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var filterContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(10, 20, 10, 5),
                Content = _filter
            };

            var companyListView = new CompanyListView
            {

            };
            companyListView.SetBinding(CompanyListView.ItemsSourceProperty, "Companies", BindingMode.TwoWay);
            companyListView.SetBinding(CompanyListView.SelectedItemProperty, "SelectedCompany", BindingMode.TwoWay);
            companyListView.SetBinding(CompanyListView.ItemClickCommandProperty, "SelectCompany");

            var cancelButton = new AppPopupButton
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Fill,
                //AppStyle = AppButtonStyle.Left
            };
            cancelButton.SetBinding(AppPopupButton.TextProperty, new Binding("SignUpSelectCompanyCancelButtonText", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            cancelButton.SetBinding(AppButton.CommandProperty, "SelectCompanyCancelCommand");

            var acceptButton = new AppPopupButton
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Fill,
                //AppStyle = AppButtonStyle.Right
            };
            acceptButton.SetBinding(AppPopupButton.TextProperty, new Binding("SignUpSelectCompanyAcceptButtonText", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            acceptButton.SetBinding(AppPopupButton.CommandProperty, "SelectCompanyAcceptCommand");

            var buttons = new Grid
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 0,
                Padding = new Thickness(0, 1, 0, 0),
                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                }
            };
            buttons.Children.Add(cancelButton, 0, 0);
            buttons.Children.Add(acceptButton, 1, 0);

            var popupContent = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                HeightRequest = this.Height / 2,
                RowSpacing = 0,
                ColumnSpacing = 0,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) }
                }
            };
            popupContent.Children.Add(filterContent, 0, 0);
            popupContent.Children.Add(companyListView, 0, 1);
            popupContent.Children.Add(buttons, 0, 2);

            var content = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Start,
                RowSpacing = 0,
                ColumnSpacing = 0,
                Padding = new Thickness(40, 40, 40, 0)
            };
            content.SetBinding(Grid.IsVisibleProperty, "SelectCompanyPopupVisible", BindingMode.TwoWay);
            content.PropertyChanged += (sender, e) =>
            {
                if (this.ViewModel.SelectCompanyPopupVisible)
                    _filter.Focus();
            };

            content.Children.Add(appBoxView);
            content.Children.Add(popupContent);

            return content;
        }

        private View CreateEnterConfirmationCodePopup()
        {
            var appBoxView = new AppBoxView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Color = Color.White
            };

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

            var submit = new AppButton
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.End
            };
            submit.SetBinding(AppButton.TextProperty, new Binding("SignUpSubmitButtonText", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            submit.SetBinding(AppButton.CommandProperty, "SubmitCodeCommand");

            var sentLabel = new AppLabel
            {
                HorizontalOptions = LayoutOptions.Fill,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor =  PlatformHelper.HomeLabelTextColor,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                AppLabelStyle = AppLabelStyles.InfoGray
            };
            sentLabel.SetBinding(Label.TextProperty, new Binding("SignUpConfirmationCodeSentLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var sentLabelContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(20, 0, 20, 10),
                Content = sentLabel
            };
            sentLabelContent.SetBinding(ContentView.IsVisibleProperty, "ConfirmationCodeSentVisible");

            var invalidCodeLabel = new AppLabel
            {
                HorizontalOptions = LayoutOptions.Fill,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = PlatformHelper.HomeLabelTextColor,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                AppLabelStyle = AppLabelStyles.Error
            };
            invalidCodeLabel.SetBinding(Label.TextProperty, new Binding("SignUpConfirmationCodeInvalidCodeLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var invalidCodeContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(20, 0, 20, 10),
                Content = invalidCodeLabel
            };
            invalidCodeContent.SetBinding(ContentView.IsVisibleProperty, "ConfirmationCodeInvalidVisible");

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
                //AppStyle = AppButtonStyle.Left
            };
            cancelButton.SetBinding(AppPopupButton.TextProperty, new Binding("SignUpSelectCompanyCancelButtonText", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            cancelButton.SetBinding(AppButton.CommandProperty, "CancelConfirmationCodeCommand");

            var resendButton = new AppPopupButton
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Fill,
                //AppStyle = AppButtonStyle.Right
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
                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                }
            };
            buttons.Children.Add(cancelButton, 0, 0);
            buttons.Children.Add(resendButton, 1, 0);

            var popupContent = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                HeightRequest = this.Height / 2,
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

            var content = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Start,
                RowSpacing = 0,
                ColumnSpacing = 0,
                Padding = new Thickness(40, 40, 40, 0)
            };
            content.SetBinding(Grid.IsVisibleProperty, "EnterConfirmationCodePopupVisible", BindingMode.TwoWay);
            content.PropertyChanged += (sender, e) =>
            {
                //if (this.ViewModel.EnterConfirmationCodePopupVisible)
                //    confirmationCode.Focus();
            };

            content.Children.Add(appBoxView);
            content.Children.Add(popupContent);

            return content;
        }

        private View CreateConfirmationCodeAcceptedPopup()
        {
            var appBoxView = new AppBoxView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Color = Color.White
            };

            var infoLabel = new AppLabel
            {
                HorizontalOptions = LayoutOptions.Fill,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = PlatformHelper.HomeLabelTextColor,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                AppLabelStyle = AppLabelStyles.Success
            };
            infoLabel.SetBinding(AppLabel.TextProperty, new Binding("SignUpConfirmationCodeSuccessLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var infoContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(10, 20, 10, 20),
                Content = infoLabel
            };

            var continueButton = new AppPopupButton
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Fill,
                //AppStyle = AppButtonStyle.Normal
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
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) }
                },
            };
            popupContent.Children.Add(infoContent, 0, 0);
            popupContent.Children.Add(continueButton, 0, 1);

            var content = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                RowSpacing = 0,
                ColumnSpacing = 0,
                Padding = new Thickness(40, 0, 40, 0)
            };
            content.SetBinding(Grid.IsVisibleProperty, "ConfirmationCodeAcceptedPopupVisible", BindingMode.TwoWay);

            content.Children.Add(appBoxView);
            content.Children.Add(popupContent);

            return content;
        }

        public new SignUpDriverViewModel ViewModel
        {
            get { return (this.BindingContext as SignUpDriverViewModel); }
        }
    }
    #endregion
}

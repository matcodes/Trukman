using KAS.Trukman.Controls;
using KAS.Trukman.Helpers;
using KAS.Trukman.Languages;
using KAS.Trukman.ViewModels.Pages.Owner;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Views.Pages.Owner
{
    #region OwnerAddBrokerPage
    public class OwnerAddBrokerPage : TrukmanPage
    {
        public OwnerAddBrokerPage() : base()
        {
            this.BindingContext = new OwnerAddBrokerViewModel();
        }

        protected override View CreateContent()
        {
            var titleBar = new TitleBar
            {
                RightIcon = PlatformHelper.HomeImageSource
            };
            titleBar.SetBinding(TitleBar.TitleProperty, "Title", BindingMode.OneWay);
            titleBar.SetBinding(TitleBar.LeftCommandProperty, "ShowMainMenuCommand", BindingMode.OneWay);
            titleBar.SetBinding(TitleBar.RightCommandProperty, "ShowHomePageCommand", BindingMode.OneWay);

            #region Name
            var name = new AppEntry
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                PlaceholderColor = PlatformHelper.EntryPlaceholderColor
            };
            name.SetBinding(AppEntry.TextProperty, "Name", BindingMode.TwoWay);
            name.SetBinding(AppEntry.PlaceholderProperty, new Binding("OwnerAddBrokerNamePlaceholder", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var nameContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(20, 0, 20, 10),
                Content = name
            };
            #endregion

            #region Email
            var email = new AppEntry
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                PlaceholderColor = PlatformHelper.EntryPlaceholderColor
            };
            email.SetBinding(AppEntry.TextProperty, "Email", BindingMode.TwoWay);
            email.SetBinding(AppEntry.PlaceholderProperty, new Binding("OwnerAddBrokerEmailPlaceholder", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var emailContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(20, 0, 20, 10),
                Content = email
            };
            #endregion

            #region Address
            var address = new AppEntry
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                PlaceholderColor = PlatformHelper.EntryPlaceholderColor
            };
            address.SetBinding(AppEntry.TextProperty, "Address", BindingMode.TwoWay);
            address.SetBinding(AppEntry.PlaceholderProperty, new Binding("OwnerAddBrokerAddressPlaceholder", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var addressContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(20, 0, 20, 10),
                Content = address
            };
            #endregion

            #region State
            var state = new AppEntry
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                PlaceholderColor = PlatformHelper.EntryPlaceholderColor
            };
            state.SetBinding(AppEntry.TextProperty, "State", BindingMode.TwoWay);
            state.SetBinding(AppEntry.PlaceholderProperty, new Binding("OwnerAddBrokerStatePlaceholder", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var stateContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(20, 0, 20, 10),
                Content = state
            };
            #endregion

            #region Zip
            var zip = new AppEntry
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                PlaceholderColor = PlatformHelper.EntryPlaceholderColor
            };
            zip.SetBinding(AppEntry.TextProperty, "ZIP", BindingMode.TwoWay);
            zip.SetBinding(AppEntry.PlaceholderProperty, new Binding("OwnerAddBrokerZipPlaceholder", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var zipContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(20, 0, 20, 10),
                Content = zip
            };
            #endregion

            #region Phone
            var phone = new AppEntry
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                PlaceholderColor = PlatformHelper.EntryPlaceholderColor
            };
            phone.SetBinding(AppEntry.TextProperty, "Phone", BindingMode.TwoWay);
            phone.SetBinding(AppEntry.PlaceholderProperty, new Binding("OwnerAddBrokerPhonePlaceholder", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var phoneContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(20, 0, 20, 10),
                Content = phone
            };
            #endregion

            #region Contact Title
            var contactTitle = new AppEntry
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                PlaceholderColor = PlatformHelper.EntryPlaceholderColor
            };
            contactTitle.SetBinding(AppEntry.TextProperty, "ContactTitle", BindingMode.TwoWay);
            contactTitle.SetBinding(AppEntry.PlaceholderProperty, new Binding("OwnerAddBrokerContactTitlePlaceholder", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var contactTitleContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(20, 0, 20, 10),
                Content = contactTitle
            };
            #endregion

            #region Contact Name
            var contactName= new AppEntry
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                PlaceholderColor = PlatformHelper.EntryPlaceholderColor
            };
            contactName.SetBinding(AppEntry.TextProperty, "ContactName", BindingMode.TwoWay);
            contactName.SetBinding(AppEntry.PlaceholderProperty, new Binding("OwnerAddBrokerContactNamePlaceholder", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var contactNameContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(20, 0, 20, 10),
                Content = contactName
            };
            #endregion

            #region Docket Number
            var docketNumber = new AppEntry
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                PlaceholderColor = PlatformHelper.EntryPlaceholderColor
            };
            docketNumber.SetBinding(AppEntry.TextProperty, "DocketNumber", BindingMode.TwoWay);
            docketNumber.SetBinding(AppEntry.PlaceholderProperty, new Binding("OwnerAddBrokerDocketNumberPlaceholder", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var docketNumberContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(20, 0, 20, 10),
                Content = docketNumber
            };
            #endregion

            var submit = new AppRoundButton
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.End
            };
            submit.SetBinding(AppRoundButton.TextProperty, new Binding("OwnerAddBrokerSubmitCommandText", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
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
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                }
            };
            content.Children.Add(nameContent, 0, 0);
            content.Children.Add(emailContent, 0, 1);
            content.Children.Add(addressContent, 0, 2);
            content.Children.Add(stateContent, 0, 3);
            content.Children.Add(zipContent, 0, 4);
            content.Children.Add(phoneContent, 0, 5);
            content.Children.Add(contactTitleContent, 0, 6);
            content.Children.Add(contactNameContent, 0, 7);
            content.Children.Add(docketNumberContent, 0, 8);
            content.Children.Add(submitContent, 0, 9);

            var busyIndicator = new ActivityIndicator
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };
            busyIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy", BindingMode.TwoWay);

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
            scrollableContent.Children.Add(titleBar, 0, 0);
            scrollableContent.Children.Add(scroll, 0, 1);

            var pageContent = new Grid
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 0
            };
            pageContent.Children.Add(scrollableContent);
            pageContent.Children.Add(busyIndicator);

            return pageContent;
        }

        public new OwnerAddBrokerViewModel ViewModel
        {
            get { return (this.BindingContext as OwnerAddBrokerViewModel); }
        }
    }
    #endregion
}

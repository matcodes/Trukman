using KAS.Trukman.Controls;
using KAS.Trukman.Helpers;
using KAS.Trukman.Languages;
using KAS.Trukman.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Views.Pages
{
    #region OwnerUserAuthorizationPage
    public class OwnerUserAuthorizationPage : TrukmanPage
    {
        private Color lineColor = Color.FromHex("#808080");

        public OwnerUserAuthorizationPage() : base()
        {
            this.BindingContext = new OwnerUserAuthorizationViewModel();
        }

        protected override View CreateContent()
        {
            var titleBar = new TitleBar
            {
            };
            titleBar.SetBinding(TitleBar.TitleProperty, "Title", BindingMode.OneWay);
            titleBar.SetBinding(TitleBar.LeftCommandProperty, "ShowOwnerMainMenuCommand", BindingMode.OneWay);

            var image = new Image
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                HeightRequest = 180,
                WidthRequest = 180,
                Source = PlatformHelper.LockNormalImageSource
            };

            var imageContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Padding = new Thickness(10),
                Content = image
            };

            var commonLabel = new Label
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = PlatformHelper.DriverAuthorizationTextColor,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            };
            commonLabel.SetBinding(Label.TextProperty, "CommonText");

            var commonContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Padding = new Thickness(20, 0, 20, 10),
                Content = commonLabel
            };

            var assignIDNumber = new AppEntry
            {
                PlaceholderColor = PlatformHelper.EntryPlaceholderColor
            };
            assignIDNumber.SetBinding(AppEntry.TextProperty, "AssignIDNumber", BindingMode.TwoWay);
            assignIDNumber.SetBinding(AppEntry.PlaceholderProperty, new Binding("UserAuthorizationAssignIDNumberPlaceholder", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var assignIDNumberContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Padding = new Thickness(20, 0, 20, 10),
                Content = assignIDNumber
            };

            var authorizeButton = new AppButton
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Fill,
                BackgroundColor = Color.Transparent
            };
            authorizeButton.SetBinding(AppButton.TextProperty, new Binding("UserAuthorizationAuthorizeButtonText", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            authorizeButton.SetBinding(AppButton.CommandProperty, "AuthorizeCommand");

            var declineButton = new AppButton
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Fill,
                BackgroundColor = Color.Transparent
            };
            declineButton.SetBinding(AppButton.TextProperty, new Binding("UserAuthorizationDeclineButtonText", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            declineButton.SetBinding(AppButton.CommandProperty, "DeclineCommand");

            var commandButtons = new Grid
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 1,
                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                }
            };
            commandButtons.Children.Add(authorizeButton, 0, 0);
            commandButtons.Children.Add(this.CreateVerticalLine(), 1, 0);
            commandButtons.Children.Add(declineButton, 2, 0);

            var frameBackground = Color.Black;

            var frame = new Frame
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Start,
                Padding = new Thickness(0),
                BackgroundColor = frameBackground,
                CornerRadius = 25,
                OutlineColor = frameBackground,
                HasShadow = false,
                IsClippedToBounds = true,
                Content = commandButtons
            };

            var commandContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Padding = new Thickness(20, 0, 20, 10),
                Content = frame
            };
            commandContent.SetBinding(ContentView.IsVisibleProperty, "RequestedButtonsVisible", BindingMode.OneWay);

            var contentGrid = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 0,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) }
                }
            };
            contentGrid.Children.Add(imageContent, 0, 0);
            contentGrid.Children.Add(commonContent, 0, 1);
            contentGrid.Children.Add(assignIDNumberContent, 0, 2);
            contentGrid.Children.Add(commandContent, 0, 3);

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

        public new OwnerUserAuthorizationViewModel ViewModel
        {
            get { return (this.BindingContext as OwnerUserAuthorizationViewModel); }
        }
    }
    #endregion
}

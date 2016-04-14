using KAS.Trukman.Controls;
using KAS.Trukman.Converters;
using KAS.Trukman.Helpers;
using KAS.Trukman.Languages;
using KAS.Trukman.ViewModels.Pages;
using KAS.Trukman.Views.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Views.Pages
{
    #region FuelAdvancePage
    public class FuelAdvancePage : TrukmanPage
    {
        public FuelAdvancePage()
            : base()
        {
            this.BindingContext = new FuelAdvanceViewModel();
        }

        protected override View CreateContent()
        {
            var titleBar = new TitleBar
            {
                RightIcon = PlatformHelper.HomeImageSource
            };
            titleBar.SetBinding(TitleBar.TitleProperty, "Title", BindingMode.OneWay);
            titleBar.SetBinding(TitleBar.LeftCommandProperty, "ShowHomePageCommand", BindingMode.OneWay);
            titleBar.SetBinding(TitleBar.RightCommandProperty, "ShowPrevPageCommand", BindingMode.OneWay);

            var fuelAdvanceStateToImageConverter = new FuelAdvanceStateToImageConverter();

            var fuelImage = new Image {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                HeightRequest = 180,
                WidthRequest = 180
            };
            fuelImage.SetBinding(Image.SourceProperty, new Binding("State", BindingMode.OneWay, fuelAdvanceStateToImageConverter));

            var fuelContent = new ContentView {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Content = fuelImage
            };

            var stateText = new Label {
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                TextColor = PlatformHelper.FuelAdvanceTextColor,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };
            stateText.SetBinding(Label.TextProperty, "StateText", BindingMode.OneWay);

            var stateContent = new ContentView {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(20, 0, 20, 0),
                Content = stateText
            };

            var requestButton = new AppButton {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Fill
            };
            requestButton.SetBinding(AppButton.TextProperty, new Binding("FuelAdvanceNoneRequestButtonText", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            requestButton.SetBinding(AppButton.CommandProperty, "RequestCommand");

            var requestContent = new ContentView {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(20, 0, 20, 0),
                Content = requestButton
            };
            requestContent.SetBinding(ContentView.IsVisibleProperty, "NoneButtonVisible", BindingMode.OneWay);

            var resendButton = new AppButton {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Fill,
                AppStyle = AppButtonStyle.Left
            };
            resendButton.SetBinding(AppButton.TextProperty, new Binding("FuelAdvanceReceivedResendButtonText", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            resendButton.SetBinding(AppButton.CommandProperty, "ResendCommand");

            var cancelButton = new AppButton {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Fill,
                AppStyle = AppButtonStyle.Right
            };
            cancelButton.SetBinding(AppButton.TextProperty, new Binding("FuelAdvanceReceivedCancelButtonText", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            cancelButton.SetBinding(AppButton.CommandProperty, "CancelCommand");

            var requestedButtons = new Grid {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 1,
                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                }
            };
            requestedButtons.Children.Add(resendButton, 0, 0);
            requestedButtons.Children.Add(cancelButton, 1, 0);

            var requestedContent = new ContentView {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Padding = new Thickness(20, 0, 20, 0),
                Content = requestedButtons
            };
            requestedContent.SetBinding(ContentView.IsVisibleProperty, "RequestedButtonsVisible", BindingMode.OneWay);

            var receivedStateInfoText = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TextColor = PlatformHelper.FuelAdvanceTextColor,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };
            receivedStateInfoText.SetBinding(Label.TextProperty, "StateInfoText", BindingMode.OneWay);

            var receivedStateInfoContent = new ContentView
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(20, 0, 20, 0),
                Content = receivedStateInfoText
            };
            receivedStateInfoContent.SetBinding(ContentView.IsVisibleProperty, "ReceivedTextInfoVisible", BindingMode.OneWay);

            var content = new Grid {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 0,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(3, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                }
            };
            content.Children.Add(titleBar, 0, 0);
            content.Children.Add(fuelContent, 0, 1);
            content.Children.Add(stateContent, 0, 2);
            content.Children.Add(requestContent, 0, 3);
            content.Children.Add(requestedContent, 0, 3);
            content.Children.Add(receivedStateInfoContent, 0, 3);

            var busyIndicator = new ActivityIndicator
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };
            busyIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy", BindingMode.TwoWay);

            var pageContent = new Grid {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 0
            };
            pageContent.Children.Add(content);
            pageContent.Children.Add(busyIndicator);

            return pageContent;
        }

        public new FuelAdvanceViewModel ViewModel
        {
            get { return (this.BindingContext as FuelAdvanceViewModel); }
        }
    }
    #endregion
}

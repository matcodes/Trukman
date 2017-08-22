using KAS.Trukman.Controls;
using KAS.Trukman.Converters;
using KAS.Trukman.Helpers;
using KAS.Trukman.Languages;
using KAS.Trukman.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Views.Pages
{
    #region AdvancesPage
    public class AdvancesPage : TrukmanPage
    {
        public AdvancesPage()
            : base()
        {
            this.BindingContext = new AdvancesViewModel();
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

            var grid = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 0,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                }
            };
            grid.Children.Add(titleBar, 0, 0);
            grid.Children.Add(this.CreateFuelView(), 0, 1);
            grid.Children.Add(this.CreateLumperView(), 0, 2);

            var busyIndicator = new ActivityIndicator
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };
            busyIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy", BindingMode.TwoWay);

            var lumperBusyIndicator = new ActivityIndicator
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };
            lumperBusyIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "LumperIsBusy", BindingMode.TwoWay);

            var content = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 0
            };
            content.Children.Add(grid);
            content.Children.Add(busyIndicator);
            content.Children.Add(lumperBusyIndicator);

            return content;
        }

        private View CreateFuelView()
        {
            var fuelAdvanceStateToImageConverter = new FuelAdvanceStateToImageConverter();

            var fuelImage = new Image
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                HeightRequest = 180,
                WidthRequest = 180
            };
            fuelImage.SetBinding(Image.SourceProperty, new Binding("FuelState", BindingMode.OneWay, fuelAdvanceStateToImageConverter));

            var fuelContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Padding = new Thickness(10),
                Content = fuelImage
            };

            var stateText = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                TextColor = PlatformHelper.FuelAdvanceTextColor,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };
            stateText.SetBinding(Label.TextProperty, "FuelStateText", BindingMode.OneWay);

            var stateContent = new ContentView
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(0, 10, 10, 10),
                Content = stateText
            };

            var grid = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 0,
                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                }
            };
            grid.Children.Add(fuelContent, 0, 0);
            grid.Children.Add(stateContent, 1, 0);

            var requestButton = new AppButton
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Fill
            };
            requestButton.SetBinding(AppButton.TextProperty, new Binding("FuelAdvanceNoneRequestButtonText", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            requestButton.SetBinding(AppButton.CommandProperty, "FuelRequestCommand");

            var requestContent = new ContentView
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(20, 0, 20, 0),
                Content = requestButton
            };
            requestContent.SetBinding(ContentView.IsVisibleProperty, "FuelNoneButtonVisible", BindingMode.OneWay);

            var resendButton = new AppButton
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Fill,
                //AppStyle = AppButtonStyle.Left
            };
            resendButton.SetBinding(AppButton.TextProperty, new Binding("FuelAdvanceReceivedResendButtonText", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            resendButton.SetBinding(AppButton.CommandProperty, "FuelResendCommand");

            var cancelButton = new AppButton
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Fill,
                //AppStyle = AppButtonStyle.Right
            };
            cancelButton.SetBinding(AppButton.TextProperty, new Binding("FuelAdvanceReceivedCancelButtonText", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            cancelButton.SetBinding(AppButton.CommandProperty, "FuelCancelCommand");

            var requestedButtons = new Grid
            {
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

            var requestedContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Padding = new Thickness(20, 0, 20, 0),
                Content = requestedButtons
            };
            requestedContent.SetBinding(ContentView.IsVisibleProperty, "FuelRequestedButtonsVisible", BindingMode.OneWay);

            var receivedStateInfoText = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TextColor = PlatformHelper.FuelAdvanceTextColor,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };
            receivedStateInfoText.SetBinding(Label.TextProperty, "FuelStateInfoText", BindingMode.OneWay);

            var receivedStateInfoContent = new ContentView
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(20, 0, 20, 0),
                Content = receivedStateInfoText
            };
            receivedStateInfoContent.SetBinding(ContentView.IsVisibleProperty, "FuelReceivedTextInfoVisible", BindingMode.OneWay);

            var content = new Grid
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
            content.Children.Add(grid, 0, 0);
            content.Children.Add(requestContent, 0, 1);
            content.Children.Add(requestedContent, 0, 1);
            content.Children.Add(receivedStateInfoContent, 0, 1);

            return content;
        }

        private View CreateLumperView()
        {
            var lumperStateToImageConverter = new LumperStateToImageConverter();

            var lumperImage = new Image
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                HeightRequest = 180,
                WidthRequest = 180
            };
            lumperImage.SetBinding(Image.SourceProperty, new Binding("LumperState", BindingMode.OneWay, lumperStateToImageConverter));

            var lumperContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Padding = new Thickness(10),
                Content = lumperImage
            };

            var stateText = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                TextColor = PlatformHelper.FuelAdvanceTextColor,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };
            stateText.SetBinding(Label.TextProperty, "LumperStateText", BindingMode.OneWay);

            var stateContent = new ContentView
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(0, 10, 10, 10),
                Content = stateText
            };

            var grid = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 0,
                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                }
            };
            grid.Children.Add(lumperContent, 0, 0);
            grid.Children.Add(stateContent, 1, 0);

            var requestButton = new AppButton
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Fill
            };
            requestButton.SetBinding(AppButton.TextProperty, new Binding("LumperNoneRequestButtonText", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            requestButton.SetBinding(AppButton.CommandProperty, "LumperRequestCommand");

            var requestContent = new ContentView
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(20, 0, 20, 0),
                Content = requestButton
            };
            requestContent.SetBinding(ContentView.IsVisibleProperty, "LumperNoneButtonVisible", BindingMode.OneWay);

            var resendButton = new AppButton
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Fill,
                //AppStyle = AppButtonStyle.Left
            };
            resendButton.SetBinding(AppButton.TextProperty, new Binding("LumperReceivedResendButtonText", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            resendButton.SetBinding(AppButton.CommandProperty, "LumperResendCommand");

            var cancelButton = new AppButton
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Fill,
                //AppStyle = AppButtonStyle.Right
            };
            cancelButton.SetBinding(AppButton.TextProperty, new Binding("LumperReceivedCancelButtonText", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            cancelButton.SetBinding(AppButton.CommandProperty, "LumperCancelCommand");

            var requestedButtons = new Grid
            {
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

            var requestedContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Padding = new Thickness(20, 0, 20, 0),
                Content = requestedButtons
            };
            requestedContent.SetBinding(ContentView.IsVisibleProperty, "LumperRequestedButtonsVisible", BindingMode.OneWay);

            var receivedStateInfoText = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TextColor = PlatformHelper.FuelAdvanceTextColor,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };
            receivedStateInfoText.SetBinding(Label.TextProperty, "LumperStateInfoText", BindingMode.OneWay);

            var receivedStateInfoContent = new ContentView
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(20, 0, 20, 0),
                Content = receivedStateInfoText
            };
            receivedStateInfoContent.SetBinding(ContentView.IsVisibleProperty, "LumperReceivedTextInfoVisible", BindingMode.OneWay);

            var content = new Grid
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
            content.Children.Add(grid, 0, 0);
            content.Children.Add(requestContent, 0, 1);
            content.Children.Add(requestedContent, 0, 1);
            content.Children.Add(receivedStateInfoContent, 0, 1);

            return content;
        }

        public new AdvancesViewModel ViewModel
        {
            get { return (this.BindingContext as AdvancesViewModel); }
        }
    }
    #endregion
}

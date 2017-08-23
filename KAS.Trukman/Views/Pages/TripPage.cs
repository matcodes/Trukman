using KAS.Trukman.Controls;
using KAS.Trukman.Converters;
using KAS.Trukman.Helpers;
using KAS.Trukman.Languages;
using KAS.Trukman.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace KAS.Trukman.Views.Pages
{
    #region TripPage
    public class TripPage : TrukmanPage
    {
        private Map _map = null;

        private TripViewModel _viewModel = null;
        private Color lineColor = Color.FromHex("#808080");

        public TripPage() : base()
        {
            _viewModel = new TripViewModel();
            _viewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "ContractorPosition")
                    this.MapLocateAddress(_map, this.ViewModel.ContractorPosition);
            };

            this.BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
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

            var tripContractorItemsToColorConverter = new TripContractorItemsToColorConverter();

            var shipperTitleLabel = new TappedLabel
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                TapCommandParameter = 0
            };
            shipperTitleLabel.SetBinding(TappedLabel.TextProperty, new Binding("TripShipperTitleLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            shipperTitleLabel.SetBinding(TappedLabel.TextColorProperty, new Binding("SelectedItem", BindingMode.OneWay, tripContractorItemsToColorConverter, 0));
            shipperTitleLabel.SetBinding(TappedLabel.TapCommandProperty, "SelectItemCommand");

            var showShipperPosition = new ToolButton
            {
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center,
                ImageSourceName = PlatformHelper.LocationImageSource,
                WidthRequest = 48,
                HeightRequest = 48
            };
            showShipperPosition.SetBinding(ToolButton.CommandProperty, "ShowShipperLocationCommand");

            var shipperTitleContent = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(0, 0, 0, 0),
                ColumnSpacing = 0,
                RowSpacing = 0
            };
            shipperTitleContent.Children.Add(shipperTitleLabel);
            shipperTitleContent.Children.Add(showShipperPosition);

            //var shipperNameLabel = new TappedLabel
            //{
            //    FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
            //    TapCommandParameter = 0
            //};
            //shipperNameLabel.SetBinding(TappedLabel.TextProperty, new Binding("ContractorPageNameLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            //shipperNameLabel.SetBinding(TappedLabel.TextColorProperty, new Binding("SelectedItem", BindingMode.OneWay, tripContractorItemsToColorConverter, 0));
            //shipperNameLabel.SetBinding(TappedLabel.TapCommandProperty, "SelectItemCommand");

            var shipperNameValue = new TappedLabel
            {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TapCommandParameter = 0
            };
            shipperNameValue.SetBinding(TappedLabel.TextProperty, "Shipper.Name");
            shipperNameValue.SetBinding(TappedLabel.TextColorProperty, new Binding("SelectedItem", BindingMode.OneWay, tripContractorItemsToColorConverter, 0));
            shipperNameValue.SetBinding(TappedLabel.TapCommandProperty, "SelectItemCommand");

            var shipperName = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(0, 2, 0, 2),
                Spacing = 5
            };
            //shipperName.Children.Add(shipperNameLabel);
            shipperName.Children.Add(shipperNameValue);

            var shipperPhoneLabel = new TappedLabel
            {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TapCommandParameter = 0
            };
            shipperPhoneLabel.SetBinding(TappedLabel.TextProperty, new Binding("ContractorPagePhoneLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            shipperPhoneLabel.SetBinding(TappedLabel.TextColorProperty, new Binding("SelectedItem", BindingMode.OneWay, tripContractorItemsToColorConverter, 0));
            shipperPhoneLabel.SetBinding(TappedLabel.TapCommandProperty, "SelectItemCommand");

            var shipperPhoneValue = new TappedLabel
            {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TapCommandParameter = 0
            };
            shipperPhoneValue.SetBinding(TappedLabel.TextProperty, "Shipper.Phone");
            shipperPhoneValue.SetBinding(TappedLabel.TextColorProperty, new Binding("SelectedItem", BindingMode.OneWay, tripContractorItemsToColorConverter, 0));
            shipperPhoneValue.SetBinding(TappedLabel.TapCommandProperty, "SelectItemCommand");

            var shipperPhone = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(0, 2, 0, 2),
                Spacing = 5
            };
            shipperPhone.Children.Add(shipperPhoneLabel);
            shipperPhone.Children.Add(shipperPhoneValue);

            var shipperFaxLabel = new TappedLabel
            {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TapCommandParameter = 0
            };
            shipperFaxLabel.SetBinding(TappedLabel.TextProperty, new Binding("ContractorPageFaxLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            shipperFaxLabel.SetBinding(TappedLabel.TextColorProperty, new Binding("SelectedItem", BindingMode.OneWay, tripContractorItemsToColorConverter, 0));
            shipperFaxLabel.SetBinding(TappedLabel.TapCommandProperty, "SelectItemCommand");

            var shipperFaxValue = new TappedLabel
            {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TapCommandParameter = 0
            };
            shipperFaxValue.SetBinding(TappedLabel.TextProperty, "Shipper.Fax");
            shipperFaxValue.SetBinding(TappedLabel.TextColorProperty, new Binding("SelectedItem", BindingMode.OneWay, tripContractorItemsToColorConverter, 0));
            shipperFaxValue.SetBinding(TappedLabel.TapCommandProperty, "SelectItemCommand");

            var shipperFax = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(0, 2, 0, 2),
                Spacing = 5
            };
            shipperFax.Children.Add(shipperFaxLabel);
            shipperFax.Children.Add(shipperFaxValue);

            var shipperPhoneFaxLineContent = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                ColumnSpacing = 0,
                RowSpacing = 0,
                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                }
            };
            shipperPhoneFaxLineContent.Children.Add(shipperPhone, 0, 0);
            shipperPhoneFaxLineContent.Children.Add(shipperFax, 1, 0);

            //var shipperAddressLabel = new TappedLabel
            //{
            //    FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
            //    TapCommandParameter = 0
            //};
            //shipperAddressLabel.SetBinding(TappedLabel.TextProperty, new Binding("ContractorPageAddressLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            //shipperAddressLabel.SetBinding(TappedLabel.TextColorProperty, new Binding("SelectedItem", BindingMode.OneWay, tripContractorItemsToColorConverter, 0));
            //shipperAddressLabel.SetBinding(TappedLabel.TapCommandProperty, "SelectItemCommand");

            var shipperAddressValue = new TappedLabel
            {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TapCommandParameter = 0
            };
            shipperAddressValue.SetBinding(TappedLabel.TextProperty, "Shipper.Address");
            shipperAddressValue.SetBinding(TappedLabel.TextColorProperty, new Binding("SelectedItem", BindingMode.OneWay, tripContractorItemsToColorConverter, 0));
            shipperAddressValue.SetBinding(TappedLabel.TapCommandProperty, "SelectItemCommand");

            var shipperAddress = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(0, 2, 0, 2),
                Spacing = 5
            };
            //shipperAddress.Children.Add(shipperAddressLabel);
            shipperAddress.Children.Add(shipperAddressValue);

            var shipperSpecialInstruction = new TappedLabel
            {
                HorizontalOptions = LayoutOptions.Center,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                TapCommandParameter = 0
            };
            shipperSpecialInstruction.SetBinding(TappedLabel.TextProperty, new Binding("TripSpecialInstructionLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            shipperSpecialInstruction.SetBinding(TappedLabel.TextColorProperty, new Binding("SelectedItem", BindingMode.OneWay, tripContractorItemsToColorConverter, 0));
            shipperSpecialInstruction.SetBinding(TappedLabel.TapCommandProperty, "ShipperSpecialInstructionCommand");

            var shipperSpecialInstructionContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(0, 5, 0, 5),
                Content = shipperSpecialInstruction
            };

            var receiverTitleLabel = new TappedLabel
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                TapCommandParameter = 1
            };
            receiverTitleLabel.SetBinding(TappedLabel.TextProperty, new Binding("TripReceiverTitleLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            receiverTitleLabel.SetBinding(TappedLabel.TextColorProperty, new Binding("SelectedItem", BindingMode.OneWay, tripContractorItemsToColorConverter, 1));
            receiverTitleLabel.SetBinding(TappedLabel.TapCommandProperty, "SelectItemCommand");

            var showReceiverPosition = new ToolButton
            {
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center,
                ImageSourceName = PlatformHelper.LocationImageSource,
                WidthRequest = 48,
                HeightRequest = 48
            };
            showReceiverPosition.SetBinding(ToolButton.CommandProperty, "ShowReceiverLocationCommand");

            var receiverTitleContent = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(0, 0, 0, 0),
                RowSpacing = 0,
                ColumnSpacing = 0
            };
            receiverTitleContent.Children.Add(receiverTitleLabel);
            receiverTitleContent.Children.Add(showReceiverPosition);

            //var receiverNameLabel = new TappedLabel
            //{
            //    FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
            //    TapCommandParameter = 1
            //};
            //receiverNameLabel.SetBinding(TappedLabel.TextProperty, new Binding("ContractorPageNameLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            //receiverNameLabel.SetBinding(TappedLabel.TextColorProperty, new Binding("SelectedItem", BindingMode.OneWay, tripContractorItemsToColorConverter, 1));
            //receiverNameLabel.SetBinding(TappedLabel.TapCommandProperty, "SelectItemCommand");

            var receiverNameValue = new TappedLabel
            {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TapCommandParameter = 1
            };
            receiverNameValue.SetBinding(TappedLabel.TextProperty, "Receiver.Name");
            receiverNameValue.SetBinding(TappedLabel.TextColorProperty, new Binding("SelectedItem", BindingMode.OneWay, tripContractorItemsToColorConverter, 1));
            receiverNameValue.SetBinding(TappedLabel.TapCommandProperty, "SelectItemCommand");

            var receiverName = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(0, 2, 0, 2),
                Spacing = 5
            };
            //receiverName.Children.Add(receiverNameLabel);
            receiverName.Children.Add(receiverNameValue);

            var receiverPhoneLabel = new TappedLabel
            {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TapCommandParameter = 1
            };
            receiverPhoneLabel.SetBinding(TappedLabel.TextProperty, new Binding("ContractorPagePhoneLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            receiverPhoneLabel.SetBinding(TappedLabel.TextColorProperty, new Binding("SelectedItem", BindingMode.OneWay, tripContractorItemsToColorConverter, 1));
            receiverPhoneLabel.SetBinding(TappedLabel.TapCommandProperty, "SelectItemCommand");

            var receiverPhoneValue = new TappedLabel
            {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TapCommandParameter = 1
            };
            receiverPhoneValue.SetBinding(TappedLabel.TextProperty, "Receiver.Phone");
            receiverPhoneValue.SetBinding(TappedLabel.TextColorProperty, new Binding("SelectedItem", BindingMode.OneWay, tripContractorItemsToColorConverter, 1));
            receiverPhoneValue.SetBinding(TappedLabel.TapCommandProperty, "SelectItemCommand");

            var receiverPhone = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(0, 2, 0, 2),
                Spacing = 5
            };
            receiverPhone.Children.Add(receiverPhoneLabel);
            receiverPhone.Children.Add(receiverPhoneValue);

            var receiverFaxLabel = new TappedLabel
            {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TapCommandParameter = 1
            };
            receiverFaxLabel.SetBinding(TappedLabel.TextProperty, new Binding("ContractorPageFaxLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            receiverFaxLabel.SetBinding(TappedLabel.TextColorProperty, new Binding("SelectedItem", BindingMode.OneWay, tripContractorItemsToColorConverter, 1));
            receiverFaxLabel.SetBinding(TappedLabel.TapCommandProperty, "SelectItemCommand");

            var receiverFaxValue = new TappedLabel
            {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TapCommandParameter = 1
            };
            receiverFaxValue.SetBinding(TappedLabel.TextProperty, "Receiver.Fax");
            receiverFaxValue.SetBinding(TappedLabel.TextColorProperty, new Binding("SelectedItem", BindingMode.OneWay, tripContractorItemsToColorConverter, 1));
            receiverFaxValue.SetBinding(TappedLabel.TapCommandProperty, "SelectItemCommand");

            var receiverFax = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(0, 2, 0, 2),
                Spacing = 5
            };
            receiverFax.Children.Add(receiverFaxLabel);
            receiverFax.Children.Add(receiverFaxValue);

            var receiverPhoneFaxLineContent = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 0,
                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                }
            };
            receiverPhoneFaxLineContent.Children.Add(receiverPhone, 0, 0);
            receiverPhoneFaxLineContent.Children.Add(receiverFax, 1, 0);

            //var receiverAddressLabel = new TappedLabel
            //{
            //    FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
            //    TapCommandParameter = 1
            //};
            //receiverAddressLabel.SetBinding(TappedLabel.TextProperty, new Binding("ContractorPageAddressLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            //receiverAddressLabel.SetBinding(TappedLabel.TextColorProperty, new Binding("SelectedItem", BindingMode.OneWay, tripContractorItemsToColorConverter, 1));
            //receiverAddressLabel.SetBinding(TappedLabel.TapCommandProperty, "SelectItemCommand");

            var receiverAddressValue = new TappedLabel
            {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TapCommandParameter = 1
            };
            receiverAddressValue.SetBinding(Label.TextProperty, "Receiver.Address");
            receiverAddressValue.SetBinding(TappedLabel.TextColorProperty, new Binding("SelectedItem", BindingMode.OneWay, tripContractorItemsToColorConverter, 1));
            receiverAddressValue.SetBinding(TappedLabel.TapCommandProperty, "SelectItemCommand");

            var receiverAddress = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(0, 2, 0, 2),
                Spacing = 5
            };
            //receiverAddress.Children.Add(receiverAddressLabel);
            receiverAddress.Children.Add(receiverAddressValue);

            var receiverSpecialInstruction = new TappedLabel
            {
                HorizontalOptions = LayoutOptions.Center,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                TapCommandParameter = 0
            };
            receiverSpecialInstruction.SetBinding(TappedLabel.TextProperty, new Binding("TripSpecialInstructionLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            receiverSpecialInstruction.SetBinding(TappedLabel.TextColorProperty, new Binding("SelectedItem", BindingMode.OneWay, tripContractorItemsToColorConverter, 1));
            receiverSpecialInstruction.SetBinding(TappedLabel.TapCommandProperty, "ReceiverSpecialInstructionCommand");

            var receiverSpecialInstructionContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(0, 5, 0, 5),
                Content = receiverSpecialInstruction
            };

            var showRoutePage = new AppRoundButton
            {
                HorizontalOptions = LayoutOptions.Fill
            };
            showRoutePage.SetBinding(AppRoundButton.TextProperty, new Binding("TripShowRouteButtonText", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            showRoutePage.SetBinding(AppRoundButton.CommandProperty, "ShowRouteCommand");

            var showRoutePageContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(20, 5, 20, 5),
                Content = showRoutePage
            };

            var info = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness(2, 0, 2, 5),
                Spacing = 1
            };

            info.Children.Add(shipperTitleContent);
            info.Children.Add(shipperName);
            info.Children.Add(shipperPhoneFaxLineContent);
            info.Children.Add(shipperAddress);
            info.Children.Add(shipperSpecialInstructionContent);
            info.Children.Add(receiverTitleContent);
            info.Children.Add(receiverName);
            info.Children.Add(receiverPhoneFaxLineContent);
            info.Children.Add(receiverAddress);
            info.Children.Add(receiverSpecialInstructionContent);
            info.Children.Add(showRoutePageContent);

            _map = new Map
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill
            };

            var content = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 0,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                }
            };
            content.Children.Add(titleBar, 0, 0);
            content.Children.Add(info, 0, 1);
            content.Children.Add(_map, 0, 2);

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
            pageContent.Children.Add(this.CreateSpecialInstructionPopup());

            this.MapLocateAddress(_map, this.ViewModel.ContractorPosition);

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

        private View CreateSpecialInstructionPopup()
        {
            var mainLabel = new Label
            {
                HorizontalOptions = LayoutOptions.Fill,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TextColor = Color.Black
            };
            mainLabel.SetBinding(Label.TextProperty, "ContractorSpecialInstruction");

            var mainContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(20, 20, 20, 10),
                Content = mainLabel
            };

            var continueButton = new AppPopupButton
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Fill
            };
            continueButton.SetBinding(AppPopupButton.TextProperty, new Binding("TripPopupContinueButtonText", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            continueButton.SetBinding(AppPopupButton.CommandProperty, "PopupContinueCommand");

            var buttons = new Grid
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 0,
                Padding = new Thickness(0, 1, 0, 0)
            };
            buttons.Children.Add(continueButton, 0, 0);

            var popupContent = new StackLayout
            {
                Spacing = 0,
                HorizontalOptions = LayoutOptions.Fill
            };
            popupContent.Children.Add(mainContent);
            popupContent.Children.Add(this.CreateHorizontalLine());
            popupContent.Children.Add(buttons);

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
                IsClippedToBounds = true,
                Content = popupContent
            };

            var background = Color.FromRgba(0, 0, 0, 120);

            var content = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                BackgroundColor = background,
                Padding = new Thickness(40, 0),
                Content = frame
            };
            content.SetBinding(ContentView.IsVisibleProperty, "PopupVisible", BindingMode.TwoWay);

            return content;
        }

        private void MapLocateAddress(Map map, Position position)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if ((map != null) && (this.ViewModel != null))
                {
                    map.MoveToRegion(new MapSpan(position, 0.5, 0.5));
                    map.Pins.Clear();
                    var label = (this.ViewModel.SelectedItem == TripContractorItems.Shipper ? "Shipper" : "Receiver");
                    map.Pins.Add(new Pin { Type = PinType.Place, Position = position, Label = label });
                    map.MoveToRegion(new MapSpan(position, 0.5, 0.5));
                }
            });
        }

        public new TripViewModel ViewModel
        {
            get { return _viewModel; }
        }
    }
    #endregion
}

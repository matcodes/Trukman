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

        public TripPage()
            : base()
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

            this.MapLocateAddress(_map, this.ViewModel.ContractorPosition);
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

            var shipperTitleLabel = new TappedLabel {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                TapCommandParameter = 0
            };
            shipperTitleLabel.SetBinding(TappedLabel.TextProperty, new Binding("TripShipperTitleLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            shipperTitleLabel.SetBinding(TappedLabel.TextColorProperty, new Binding("SelectedItem", BindingMode.OneWay, tripContractorItemsToColorConverter, 0));
            shipperTitleLabel.SetBinding(TappedLabel.TapCommandProperty, "SelectItemCommand");

            var shipperTitleContent = new ContentView {
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(0, 10, 0, 0),
                Content = shipperTitleLabel
            };

            var shipperNameLabel = new TappedLabel
            {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TapCommandParameter = 0
            };
            shipperNameLabel.SetBinding(TappedLabel.TextProperty, new Binding("ContractorPageNameLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            shipperNameLabel.SetBinding(TappedLabel.TextColorProperty, new Binding("SelectedItem", BindingMode.OneWay, tripContractorItemsToColorConverter, 0));
            shipperNameLabel.SetBinding(TappedLabel.TapCommandProperty, "SelectItemCommand");

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
            shipperName.Children.Add(shipperNameLabel);
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

            var shipperAddressLabel = new TappedLabel
            {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TapCommandParameter = 0
            };
            shipperAddressLabel.SetBinding(TappedLabel.TextProperty, new Binding("ContractorPageAddressLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            shipperAddressLabel.SetBinding(TappedLabel.TextColorProperty, new Binding("SelectedItem", BindingMode.OneWay, tripContractorItemsToColorConverter, 0));
            shipperAddressLabel.SetBinding(TappedLabel.TapCommandProperty, "SelectItemCommand");

            var shipperAddressLineFirstValue = new TappedLabel
            {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TapCommandParameter = 0
            };
            shipperAddressLineFirstValue.SetBinding(TappedLabel.TextProperty, "Shipper.AddressLineFirst");
            shipperAddressLineFirstValue.SetBinding(TappedLabel.TextColorProperty, new Binding("SelectedItem", BindingMode.OneWay, tripContractorItemsToColorConverter, 0));
            shipperAddressLineFirstValue.SetBinding(TappedLabel.TapCommandProperty, "SelectItemCommand");

            var shipperAddressLineSecondValue = new TappedLabel
            {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TapCommandParameter = 0
            };
            shipperAddressLineSecondValue.SetBinding(TappedLabel.TextProperty, "Shipper.AddressLineSecond");
            shipperAddressLineSecondValue.SetBinding(TappedLabel.TextColorProperty, new Binding("SelectedItem", BindingMode.OneWay, tripContractorItemsToColorConverter, 0));
            shipperAddressLineSecondValue.SetBinding(TappedLabel.TapCommandProperty, "SelectItemCommand");

            var shipperAddressValue = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Spacing = 4
            };
            shipperAddressValue.Children.Add(shipperAddressLineFirstValue);
            shipperAddressValue.Children.Add(shipperAddressLineSecondValue);

            var shipperAddress = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(0, 2, 0, 2),
                Spacing = 5
            };
            shipperAddress.Children.Add(shipperAddressLabel);
            shipperAddress.Children.Add(shipperAddressValue);

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

            var receiverTitleContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(0, 10, 0, 0),
                Content = receiverTitleLabel
            };

            var receiverNameLabel = new TappedLabel
            {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TapCommandParameter = 1
            };
            receiverNameLabel.SetBinding(TappedLabel.TextProperty, new Binding("ContractorPageNameLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            receiverNameLabel.SetBinding(TappedLabel.TextColorProperty, new Binding("SelectedItem", BindingMode.OneWay, tripContractorItemsToColorConverter, 1));
            receiverNameLabel.SetBinding(TappedLabel.TapCommandProperty, "SelectItemCommand");

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
            receiverName.Children.Add(receiverNameLabel);
            receiverName.Children.Add(receiverNameValue);

            var receiverPhoneLabel = new TappedLabel
            {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TapCommandParameter= 1
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

            var receiverAddressLabel = new TappedLabel
            {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TapCommandParameter = 1
            };
            receiverAddressLabel.SetBinding(TappedLabel.TextProperty, new Binding("ContractorPageAddressLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            receiverAddressLabel.SetBinding(TappedLabel.TextColorProperty, new Binding("SelectedItem", BindingMode.OneWay, tripContractorItemsToColorConverter, 1));
            receiverAddressLabel.SetBinding(TappedLabel.TapCommandProperty, "SelectItemCommand");

            var receiverAddressLineFirstValue = new TappedLabel
            {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TapCommandParameter = 1
            };
            receiverAddressLineFirstValue.SetBinding(Label.TextProperty, "Receiver.AddressLineFirst");
            receiverAddressLineFirstValue.SetBinding(TappedLabel.TextColorProperty, new Binding("SelectedItem", BindingMode.OneWay, tripContractorItemsToColorConverter, 1));
            receiverAddressLineFirstValue.SetBinding(TappedLabel.TapCommandProperty, "SelectItemCommand");

            var receiverAddressLineSecondValue = new TappedLabel
            {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TapCommandParameter = 1
            };
            receiverAddressLineSecondValue.SetBinding(TappedLabel.TextProperty, "Receiver.AddressLineSecond");
            receiverAddressLineSecondValue.SetBinding(TappedLabel.TextColorProperty, new Binding("SelectedItem", BindingMode.OneWay, tripContractorItemsToColorConverter, 1));
            receiverAddressLineSecondValue.SetBinding(TappedLabel.TapCommandProperty, "SelectItemCommand");

            var receiverAddressValue = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Spacing = 4
            };
            receiverAddressValue.Children.Add(receiverAddressLineFirstValue);
            receiverAddressValue.Children.Add(receiverAddressLineSecondValue);

            var receiverAddress = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(0, 2, 0, 2),
                Spacing = 5
            };
            receiverAddress.Children.Add(receiverAddressLabel);
            receiverAddress.Children.Add(receiverAddressValue);

            var info = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness(2, 0, 2, 5),
                Spacing = 1
            };
            info.Children.Add(shipperTitleContent);
            info.Children.Add(shipperName);
            info.Children.Add(shipperPhone);
            info.Children.Add(shipperFax);
            info.Children.Add(shipperAddress);
            info.Children.Add(receiverTitleContent);
            info.Children.Add(receiverName);
            info.Children.Add(receiverPhone);
            info.Children.Add(receiverFax);
            info.Children.Add(receiverAddress);

            _map = new Map {
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

            return content;
        }

        private void MapLocateAddress(Map map, Position position)
        {
            Device.BeginInvokeOnMainThread(() => {
                if ((map != null) && (this.ViewModel != null))
                    map.MoveToRegion(new MapSpan(position, 0.5, 0.5));
            });
        }

        public new TripViewModel ViewModel
        {
            get { return _viewModel; }
        }
    }
    #endregion
}

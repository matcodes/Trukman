using KAS.Trukman.Controls;
using KAS.Trukman.Helpers;
using KAS.Trukman.Languages;
using KAS.Trukman.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace KAS.Trukman.Views.Pages
{
    #region ContractorInfoPage
    public class ContractorInfoPage : TrukmanPage
    {
        private ContractorInfoViewModel _viewModel = null;

        private Map _map = null;

        public ContractorInfoPage() 
            : base()
        {
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (_viewModel != null)
                _viewModel.PropertyChanged -= this.ViewModelPropertyChanged;

            _viewModel = (this.BindingContext as ContractorInfoViewModel);

            if (_viewModel != null)
                _viewModel.PropertyChanged += this.ViewModelPropertyChanged;
        }

        private void ViewModelPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "AddressPosition")
                this.MapLocateAddress();
        }

        private void MapLocateAddress()
        {
            Device.BeginInvokeOnMainThread(() => {
                if ((_map != null) && (this.ViewModel != null))
                    _map.MoveToRegion(new MapSpan(this.ViewModel.AddressPosition, 0.5, 0.5));
            });
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

            var nameLabel = new Label {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TextColor = PlatformHelper.ContractorTextColor
            };
            nameLabel.SetBinding(Label.TextProperty, new Binding("ContractorPageNameLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var nameValue = new Label {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TextColor = PlatformHelper.ContractorTextColor
            };
            nameValue.SetBinding(Label.TextProperty, "Name");

            var name = new StackLayout {
                Orientation = StackOrientation.Horizontal,
                Padding= new Thickness(0, 2, 0, 2),
                Spacing = 5
            };
            name.Children.Add(nameLabel);
            name.Children.Add(nameValue);

            var phoneLabel = new Label {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TextColor = PlatformHelper.ContractorTextColor
            };
            phoneLabel.SetBinding(Label.TextProperty, new Binding("ContractorPagePhoneLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var phoneValue = new Label {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TextColor = PlatformHelper.ContractorTextColor
            };
            phoneValue.SetBinding(Label.TextProperty, "Phone");

            var phone = new StackLayout {
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(0, 2, 0, 2),
                Spacing = 5
            };
            phone.Children.Add(phoneLabel);
            phone.Children.Add(phoneValue);

            var faxLabel = new Label {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TextColor = PlatformHelper.ContractorTextColor
            };
            faxLabel.SetBinding(Label.TextProperty, new Binding("ContractorPageFaxLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var faxValue = new Label {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TextColor = PlatformHelper.ContractorTextColor
            };
            faxValue.SetBinding(Label.TextProperty, "Fax");

            var fax = new StackLayout {
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(0, 2, 0, 2),
                Spacing = 5
            };
            fax.Children.Add(faxLabel);
            fax.Children.Add(faxValue);

            var addressLabel = new Label {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TextColor = PlatformHelper.ContractorTextColor
            };
            addressLabel.SetBinding(Label.TextProperty, new Binding("ContractorPageAddressLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var addressLineFirstValue = new Label {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TextColor = PlatformHelper.ContractorTextColor
            };
            addressLineFirstValue.SetBinding(Label.TextProperty, "AddressLineFirst");

            var addressLineSecondValue = new Label {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TextColor = PlatformHelper.ContractorTextColor
            };
            addressLineSecondValue.SetBinding(Label.TextProperty, "AddressLineSecond");

            var addressValue = new StackLayout {
                Orientation = StackOrientation.Vertical,
                Spacing = 4
            };
            addressValue.Children.Add(addressLineFirstValue);
            addressValue.Children.Add(addressLineSecondValue);

            var address = new StackLayout {
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(0, 2, 0, 2),
                Spacing = 5
            };
            address.Children.Add(addressLabel);
            address.Children.Add(addressValue);

            var info = new StackLayout {
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness(2, 0, 2, 0),
                Spacing = 1
            };
            info.Children.Add(name);
            info.Children.Add(phone);
            info.Children.Add(fax);
            info.Children.Add(address);

            _map = new Map {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill
            };

            var content = new Grid {
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

            this.MapLocateAddress();

            return content;
        }

        public new ContractorInfoViewModel ViewModel
        {
            get { return _viewModel; }
        }
    }
    #endregion
}

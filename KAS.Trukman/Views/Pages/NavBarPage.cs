using KAS.Trukman.Controls;
using KAS.Trukman.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Views.Pages
{
    #region NavBarPage
    public class NavBarPage : TrukmanPage
    {
        public NavBarPage() : base()
        {
            this.BindingContext = new NavBarViewModel();
        }

        protected override View CreateContent()
        {
            var titleBar = new TitleBar();
            titleBar.SetBinding(TitleBar.TitleProperty, "Title", BindingMode.OneWay);

            var showTripButton = new AppButton {
                HorizontalOptions = LayoutOptions.Fill,
                Text = "Show trip"
            };
            showTripButton.SetBinding(AppButton.CommandProperty, "ShowTripCommand");

            var showTripContent = new ContentView {
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(10, 0, 10, 0),
                Content = showTripButton
            };

            var showShipperInfoButton = new AppButton {
                HorizontalOptions= LayoutOptions.Fill,
                Text = "Show shipper info"
            };
            showShipperInfoButton.SetBinding(AppButton.CommandProperty, "ShowShipperInfoCommand");

            var showShipperInfoContent = new ContentView {
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(10, 0, 10, 0),
                Content = showShipperInfoButton
            };

            var showReceiverInfoButton = new AppButton {
                HorizontalOptions = LayoutOptions.Fill,
                Text = "Show receiver info"
            };
            showReceiverInfoButton.SetBinding(AppButton.CommandProperty, "ShowReceiverInfoCommand");

            var showReceiverInfoContent = new ContentView {
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(10, 0, 10, 0),
                Content = showReceiverInfoButton
            };

            var showFuelAdvanceButton = new AppButton {
                HorizontalOptions = LayoutOptions.Fill,
                Text = "Fuel Advance"
            };
            showFuelAdvanceButton.SetBinding(AppButton.CommandProperty, "ShowFuelAdvanceCommand");

            var showFuelAdvanceContent = new ContentView {
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(10, 0, 10, 0),
                Content = showFuelAdvanceButton
            };

            var showLumperButton = new AppButton {
                HorizontalOptions = LayoutOptions.Fill,
                Text = "Lumper"
            };
            showLumperButton.SetBinding(AppButton.CommandProperty, "ShowLumperCommand");

            var showLumperContent = new ContentView {
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(10, 0, 10, 0),
                Content = showLumperButton
            };

            var showDelayEmergencyButton = new AppButton {
                HorizontalOptions = LayoutOptions.Fill,
                Text = "Delay/Emergency"
            };
            showDelayEmergencyButton.SetBinding(AppButton.CommandProperty, "ShowDelayEmergencyCommand");

            var showDelayEmergencyContent = new ContentView {
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(10, 0, 10, 0),
                Content = showDelayEmergencyButton
            };

            var buttonsLayout = new StackLayout {
                Orientation = StackOrientation.Vertical
            };
            buttonsLayout.Children.Add(showTripContent);
            buttonsLayout.Children.Add(showShipperInfoContent);
            buttonsLayout.Children.Add(showReceiverInfoContent);
            buttonsLayout.Children.Add(showFuelAdvanceContent);
            buttonsLayout.Children.Add(showLumperContent);
            buttonsLayout.Children.Add(showDelayEmergencyContent);

            var content = new Grid {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                ColumnSpacing = 0,
                RowSpacing = 0,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                }
            };
            content.Children.Add(titleBar, 0, 0);
            content.Children.Add(buttonsLayout, 0, 1);

            return content;
        }

        public new NavBarViewModel ViewModel
        {
            get { return (base.ViewModel as NavBarViewModel); }
        }
    }
    #endregion
}

using KAS.Trukman.Controls;
using KAS.Trukman.Helpers;
using KAS.Trukman.ViewModels.Pages.Owner;
using KAS.Trukman.Views.Lists;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Views.Pages.Owner
{
    #region OwnerFleetPage
    public class OwnerFleetPage : TrukmanPage
    {
        public OwnerFleetPage()
            : base()
        {
            this.BindingContext = new OwnerFleetViewModel();
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

            var fleetTrips = new FleetTripListView
            {
            };
            fleetTrips.SetBinding(MainMenuListView.ItemsSourceProperty, "Trips", BindingMode.TwoWay);
            fleetTrips.SetBinding(MainMenuListView.SelectedItemProperty, "SelectedTrip", BindingMode.TwoWay);
            fleetTrips.SetBinding(MainMenuListView.ItemClickCommandProperty, "SelectTripCommand");
			fleetTrips.SetBinding(MainMenuListView.RefreshCommandProperty, "RefreshCommand");
			fleetTrips.SetBinding(MainMenuListView.IsRefreshingProperty, "IsRefreshing", BindingMode.TwoWay);

            var appMap = new AppMap
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill
            };
//            appMap.SetBinding(AppMap.RouteRegionProperty, "RouteRegion", BindingMode.OneWay);
            appMap.SetBinding(AppMap.RoutePointsProperty, "RoutePoints", BindingMode.OneWay);
            appMap.SetBinding(AppMap.BaseRoutePointsProperty, "BaseRoutePoints", BindingMode.OneWay);
            appMap.SetBinding(AppMap.RouteStartPositionProperty, "StartPosition", BindingMode.OneWay);
            appMap.SetBinding(AppMap.RouteEndPositionProperty, "EndPosition", BindingMode.OneWay);
            appMap.SetBinding(AppMap.RouteCarPositionProperty, "CurrentPosition", BindingMode.OneWay);

            var content = new Grid
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 0,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(3, GridUnitType.Star) }
                }
            };
            content.Children.Add(titleBar, 0, 0);
            content.Children.Add(fleetTrips, 0, 1);
            content.Children.Add(appMap, 0, 2);

            var busyIndicator = new ActivityIndicator
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };
            busyIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy", BindingMode.TwoWay);

            var pageContent = new Grid
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 0
            };
            pageContent.Children.Add(content);
            pageContent.Children.Add(busyIndicator);

            return pageContent;
        }

        public new OwnerFleetViewModel ViewModel
        {
            get { return (this.BindingContext as OwnerFleetViewModel); }
        }
    }
    #endregion
}

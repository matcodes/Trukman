using KAS.Trukman.Controls;
using KAS.Trukman.Helpers;
using KAS.Trukman.ViewModels.Pages;
using KAS.Trukman.Views.Lists;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Views.Pages
{
    #region RoutePage
    public class RoutePage : TrukmanPage
    {
        public RoutePage()
            : base()
        {
            this.BindingContext = new RouteViewModel();
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

            var routeStepsListView = new RouteStepsListView { 
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill
            };
            routeStepsListView.SetBinding(RouteStepsListView.ItemsSourceProperty, "RouteSteps");
            routeStepsListView.SetBinding(RouteStepsListView.SelectedItemProperty, "SelectedRouteStep", BindingMode.TwoWay);

            var stepInfo = new AppLabel {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = PlatformHelper.RouteTextColor,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))
            };
            stepInfo.SetBinding(AppLabel.TextProperty, "StepInfoText", BindingMode.OneWay);

            var stepInfoContent = new ContentView {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Padding = new Thickness(20, 2, 20, 2),
                Content = stepInfo
            };

            var info = new Grid {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 0,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) }
                }
            };
            info.Children.Add(routeStepsListView, 0, 0);
            info.Children.Add(stepInfoContent, 0, 1);

            var appMap = new AppMap() {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill
            };
            appMap.SetBinding(AppMap.RouteRegionProperty, "RouteRegion", BindingMode.OneWay);
            appMap.SetBinding(AppMap.RoutePointsProperty, "RoutePoints", BindingMode.OneWay);
            appMap.SetBinding(AppMap.RouteStartPositionProperty, "StartPosition", BindingMode.OneWay);
            appMap.SetBinding(AppMap.RouteEndPositionProperty, "EndPosition", BindingMode.OneWay);
            appMap.SetBinding(AppMap.RouteCarPositionProperty, "CurrentPosition", BindingMode.OneWay);

            var content = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 0,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(2, GridUnitType.Star) }
                }
            };
            content.Children.Add(titleBar, 0, 0);
            content.Children.Add(info, 0, 1);
            content.Children.Add(appMap, 0, 2);

            return content;
        }

        public new RouteViewModel ViewModel
        {
            get { return (this.BindingContext as RouteViewModel); }
        }
    }
    #endregion
}

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
    #region PointsAndRewardsPage
    public class PointsAndRewardsPage : TrukmanPage
    {
        public PointsAndRewardsPage()
            : base()
        {
            this.BindingContext = new PointsAndRewardsViewModel();
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

            var jobPoints = new DriverJobPointListView
            {
            };
            jobPoints.SetBinding(DriverJobPointListView.ItemsSourceProperty, "JobPointGroups", BindingMode.TwoWay);
            jobPoints.SetBinding(DriverJobPointListView.SelectedItemProperty, "SelectedJobPoint", BindingMode.TwoWay);
            jobPoints.SetBinding(DriverJobPointListView.ItemClickCommandProperty, "SelectJobPointCommand");
            jobPoints.SetBinding(DriverJobPointListView.RefreshCommandProperty, "RefreshCommand");
            jobPoints.SetBinding(DriverJobPointListView.IsRefreshingProperty, "IsRefreshing", BindingMode.TwoWay);

            var content = new Grid
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 0,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(3, GridUnitType.Star) }
                }
            };
            content.Children.Add(titleBar, 0, 0);
            content.Children.Add(jobPoints, 0, 1);

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

        public new PointsAndRewardsViewModel ViewModel
        {
            get { return (this.BindingContext as PointsAndRewardsViewModel); }
        }
    }
    #endregion
}

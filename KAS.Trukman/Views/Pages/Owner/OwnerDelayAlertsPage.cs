using KAS.Trukman.Controls;
using KAS.Trukman.Helpers;
using KAS.Trukman.ViewModels.Pages.Owner;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Views.Pages.Owner
{
    #region OwnerDelayAlertsPage
    public class OwnerDelayAlertsPage : TrukmanPage
    {
        public OwnerDelayAlertsPage()
            : base()
        {
            this.BindingContext = new OwnerDelayAlertsViewModel();
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

			var jobAlerts = new DelayAlertListView
            {
            };
            jobAlerts.SetBinding(DelayAlertListView.ItemsSourceProperty, "JobAlertGroups", BindingMode.TwoWay);
            jobAlerts.SetBinding(DelayAlertListView.SelectedItemProperty, "SelectedJobPoint", BindingMode.TwoWay);
            jobAlerts.SetBinding(DelayAlertListView.ItemClickCommandProperty, "SelectJobAlertCommand");
            jobAlerts.SetBinding(DelayAlertListView.RefreshCommandProperty, "RefreshCommand");
            jobAlerts.SetBinding(DelayAlertListView.IsRefreshingProperty, "IsRefreshing", BindingMode.TwoWay);

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
            content.Children.Add(jobAlerts, 0, 1);

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

        public new OwnerDelayAlertsViewModel ViewModel
        {
            get { return (this.BindingContext as OwnerDelayAlertsViewModel); }
        }
    }
    #endregion
}

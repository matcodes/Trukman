﻿using KAS.Trukman.Controls;
using KAS.Trukman.Helpers;
using KAS.Trukman.Languages;
using KAS.Trukman.ViewModels.Pages.Owner;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Views.Pages.Owner
{
	#region OwnerBrokerListPage
    public class OwnerBrokerListPage : TrukmanPage
    {
        public OwnerBrokerListPage() : base()
        {
            this.BindingContext = new OwnerBrokerListViewModel();
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

			var brokers = new BrokerListView
            {
            };
            brokers.SetBinding(BrokerListView.ItemsSourceProperty, "Brokers", BindingMode.TwoWay);
            brokers.SetBinding(BrokerListView.SelectedItemProperty, "SelectedBroker", BindingMode.TwoWay);
            brokers.SetBinding(BrokerListView.ItemClickCommandProperty, "SelectBrokerCommand");
            brokers.SetBinding(BrokerListView.RefreshCommandProperty, "RefreshCommand");
            brokers.SetBinding(BrokerListView.IsRefreshingProperty, "IsRefreshing", BindingMode.TwoWay);

            var addBroker = new AppRoundButton
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.End
            };
            addBroker.SetBinding(AppRoundButton.TextProperty, new Binding("OwnerBrokerListAddBrokerCommandText", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            addBroker.SetBinding(AppRoundButton.CommandProperty, "AddBrokerCommand");

            var addBrokerContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(20, 0, 20, 10),
                Content = addBroker
            };

            var content = new Grid
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 0,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(3, GridUnitType.Star) },
                    new RowDefinition {Height = new GridLength(1, GridUnitType.Auto) }
                }
            };
            content.Children.Add(titleBar, 0, 0);
			content.Children.Add(brokers, 0, 1);
            content.Children.Add(addBrokerContent, 0, 2);

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

        public new OwnerBrokerListViewModel ViewModel
        {
            get { return (this.BindingContext as OwnerBrokerListViewModel); }
        }
    }
    #endregion
}

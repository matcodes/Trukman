using KAS.Trukman.Controls;
using KAS.Trukman.Helpers;
using KAS.Trukman.ViewModels.Pages.Owner;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Views.Pages.Owner
{
    #region OwnerBrockerListPage
    public class OwnerBrockerListPage : TrukmanPage
    {
        public OwnerBrockerListPage() 
            : base()
        {
            this.BindingContext = new OwnerBrockerListViewModel();
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

            //var advances = new AdvanceListView
            //{
            //};
            //advances.SetBinding(AdvanceListView.ItemsSourceProperty, "Advances", BindingMode.TwoWay);
            //advances.SetBinding(AdvanceListView.SelectedItemProperty, "SelectedAdvance", BindingMode.TwoWay);
            //advances.SetBinding(AdvanceListView.ItemClickCommandProperty, "SelectAdvanceCommand");
            //advances.SetBinding(AdvanceListView.RefreshCommandProperty, "RefreshCommand");
            //advances.SetBinding(AdvanceListView.IsRefreshingProperty, "IsRefreshing", BindingMode.TwoWay);

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
            //content.Children.Add(advances, 0, 1);

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

        public new OwnerBrockerListViewModel ViewModel
        {
            get { return (this.BindingContext as OwnerBrockerListViewModel); }
        }
    }
    #endregion
}

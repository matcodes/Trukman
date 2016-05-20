using KAS.Trukman.Controls;
using KAS.Trukman.ViewModels.Pages.Owner;
using KAS.Trukman.Views.Commands;
using KAS.Trukman.Views.Lists;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using KAS.Trukman.Storage.ParseClasses;
using KAS.Trukman.Helpers;
using KAS.Trukman.Data.Classes;

namespace KAS.Trukman.Views.Pages.Owner
{
	public class OwnerCurrentJobListPage : TrukmanPage
	{
		public OwnerCurrentJobListPage ():base()
		{
			this.BindingContext = new OwnerCurrentJobsViewModel();
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

			var list = new OwnerDeliveryUpdateListView ();
			{
			};
				
			list.SetBinding(MainMenuListView.RefreshCommandProperty, "RefreshCommand");
			list.SetBinding(MainMenuListView.IsRefreshingProperty, "IsRefreshing", BindingMode.TwoWay);

			list.SetBinding(MainMenuListView.ItemsSourceProperty, "PhotosGrouped", BindingMode.TwoWay);
			list.ItemTapped += (sender, args) =>
			{
				var photo = (args.Item as Photo);
				if (photo == null)
					return;
//
				Navigation.PushAsync(new ImageViewerPage(photo));
				// Reset the selected item
				list.SelectedItem = null;
			};


			var content = new Grid
			{
				VerticalOptions = LayoutOptions.Fill,
				HorizontalOptions = LayoutOptions.Fill,
				ColumnSpacing = 0,
				RowSpacing = 0,
				RowDefinitions = {
					new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
					new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
				}
			};
			content.Children.Add(titleBar, 0, 0);
			content.Children.Add(list, 0, 1);

			var pageContent = new Grid
			{
				VerticalOptions = LayoutOptions.Fill,
				HorizontalOptions = LayoutOptions.Fill,
				ColumnSpacing = 0,
				RowSpacing = 0
			};
			pageContent.Children.Add(content);

			return pageContent;
		}

		public new OwnerCurrentJobsViewModel ViewModel
		{
			get { return (this.BindingContext as OwnerCurrentJobsViewModel); }
		}

	}
}



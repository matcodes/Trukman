using KAS.Trukman.Controls;
using KAS.Trukman.Helpers;
using KAS.Trukman.ViewModels.Pages.Owner;
using KAS.Trukman.Views.Lists;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using KAS.Trukman.Languages;

namespace KAS.Trukman.Views.Pages.Owner
{
    #region OwnerFuelAdvancePage
    public class OwnerFuelAdvancePage : TrukmanPage
    {
        private Color lineColor = Color.FromHex("#808080");

        public OwnerFuelAdvancePage() : base()
        {
            this.BindingContext = new OwnerFuelAdvanceViewModel();
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

            var advances = new AdvanceListView
            {
            };
            advances.SetBinding(AdvanceListView.ItemsSourceProperty, "Advances", BindingMode.TwoWay);
            advances.SetBinding(AdvanceListView.SelectedItemProperty, "SelectedAdvance", BindingMode.TwoWay);
            advances.SetBinding(AdvanceListView.ItemClickCommandProperty, "SelectAdvanceCommand");
            advances.SetBinding(AdvanceListView.RefreshCommandProperty, "RefreshCommand");
            advances.SetBinding(AdvanceListView.IsRefreshingProperty, "IsRefreshing", BindingMode.TwoWay);

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
            content.Children.Add(advances, 0, 1);

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
			pageContent.Children.Add (content);
			pageContent.Children.Add (this.CreateEditComcheckPopup());
			pageContent.Children.Add (busyIndicator);

            return pageContent;
        }

        private View CreateHorizontalLine()
        {
            var line = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Start,
                HeightRequest = 1,
                BackgroundColor = lineColor
            };

            return line;
        }

        private View CreateVerticalLine()
        {
            var line = new ContentView
            {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Fill,
                WidthRequest = 1,
                BackgroundColor = lineColor
            };

            return line;
        }

        private View CreateEditComcheckPopup()
		{
			var jobLabel = new Label
			{
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Start,
				TextColor = Color.Black,
				FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))
			};
			jobLabel.SetBinding (Label.TextProperty, new Binding ("AdvanceListJobNumberLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

			var job = new Label
			{
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Start,
				TextColor = Color.Black,
				FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
				FontAttributes = FontAttributes.Bold
			};
			job.SetBinding(Label.TextProperty, "EditingAdvance.JobNumber", BindingMode.OneWay);

			var driverLabel = new Label
			{
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Start,
				TextColor = Color.Black,
				FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))
			};
			driverLabel.SetBinding (Label.TextProperty, new Binding ("AdvanceListDriverNameLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

			var driver = new Label
			{
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Start,
				TextColor = Color.Black,
				FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
				FontAttributes = FontAttributes.Bold
			};
			driver.SetBinding(Label.TextProperty, "EditingAdvance.DriverName", BindingMode.OneWay);

			var grid = new Grid {
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill,
				ColumnSpacing = 10,
				RowSpacing = 0,
				Padding = new Thickness(10, 20, 10, 0),
				RowDefinitions = {
					new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
					new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
				},
				ColumnDefinitions = {
					new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) },
					new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
				}
			};
			grid.Children.Add(jobLabel, 0, 0);
			grid.Children.Add(job, 1, 0);
			grid.Children.Add(driverLabel, 0, 1);
			grid.Children.Add(driver, 1, 1);

			var comcheck = new AppEntry
			{
				HorizontalOptions = LayoutOptions.Fill,
				FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
				TextColor = Color.Black,
				PlaceholderColor = Color.Gray
			};
			comcheck.SetBinding(Entry.TextProperty, "Comcheck", BindingMode.TwoWay);
			comcheck.SetBinding(Entry.PlaceholderProperty, new Binding("ComcheckPopupEntryPlaceholer", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

			var comcheckContent = new ContentView
			{
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Center,
				Padding = new Thickness(10, 0, 10, 0),
				Content = comcheck
			};

			var cancelButton = new AppPopupButton
			{
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Fill
			};
			cancelButton.SetBinding(AppPopupButton.TextProperty, new Binding("ComcheckPopupCancelButtonText", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
			cancelButton.SetBinding(AppPopupButton.CommandProperty, "EditComcheckCancelCommand");

			var acceptButton = new AppPopupButton
			{
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Fill
			};
			acceptButton.SetBinding(AppPopupButton.TextProperty, new Binding("ComcheckPopupAcceptButtonText", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
			acceptButton.SetBinding(AppPopupButton.CommandProperty, "EditComcheckAcceptCommand");

            var buttons = new Grid
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 0,
                Padding = new Thickness(0), //, 1, 0, 0),
				ColumnDefinitions = {
					new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
				}
			};
			buttons.Children.Add(cancelButton, 0, 0);
            buttons.Children.Add(this.CreateVerticalLine(), 1, 0);
            buttons.Children.Add(acceptButton, 2, 0);

			var popupContent = new Grid
			{
				HorizontalOptions = LayoutOptions.Fill,
				HeightRequest = this.Height / 2,
				RowSpacing = 0,
				ColumnSpacing = 0,
				RowDefinitions = {
					new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
					new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
					new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) }
                }
			};
			popupContent.Children.Add(grid, 0, 0);
			popupContent.Children.Add(comcheckContent, 0, 1);
            popupContent.Children.Add(this.CreateHorizontalLine(), 0, 2);
            popupContent.Children.Add(buttons, 0, 3);

            var frameBackground = Color.FromHex("#F2F2F2");

            var frame = new Frame
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(0),
                BackgroundColor = frameBackground,
                CornerRadius = 8,
                OutlineColor = frameBackground,
                HasShadow = false,
                Content = popupContent
            };

            var background = Color.FromRgba(0, 0, 0, 120);

            var content = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                BackgroundColor = background,
                Padding = new Thickness(40),
                Content = frame
            };
			content.SetBinding(ContentView.IsVisibleProperty, "EditComcheckPopupVisible", BindingMode.TwoWay);
			content.PropertyChanged += (sender, e) => 
			{
				if (this.ViewModel.EditComcheckPopupVisible)
					comcheck.Focus();
			};

			return content;
		}

        public new OwnerFuelAdvanceViewModel ViewModel
        {
            get { return (this.BindingContext as OwnerFuelAdvanceViewModel); }
        }
    }
    #endregion
}

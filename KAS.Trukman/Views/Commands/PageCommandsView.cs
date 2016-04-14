using KAS.Trukman.Controls;
using KAS.Trukman.Helpers;
using KAS.Trukman.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Views.Commands
{
    #region PageCommandsView
    public class PageCommandsView : Grid
    {
        public PageCommandsView() 
            : base()
        {
            this.BindingContext = new PageCommandsViewModel();

            this.HorizontalOptions = LayoutOptions.Fill;
            this.VerticalOptions = LayoutOptions.Center;

            this.RowSpacing = 0;
            this.ColumnSpacing = 0;

            this.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            this.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            this.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            this.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            this.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            var size = 48;

            var homeButton = new ToolButton {
                WidthRequest = size,
                HeightRequest = size,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                ImageSourceName = PlatformHelper.HomeImageSource
            };
            homeButton.SetBinding(ToolButton.CommandProperty, "HomeCommand");

            var mailButton = new ToolButton
            {
                WidthRequest = size,
                HeightRequest = size,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                ImageSourceName = PlatformHelper.MailImageSource
            };
            mailButton.SetBinding(ToolButton.CommandProperty, "MailCommand");

            var mapButton = new ToolButton
            {
                WidthRequest = size,
                HeightRequest = size,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                ImageSourceName = PlatformHelper.MapImageSource
            };
            mapButton.SetBinding(ToolButton.CommandProperty, "MapCommand");

            var locationButton = new ToolButton
            {
                WidthRequest = size,
                HeightRequest = size,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                ImageSourceName = PlatformHelper.LocationImageSource
            };
            locationButton.SetBinding(ToolButton.CommandProperty, "LocationCommand");

            var cameraButton = new ToolButton
            {
                WidthRequest = size,
                HeightRequest = size,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                ImageSourceName = PlatformHelper.CameraImageSource
            };
            cameraButton.SetBinding(ToolButton.CommandProperty, "CameraCommand");

            this.Children.Add(homeButton, 0, 0);
            this.Children.Add(mailButton, 1, 0);
            this.Children.Add(mapButton, 2, 0);
            this.Children.Add(locationButton, 3, 0);
            this.Children.Add(cameraButton, 4, 0);
        }
    }
    #endregion
}

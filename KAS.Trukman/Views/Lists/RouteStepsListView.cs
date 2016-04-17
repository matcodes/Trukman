using KAS.Trukman.Converters;
using KAS.Trukman.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Views.Lists
{
    #region RouteStepsListView
    public class RouteStepsListView : BaseListView
    {
        public RouteStepsListView() 
            : base()
        {
            this.BackgroundColor = Color.Transparent;

            this.IsPullToRefreshEnabled = false;

            this.ItemTemplate = new DataTemplate(typeof(RouteStepCell));
        }

    }
    #endregion

    #region RouteStepCell
    public class RouteStepCell : ViewCell
    {
        public RouteStepCell() 
            : base()
        {
            var text = new Label {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center,
                TextColor = PlatformHelper.RouteTextColor,
                HorizontalTextAlignment = TextAlignment.Start,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))
            };
            text.SetBinding(Label.FormattedTextProperty, "Text", BindingMode.OneWay);

            var textContent = new ContentView {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Padding = new Thickness(2, 0, 0, 0),
                Content = text
            };

            var distance = new Label {
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center,
                TextColor = PlatformHelper.RouteTextColor,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            };
            distance.SetBinding(Label.TextProperty, "Distance", BindingMode.OneWay);

            var distanceContent = new ContentView {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Padding = new Thickness(5, 0, 0, 0),
                Content = distance
            };

            var routeStepTurnToImageConverter = new RouteStepTurnToImageConverter();

            var image = new Image {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                HeightRequest = 48,
                WidthRequest = 48,
            };
            image.SetBinding(Image.SourceProperty, new Binding("Turn", BindingMode.OneWay, routeStepTurnToImageConverter));

            var imageContent = new ContentView {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Padding = new Thickness(5, 0, 2, 0),
                Content = image
            };

            var grid = new Grid {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 0,
                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) }
                }
            };
            grid.Children.Add(textContent, 0, 0);
            grid.Children.Add(distanceContent, 1, 0);
            grid.Children.Add(imageContent, 2, 0);

            var content = new ContentView {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Padding = new Thickness(2),
                Content = grid
            };

            this.View = content;
        }
    }
    #endregion
}

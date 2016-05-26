using KAS.Trukman.Languages;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Views.Lists
{
    #region DriverJobPointListView
    public class DriverJobPointListView : BaseListView
    {
        public DriverJobPointListView()
            : base()
        {
            this.BackgroundColor = Color.Transparent;
            this.IsPullToRefreshEnabled = true;
            this.IsGroupingEnabled = true;
            this.GroupDisplayBinding = new Binding("Job");
            if (Device.OS != TargetPlatform.WinPhone)
                this.GroupHeaderTemplate = new DataTemplate(typeof(DriverJobPointGroupCell));
            this.ItemTemplate = new DataTemplate(typeof(DriverJobPointItemCell));
        }
    }
    #endregion

    #region DriverJobPointGroupCell
    public class DriverJobPointGroupCell : ViewCell
    {
        public DriverJobPointGroupCell() 
            : base()
        {
            var jobLabel = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,
                TextColor = Color.Black,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))
            };
            jobLabel.SetBinding(Label.TextProperty, new Binding("JobAlertListJobNumberLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var job = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,
                TextColor = Color.Black,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            };
            job.SetBinding(Label.TextProperty, "Job.JobRef", BindingMode.OneWay);

            var points = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,
                TextColor = Color.Black,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            };
            points.SetBinding(Label.TextProperty, "Points", BindingMode.OneWay);

            var grid = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                ColumnSpacing = 10,
                RowSpacing = 0,
                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) }
                }
            };
            grid.Children.Add(jobLabel, 0, 0);
            grid.Children.Add(job, 1, 0);
            grid.Children.Add(points, 2, 0);

            var displayNameContent = new ContentView
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(10, 0, 10, 0),
                Content = grid
            };

            this.View = displayNameContent;
        }
    }
    #endregion

    #region DriverJobPointItemCell
    public class DriverJobPointItemCell : ViewCell
    {
        public DriverJobPointItemCell() 
            : base()
        {
            var job = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))
            };
            job.SetBinding(Label.TextProperty, new Binding("Text", BindingMode.OneWay, null, null, null));

            var points = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))
            };
            points.SetBinding(Label.TextProperty, new Binding("Value", BindingMode.OneWay, null, null, null));

            var grid = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                ColumnSpacing = 10,
                RowSpacing = 0,
                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) }
                }
            };
            grid.Children.Add(job, 0, 0);
            grid.Children.Add(points, 1, 0);

            var displayNameContent = new ContentView
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(10, 0, 10, 0),
                Content = grid
            };

            this.View = displayNameContent;
        }
    }
    #endregion
}

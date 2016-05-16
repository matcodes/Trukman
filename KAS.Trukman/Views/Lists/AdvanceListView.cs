using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Views.Lists
{
    #region AdvanceListView
    public class AdvanceListView : BaseListView
    {
        public AdvanceListView() 
            : base()
        {
            this.BackgroundColor = Color.Transparent;

            this.IsPullToRefreshEnabled = true;

            this.ItemTemplate = new DataTemplate(typeof(AdvanceCell));
        }
    }
    #endregion

    #region AdvanceCell
    public class AdvanceCell : ViewCell
    {
        public AdvanceCell()
            : base()
        {
            var jobLabel = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                Text = "Job#:"
            };

            var job = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            };
            job.SetBinding(Label.TextProperty, "JobNumber", BindingMode.OneWay);

            var driverLabel = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                Text = "Driver:"
            };

            var driver = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            };
            driver.SetBinding(Label.TextProperty, "DriverName", BindingMode.OneWay);

            var grid = new Grid {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                ColumnSpacing = 10,
                RowSpacing = 0,
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

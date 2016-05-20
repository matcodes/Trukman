using KAS.Trukman.Languages;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Views.Lists
{
    #region InvoiceListView
    public class InvoiceListView :  BaseListView
    {
        public InvoiceListView() 
            : base()
        {
            this.BackgroundColor = Color.Transparent;

            this.IsPullToRefreshEnabled = true;

            this.ItemTemplate = new DataTemplate(typeof(InvoiceCell));
        }
    }
    #endregion

    #region InvoiceCell
    public class InvoiceCell : ViewCell
    {
        public InvoiceCell()
            : base()
        {
            var jobLabel = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))
            };
            jobLabel.SetBinding(Label.TextProperty, new Binding("InvoiceListJobNumberLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var job = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            };
            job.SetBinding(Label.TextProperty, "JobRef", BindingMode.OneWay);

            var driverLabel = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))
            };
            driverLabel.SetBinding(Label.TextProperty, new Binding("InvoiceListDriverNameLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var driver = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            };
            driver.SetBinding(Label.TextProperty, "Driver.FullName", BindingMode.OneWay);

            var grid = new Grid
            {
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

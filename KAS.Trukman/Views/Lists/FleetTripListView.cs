using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Views.Lists
{
    #region FleepTripListView
    public class FleetTripListView : BaseListView
    {
        public FleetTripListView()
            : base()
        {
            this.BackgroundColor = Color.Transparent;

            this.IsPullToRefreshEnabled = false;

            this.ItemTemplate = new DataTemplate(typeof(FleetTripCell));
        }
    }
    #endregion

    #region CompanyCell
    public class FleetTripCell : ViewCell
    {
        public FleetTripCell()
            : base()
        {
            var displayName = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Fill,
                TextColor = Color.Black,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))
            };
            displayName.SetBinding(Label.TextProperty, "DriverDisplayName", BindingMode.OneWay);

            var displayNameContent = new ContentView
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(10, 0, 10, 0),
                Content = displayName
            };

            this.View = displayNameContent;
        }
    }
    #endregion
}


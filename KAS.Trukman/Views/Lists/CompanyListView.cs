using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Views.Lists
{
    #region CompanyListView
    public class CompanyListView : BaseListView
    {
        public CompanyListView()
            : base()
        {
            this.BackgroundColor = Color.Transparent;

            this.IsPullToRefreshEnabled = false;

            this.ItemTemplate = new DataTemplate(typeof(CompanyCell));
        }
    }
    #endregion

    #region CompanyCell
    public class CompanyCell : ViewCell
    {
        public CompanyCell()
            : base()
        {
            var displayName = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Fill,
                TextColor = Color.Black,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
            };
            displayName.SetBinding(Label.TextProperty, "DisplayName", BindingMode.OneWay);

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

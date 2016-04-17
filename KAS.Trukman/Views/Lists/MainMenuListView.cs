using KAS.Trukman.Converters;
using KAS.Trukman.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Views.Lists
{
    #region MainMenuListView
    public class MainMenuListView : BaseListView
    {
        public MainMenuListView()
            : base()
        {
            this.BackgroundColor = Color.Transparent;

            this.IsPullToRefreshEnabled = false;

            this.ItemTemplate = new DataTemplate(typeof(MainMenuCell));
        }
    }
    #endregion

    #region MenuItemCell
    public class MainMenuCell : ViewCell
    {
        public MainMenuCell()
            : base()
        {
            var mainMenuEnabledToColorConverter = new MainMenuEnabledToColorConverter();

            var text = new Label
            {
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                VerticalOptions = LayoutOptions.Center
            };
            text.SetBinding(Label.TextProperty, "Label", BindingMode.OneWay);
            text.SetBinding(Label.TextColorProperty, new Binding("IsEnabled", BindingMode.OneWay, mainMenuEnabledToColorConverter));

            var image = new Image
            {
                Source = PlatformHelper.RightImageSource
            };

            var grid = new Grid
            {
                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) }
                }
            };
            grid.Children.Add(text, 0, 0);
            grid.Children.Add(image, 1, 0);

            var content = new ContentView
            {
                Padding = new Thickness(10),
                Content = grid
            };

            this.View = content;
        }
    }
    #endregion
}

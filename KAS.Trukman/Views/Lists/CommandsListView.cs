using KAS.Trukman.Converters;
using KAS.Trukman.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Views.Lists
{
    #region CommandsListView
    public class CommandsListView : BaseListView
    {
        public CommandsListView() 
            : base()
        {
            this.BackgroundColor = Color.Transparent;

            this.IsPullToRefreshEnabled = false;

            this.ItemTemplate = new DataTemplate(typeof(CommandMenuCell));
        }
    }
    #endregion

    #region CommandItemCell
    public class CommandMenuCell : ViewCell
    {
        public CommandMenuCell()
            : base()
        {
            var mainMenuEnabledToColorConverter = new MainMenuEnabledToColorConverter();

            var icon = new Image {
            };
            icon.SetBinding(Image.SourceProperty, "Icon", BindingMode.OneWay);

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
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) }
                }
            };
            grid.Children.Add(icon, 0, 0);
            grid.Children.Add(text, 1, 0);
            grid.Children.Add(image, 2, 0);

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

using KAS.Trukman.Helpers;
using KAS.Trukman.Views.Lists;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Views.Pages.Owner
{
    #region OwnerMainMenuPage
    public class OwnerMainMenuPage : TrukmanPage
    {
        public OwnerMainMenuPage()
            : base()
        {
            this.Title = "Main menu";

            this.BackgroundColor = Color.Transparent;
            this.BackgroundImage = PlatformHelper.BackgroundMenuImageSource;
        }


        protected override View CreateContent()
        {
            var mainMenuListView = new MainMenuListView
            {
                RowHeight = PlatformHelper.DisplayHeight / 12
            };
            mainMenuListView.SetBinding(MainMenuListView.ItemsSourceProperty, "Items", BindingMode.TwoWay);
            mainMenuListView.SetBinding(MainMenuListView.SelectedItemProperty, "SelectedItem", BindingMode.TwoWay);
            mainMenuListView.SetBinding(MainMenuListView.ItemClickCommandProperty, "SelectItemCommand");

            var content = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 0,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(4, GridUnitType.Star) }
                }
            };
            content.Children.Add(mainMenuListView, 0, 2);

            return content;
        }

    }
    #endregion
}

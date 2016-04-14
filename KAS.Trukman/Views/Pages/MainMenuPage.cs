using KAS.Trukman.Helpers;
using KAS.Trukman.Views.Lists;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Views.Pages
{
    #region MainMenuPage
    public class MainMenuPage : TrukmanPage
    {
        public MainMenuPage() 
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
            };
            mainMenuListView.SetBinding(MainMenuListView.ItemsSourceProperty, "Items", BindingMode.TwoWay);
            mainMenuListView.SetBinding(MainMenuListView.SelectedItemProperty, "SelectedItem", BindingMode.TwoWay);
            mainMenuListView.SetBinding(MainMenuListView.ItemClickCommandProperty, "SelectItemCommand");

            var content = new StackLayout
            {
            };
            content.Children.Add(mainMenuListView);

            return content;
        }
    }
    #endregion
}

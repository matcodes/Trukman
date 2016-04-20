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
            var company = new Label {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = PlatformHelper.MainMenuEnabledColor,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
            };
            company.SetBinding(Label.TextProperty, "CompanyName", BindingMode.OneWay);

            var user = new Label
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = PlatformHelper.MainMenuEnabledColor,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            };
            user.SetBinding(Label.TextProperty, "UserName", BindingMode.OneWay);

            var userInfo = new Grid {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 0,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength(2, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                }
            };
            userInfo.Children.Add(company, 0, 0);
            userInfo.Children.Add(user, 0, 1);

            var mainMenuListView = new MainMenuListView {
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
                    new RowDefinition { Height = new GridLength(4, GridUnitType.Star) }
                }
            };
            content.Children.Add(userInfo, 0, 0);
            content.Children.Add(mainMenuListView, 0, 1);

            return content;
        }
    }
    #endregion
}

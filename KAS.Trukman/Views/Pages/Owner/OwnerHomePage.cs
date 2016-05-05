using KAS.Trukman.Controls;
using KAS.Trukman.ViewModels.Pages.Owner;
using KAS.Trukman.Views.Commands;
using KAS.Trukman.Views.Lists;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Views.Pages.Owner
{
    #region OwnerHomeGage
    public class OwnerHomePage : TrukmanPage
    {
        public OwnerHomePage()
            : base()
        {
            this.BindingContext = new OwnerHomeViewModel();
        }

        protected override View CreateContent()
        {
            var titleBar = new TitleBar
            {
            };
            titleBar.SetBinding(TitleBar.TitleProperty, "Title", BindingMode.OneWay);
            titleBar.SetBinding(TitleBar.LeftCommandProperty, "ShowMainMenuCommand", BindingMode.OneWay);

            var commandsList = new CommandsListView
            {
            };
            commandsList.SetBinding(MainMenuListView.ItemsSourceProperty, "Items", BindingMode.TwoWay);
            commandsList.SetBinding(MainMenuListView.SelectedItemProperty, "SelectedItem", BindingMode.TwoWay);
            commandsList.SetBinding(MainMenuListView.ItemClickCommandProperty, "SelectItemCommand");

            var pageCommands = new PageCommandsView
            {
            };

            var content = new Grid
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                ColumnSpacing = 0,
                RowSpacing = 0,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) }
                }
            };
            content.Children.Add(titleBar, 0, 0);
            content.Children.Add(commandsList, 0, 1);
            content.Children.Add(pageCommands, 0, 2);

            var pageContent = new Grid
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                ColumnSpacing = 0,
                RowSpacing = 0
            };
            pageContent.Children.Add(content);

            return pageContent;
        }

        public new OwnerHomeViewModel ViewModel
        {
            get { return (this.BindingContext as OwnerHomeViewModel); }
        }
    }
    #endregion
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace KAS.Trukman.Views.Lists
{
    #region BaseListView
    public class BaseListView : ListView
    {
        #region Статические методы
        public static readonly BindableProperty ItemClickCommandProperty = BindableProperty.Create("ItemClickCommand", typeof(ICommand), typeof(BaseListView), null);
        public static readonly BindableProperty LoadMoreCommandProperty = BindableProperty.Create("LoadMoreCommand", typeof(ICommand), typeof(BaseListView), null);
        #endregion

        private bool _isFirstVisible = false;

        public BaseListView()
            : base(ListViewCachingStrategy.RecycleElement)
        {
            VerticalOptions = LayoutOptions.FillAndExpand;

            this.IsPullToRefreshEnabled = true;

            this.ItemTapped += (sender, args) => {
                if ((args.Item != null) && (this.ItemClickCommand != null) && (this.ItemClickCommand.CanExecute(args.Item)))
                {
                    this.ItemClickCommand.Execute(args.Item);
                };
            };

            this.ItemAppearing += (sender, args) => {
                var items = (this.ItemsSource as IList);

                if (args.Item == items[0])
                    _isFirstVisible = true;

                if ((items != null) && (items.Count > 0))
                {
                    if ((args.Item == items[items.Count - 1]) && (!_isFirstVisible) && (this.LoadMoreCommand != null) && (this.LoadMoreCommand.CanExecute(null)))
                    {
                        this.LoadMoreCommand.Execute(null);
                    }
                }
            };

            this.ItemDisappearing += (sender, args) => {
                var items = (this.ItemsSource as IList);

                if (args.Item == items[0])
                    _isFirstVisible = false;
            };
        }

        public ICommand ItemClickCommand
        {
            get { return (ICommand)this.GetValue(ItemClickCommandProperty); }
            set { this.SetValue(ItemClickCommandProperty, value); }
        }

        public ICommand LoadMoreCommand
        {
            get { return (this.GetValue(LoadMoreCommandProperty) as ICommand); }
            set { this.SetValue(LoadMoreCommandProperty, value); }
        }
    }
    #endregion
}

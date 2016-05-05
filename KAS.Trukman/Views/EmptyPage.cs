using KAS.Trukman.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Views.Pages
{
    #region EmptyPage
    public class EmptyPage : TrukmanPage
    {
        public EmptyPage()
            : base()
        {
            this.BindingContext = new EmptyViewModel();
        }

        protected override View CreateContent()
        {
            var busyIndicator = new ActivityIndicator
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };
            busyIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy", BindingMode.TwoWay);

            var content = new Grid
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 0
            };
            content.Children.Add(busyIndicator);

            return content;
        }

        public new EmptyViewModel ViewModel
        {
            get { return (this.BindingContext as EmptyViewModel); }
        }
    }
    #endregion
}

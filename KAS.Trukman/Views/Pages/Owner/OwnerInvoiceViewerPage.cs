using KAS.Trukman.Controls;
using KAS.Trukman.Helpers;
using KAS.Trukman.ViewModels.Pages.Owner;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Views.Pages.Owner
{
    #region OwnerInvoiceViewerPage
    public class OwnerInvoiceViewerPage : TrukmanPage
    {
        public OwnerInvoiceViewerPage() 
            : base()
        {
            this.BindingContext = new OwnerInvoiceViewerViewModel();
        }

        protected override View CreateContent()
        {
            var titleBar = new TitleBar
            {
                LeftIcon = PlatformHelper.LeftImageSource
            };
            titleBar.SetBinding(TitleBar.TitleProperty, "Title", BindingMode.OneWay);
            titleBar.SetBinding(TitleBar.LeftCommandProperty, "ShowPrevPageCommand", BindingMode.OneWay);

            var pdfView = new PDFView {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
            };
            pdfView.SetBinding(PDFView.UriProperty, "InvoiceUri", BindingMode.OneWay);

            var content = new Grid
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                ColumnSpacing = 0,
                RowSpacing = 0,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                }
            };
            content.Children.Add(titleBar, 0, 0);
            content.Children.Add(pdfView, 0, 1);

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

        public new OwnerInvoiceViewerViewModel ViewModel
        {
            get { return (this.BindingContext as OwnerInvoiceViewerViewModel); }
        }
    }
    #endregion
}

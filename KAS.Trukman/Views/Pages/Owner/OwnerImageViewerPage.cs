using System;

using Xamarin.Forms;
using KAS.Trukman.Storage.ParseClasses;
using KAS.Trukman.Data.Classes;
using KAS.Trukman.Views.Pages;
using KAS.Trukman.ViewModels.Pages.Owner;
using KAS.Trukman.Helpers;
using KAS.Trukman.Controls;

namespace KAS.Trukman
{
    #region OwnerImageViewerPage
    public class OwnerImageViewerPage : TrukmanPage
	{
		public OwnerImageViewerPage ()
            : base()
		{
            this.BindingContext = new OwnerImageViewerViewModel();
		}

        protected override View CreateContent()
        {
            var titleBar = new TitleBar
            {
                LeftIcon = PlatformHelper.LeftImageSource
            };
            titleBar.SetBinding(TitleBar.TitleProperty, "Title", BindingMode.OneWay);
            titleBar.SetBinding(TitleBar.LeftCommandProperty, "ShowPrevPageCommand", BindingMode.OneWay);

            var image = new Image
            {
                Aspect = Aspect.AspectFit,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            image.SetBinding(Image.SourceProperty, "PhotoUri", BindingMode.OneWay);

            var scrollView = new ScrollView
            {
                Padding = 20,
                Content = new StackLayout
                {
                    Spacing = 10,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Children = { image }
                }
            };

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
            content.Children.Add(scrollView, 0, 1);

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

        public new OwnerImageViewerViewModel ViewModel
        {
            get { return (this.BindingContext as OwnerImageViewerViewModel); }
        }
    }
    #endregion
}



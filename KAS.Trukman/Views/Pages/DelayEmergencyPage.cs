using KAS.Trucman.Converters;
using KAS.Trukman.Controls;
using KAS.Trukman.Helpers;
using KAS.Trukman.Languages;
using KAS.Trukman.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Views.Pages
{
    #region DelayEmergencyPage
    public class DelayEmergencyPage : TrukmanPage
    {
        public DelayEmergencyPage() 
            : base()
        {
            this.BindingContext = new DelayEmergencyViewModel();
        }

        protected override View CreateContent()
        {
            var titleBar = new TitleBar
            {
                RightIcon = PlatformHelper.HomeImageSource
            };
            titleBar.SetBinding(TitleBar.TitleProperty, "Title", BindingMode.OneWay);
            titleBar.SetBinding(TitleBar.LeftCommandProperty, "ShowHomePageCommand", BindingMode.OneWay);
            titleBar.SetBinding(TitleBar.RightCommandProperty, "ShowPrevPageCommand", BindingMode.OneWay);

            var timeImage = new Image
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                HeightRequest = 180,
                WidthRequest = 180,
                Source = PlatformHelper.TimeImageSource
            };

            var timeContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Content = timeImage
            };

            var selectText = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                TextColor = PlatformHelper.FuelAdvanceTextColor,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };
            selectText.SetBinding(Label.TextProperty, new Binding("DelaySelectTypeLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var selectContent = new ContentView
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(20, 0, 20, 0),
                Content = selectText
            };

            var delayEmergencyItemsToColorConverter = new DelayEmergencyItemsToColorConverter();

            var flatTireItemText = new TappedLabel {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                TapCommandParameter = 0
            };
            flatTireItemText.SetBinding(TappedLabel.TextProperty, new Binding("DelayFlatTireLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            flatTireItemText.SetBinding(TappedLabel.TextColorProperty, new Binding("SelectedItem", BindingMode.OneWay, delayEmergencyItemsToColorConverter, 0));
            flatTireItemText.SetBinding(TappedLabel.TapCommandProperty, "SelectItemCommand", BindingMode.OneWay);

            var feelingSleepyItemText = new TappedLabel
            {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                TapCommandParameter = 1
            };
            feelingSleepyItemText.SetBinding(TappedLabel.TextProperty, new Binding("DelayFeelingSleepyLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            feelingSleepyItemText.SetBinding(TappedLabel.TextColorProperty, new Binding("SelectedItem", BindingMode.OneWay, delayEmergencyItemsToColorConverter, 1));
            feelingSleepyItemText.SetBinding(TappedLabel.TapCommandProperty, "SelectItemCommand", BindingMode.OneWay);

            var roadWorkAheadItemText = new TappedLabel
            {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                TapCommandParameter = 2
            };
            roadWorkAheadItemText.SetBinding(TappedLabel.TextProperty, new Binding("DelayRoadWorkAheadLabel", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            roadWorkAheadItemText.SetBinding(TappedLabel.TextColorProperty, new Binding("SelectedItem", BindingMode.OneWay, delayEmergencyItemsToColorConverter, 2));
            roadWorkAheadItemText.SetBinding(TappedLabel.TapCommandProperty, "SelectItemCommand", BindingMode.OneWay);

            var itemsLayout = new StackLayout {
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(80, 0, 80, 0)
            };
            itemsLayout.Children.Add(flatTireItemText);
            itemsLayout.Children.Add(feelingSleepyItemText);
            itemsLayout.Children.Add(roadWorkAheadItemText);

            var commentsEntry = new AppEntry
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Entry)),
                PlaceholderColor = PlatformHelper.EntryPlaceholderColor
            };
            commentsEntry.SetBinding(AppEntry.TextProperty, "Comments", BindingMode.TwoWay);
            commentsEntry.SetBinding(AppEntry.PlaceholderProperty, new Binding("DelayCommentsPlaceholderText", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));

            var commentsContent = new ContentView {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Padding= new Thickness(20, 0, 20, 0),
                Content = commentsEntry
            };

            var submitButton = new AppButton {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center
            };
            submitButton.SetBinding(AppButton.TextProperty, new Binding("DelaySubmitButtonText", BindingMode.OneWay, null, null, null, AppLanguages.CurrentLanguage));
            submitButton.SetBinding(AppButton.CommandProperty, "SubmitCommand");

            var submitContent = new ContentView {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Padding = new Thickness(20, 0, 20, 0),
                Content = submitButton
            };

            var content = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 0,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength(3, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                }
            };
            content.Children.Add(timeContent, 0, 0);
            content.Children.Add(selectContent, 0, 1);
            content.Children.Add(itemsLayout, 0, 2);
            content.Children.Add(commentsContent, 0, 3);
            content.Children.Add(submitContent, 0, 4);

            var scrollView = new ScrollView {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Content = content
            };

            var busyIndicator = new ActivityIndicator
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };
            busyIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy", BindingMode.TwoWay);

            var pageContent = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                RowSpacing = 0,
                ColumnSpacing = 0,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                }
            };
            pageContent.Children.Add(titleBar, 0, 0);
            pageContent.Children.Add(scrollView, 0, 1);
            pageContent.Children.Add(busyIndicator, 0, 1);

            return pageContent;
        }

        public new DelayEmergencyViewModel ViewModel
        {
            get { return (this.BindingContext as DelayEmergencyViewModel); }
        }
    }
    #endregion
}

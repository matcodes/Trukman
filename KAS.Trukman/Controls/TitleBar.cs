using KAS.Trukman.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace KAS.Trukman.Controls
{
    #region TitleBar
    public class TitleBar : Grid
    {
        #region Static member
        public static BindableProperty TitleProperty = BindableProperty.Create("Title", typeof(string), typeof(TitleBar), default(string), BindingMode.OneWay, null, null);
        public static BindableProperty LeftCommandProperty = BindableProperty.Create("LeftCommand", typeof(ICommand), typeof(TitleBar), null, BindingMode.OneWay, null, null);
        public static BindableProperty LeftIconProperty = BindableProperty.Create("LeftIcon", typeof(string), typeof(TitleBar), PlatformHelper.MenuImageSource, BindingMode.OneWay, null, null);
        public static BindableProperty RightCommandProperty = BindableProperty.Create("RightCommand", typeof(ICommand), typeof(TitleBar), null, BindingMode.OneWay, null, null);
        public static BindableProperty RightIconProperty = BindableProperty.Create("RightIcon", typeof(string), typeof(TitleBar), PlatformHelper.LeftImageSource, BindingMode.OneWay, null, null);
        #endregion

        private ContentView _leftCommandContent = null;
        private ContentView _rightCommandContent = null;

        public TitleBar() 
            : base()
        {
            this.BackgroundColor = Color.Transparent;

            this.HorizontalOptions = LayoutOptions.Fill;

            this.HeightRequest = PlatformHelper.ActionBarHeight;

            this.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(PlatformHelper.ActionBarHeight, GridUnitType.Absolute) });
            this.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            this.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(PlatformHelper.ActionBarHeight, GridUnitType.Absolute) });

            var leftCommandButton = new ToolButton {
            };
            leftCommandButton.SetBinding(ToolButton.CommandProperty, new Binding("LeftCommand", BindingMode.OneWay, null, null, null, this));
            leftCommandButton.SetBinding(ToolButton.ImageSourceNameProperty, new Binding("LeftIcon", BindingMode.OneWay, null, null, null, this));

            _leftCommandContent = new ContentView {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Padding = new Thickness(4),
                Content = leftCommandButton,
                IsVisible = false
            };

            var titleLabel = new Label {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = PlatformHelper.TitleBarFontSize,
                TextColor = Color.White
            };
            titleLabel.SetBinding(Label.TextProperty, new Binding("Title", BindingMode.OneWay, null, null, null, this));

            var titleLabelContent = new ContentView {
                Padding = new Thickness(10, 0, 10, 0),
                Content = titleLabel
            };

            var rightCommandButton = new ToolButton {
            };
            rightCommandButton.SetBinding(ToolButton.CommandProperty, new Binding("RightCommand", BindingMode.OneWay, null, null, null, this));
            rightCommandButton.SetBinding(ToolButton.ImageSourceNameProperty, new Binding("RightIcon", BindingMode.OneWay, null, null, null, this));
                       
            _rightCommandContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Padding = new Thickness(4),
                Content = rightCommandButton,
                IsVisible = false
            };

            this.Children.Add(_leftCommandContent, 0, 0);
            this.Children.Add(titleLabelContent, 1, 0);
            this.Children.Add(_rightCommandContent, 2, 0);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == "LeftCommand") 
                _leftCommandContent.IsVisible = (this.LeftCommand != null);
            if (propertyName == "RightCommand")
                _rightCommandContent.IsVisible = (this.RightCommand != null);
        }

        public string Title
        {
            get { return (string)this.GetValue(TitleProperty); }
            set { this.SetValue(TitleProperty, value); }
        }

        public ICommand LeftCommand
        {
            get { return (ICommand)this.GetValue(LeftCommandProperty); }
            set { this.SetValue(LeftCommandProperty, value); }
        }

        public string LeftIcon
        {
            get { return (string)this.GetValue(LeftIconProperty); }
            set { this.SetValue(LeftIconProperty, value); }
        }

        public ICommand RightCommand
        {
            get { return (ICommand)this.GetValue(RightCommandProperty); }
            set { this.SetValue(RightCommandProperty, value); }
        }

        public string RightIcon
        {
            get { return (string)this.GetValue(RightIconProperty); }
            set { this.SetValue(RightIconProperty, value); }
        }
    }
    #endregion
}

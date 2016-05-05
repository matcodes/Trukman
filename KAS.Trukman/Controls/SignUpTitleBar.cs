using KAS.Trukman.Helpers;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace KAS.Trukman.Controls
{
    #region SignUpTitleBar
    public class SignUpTitleBar : Grid
    {
        #region Static members
        public static BindableProperty TitleProperty = BindableProperty.Create("Title", typeof(string), typeof(SignUpTitleBar), default(string), BindingMode.OneWay, null, null);
        public static BindableProperty LeftCommandProperty = BindableProperty.Create("LeftCommand", typeof(ICommand), typeof(SignUpTitleBar), null, BindingMode.OneWay, null, null);
        public static BindableProperty LeftIconProperty = BindableProperty.Create("LeftIcon", typeof(string), typeof(SignUpTitleBar), PlatformHelper.MenuImageSource, BindingMode.OneWay, null, null);
        public static BindableProperty EnglishLabelTextProperty = BindableProperty.Create("EnglishLabelText", typeof(string), typeof(SignUpTitleBar), default(string), BindingMode.OneWay, null, null);
        public static BindableProperty EnglishLabelTextColorProperty = BindableProperty.Create("EnglishLabelTextColor", typeof(Color), typeof(SignUpTitleBar), Color.Default, BindingMode.OneWay, null, null);
        public static BindableProperty EnglishCommandProperty = BindableProperty.Create("EnglishCommand", typeof(ICommand), typeof(SignUpTitleBar), null, BindingMode.OneWay, null, null);
        public static BindableProperty EnglishCommandParameterProperty = BindableProperty.Create("EnglishCommandParameter", typeof(object), typeof(SignUpTitleBar), null, BindingMode.OneWay, null, null);
        public static BindableProperty EspanolLabelTextProperty = BindableProperty.Create("EspanolLabelText", typeof(string), typeof(SignUpTitleBar), default(string), BindingMode.OneWay, null, null);
        public static BindableProperty EspanolLabelTextColorProperty = BindableProperty.Create("EspanolLabelTextColor", typeof(Color), typeof(SignUpTitleBar), Color.Default, BindingMode.OneWay, null, null);
        public static BindableProperty EspanolCommandProperty = BindableProperty.Create("EspanolCommand", typeof(ICommand), typeof(SignUpTitleBar), null, BindingMode.OneWay, null, null);
        public static BindableProperty EspanolCommandParameterProperty = BindableProperty.Create("EspanolCommandParameter", typeof(object), typeof(SignUpTitleBar), null, BindingMode.OneWay, null, null);
        #endregion

        private ContentView _leftCommandContent = null;

        public SignUpTitleBar()
            : base()
        {
            this.BackgroundColor = Color.Transparent;

            this.HorizontalOptions = LayoutOptions.Fill;

            this.HeightRequest = PlatformHelper.ActionBarHeight;

            this.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(PlatformHelper.ActionBarHeight, GridUnitType.Absolute) });
            this.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            this.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
            this.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });

            var leftCommandButton = new ToolButton
            {
            };
            leftCommandButton.SetBinding(ToolButton.CommandProperty, new Binding("LeftCommand", BindingMode.OneWay, null, null, null, this));
            leftCommandButton.SetBinding(ToolButton.ImageSourceNameProperty, new Binding("LeftIcon", BindingMode.OneWay, null, null, null, this));

            _leftCommandContent = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Padding = new Thickness(4),
                Content = leftCommandButton,
                IsVisible = false
            };

            var titleLabel = new Label
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = PlatformHelper.TitleBarFontSize,
                TextColor = Color.White
            };
            titleLabel.SetBinding(Label.TextProperty, new Binding("Title", BindingMode.OneWay, null, null, null, this));

            var english = new TappedLabel
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Fill,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))
            };
            english.SetBinding(TappedLabel.TextProperty, new Binding("EnglishLabelText", BindingMode.OneWay, null, null, null, this));
            english.SetBinding(TappedLabel.TextColorProperty, new Binding("EnglishLabelTextColor", BindingMode.OneWay, null, null, null, this));
            english.SetBinding(TappedLabel.TapCommandParameterProperty, new Binding("EnglishCommandParameter", BindingMode.OneWay, null, null, null, this));
            english.SetBinding(TappedLabel.TapCommandProperty, new Binding("EnglishCommand", BindingMode.OneWay, null, null, null, this));

            var englishContent = new ContentView
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Center,
                Content = english
            };

            var espanol = new TappedLabel
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Fill,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))
            };
            espanol.SetBinding(TappedLabel.TextProperty, new Binding("EspanolLabelText", BindingMode.OneWay, null, null, null, this));
            espanol.SetBinding(TappedLabel.TextColorProperty, new Binding("EspanolLabelTextColor", BindingMode.OneWay, null, null, null, this));
            espanol.SetBinding(TappedLabel.TapCommandParameterProperty, new Binding("EspanolCommandParameter", BindingMode.OneWay, null, null, null, this));
            espanol.SetBinding(TappedLabel.TapCommandProperty, new Binding("EspanolCommand", BindingMode.OneWay, null, null, null, this));

            var espanolContent = new ContentView
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Center,
                Padding = new Thickness(10, 0, 10, 0),
                Content = espanol
            };

            var titleLabelContent = new ContentView
            {
                Padding = new Thickness(10, 0, 10, 0),
                Content = titleLabel
            };

            this.Children.Add(_leftCommandContent, 0, 0);
            this.Children.Add(titleLabelContent, 1, 0);
            this.Children.Add(englishContent, 2, 0);
            this.Children.Add(espanolContent, 3, 0);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == "LeftCommand")
                _leftCommandContent.IsVisible = (this.LeftCommand != null);
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

        public string EnglishLabelText
        {
            get { return (string)this.GetValue(EnglishLabelTextProperty); }
            set { this.SetValue(EnglishLabelTextProperty, value); }
        }

        public Color EnglishLabelTextColor
        {
            get { return (Color)this.GetValue(EnglishLabelTextColorProperty); }
            set { this.SetValue(EnglishLabelTextColorProperty, value); }
        }

        public ICommand EnglishCommand
        {
            get { return (ICommand)this.GetValue(EnglishCommandProperty); }
            set { this.SetValue(EnglishCommandProperty, value); }
        }

        public object EnglishCommandParameter
        {
            get { return this.GetValue(EnglishCommandParameterProperty); }
            set { this.SetValue(EnglishCommandParameterProperty, value); }
        }

        public string EspanolLabelText
        {
            get { return (string)this.GetValue(EspanolLabelTextProperty); }
            set { this.SetValue(EspanolLabelTextProperty, value); }
        }

        public Color EspanolLabelTextColor
        {
            get { return (Color)this.GetValue(EspanolLabelTextColorProperty); }
            set { this.SetValue(EspanolLabelTextColorProperty, value); }
        }

        public ICommand EspanolCommand
        {
            get { return (ICommand)this.GetValue(EspanolCommandProperty); }
            set { this.SetValue(EspanolCommandProperty, value); }
        }

        public object EspanolCommandParameter
        {
            get { return this.GetValue(EspanolCommandParameterProperty); }
            set { this.SetValue(EspanolCommandParameterProperty, value); }
        }
    }
    #endregion
}

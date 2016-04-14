using System;
using Xamarin.Forms;
using System.Windows.Input;

namespace Trukman
{
	public class NavBar : Grid
	{
		ContentView messageContent = null;
		ContentView mapContent = null;
		ContentView advancesContent = null;
		ContentView delayContent = null;
		ContentView cameraContent = null;

		public static BindableProperty MessagesCommandProperty = BindableProperty.Create("MessagesCommand", typeof(ICommand), typeof(NavBar), null, BindingMode.OneWay);
		public static BindableProperty MessagesIconProperty = BindableProperty.Create("MessagesIcon", typeof(string), typeof(NavBar), PlatformHelper.MessageImageSource, BindingMode.OneWay);
		public static BindableProperty MapCommandProperty = BindableProperty.Create("MapCommand", typeof(ICommand), typeof(NavBar), null, BindingMode.OneWay);
		public static BindableProperty MapIconProperty = BindableProperty.Create("MapIcon", typeof(string), typeof(NavBar), PlatformHelper.TripImageSource, BindingMode.OneWay);
		public static BindableProperty AdvancesCommandProperty = BindableProperty.Create("AdvancesCommand", typeof(ICommand), typeof(NavBar), null, BindingMode.OneWay);
		public static BindableProperty AdvancesIconProperty = BindableProperty.Create("AdvancesIcon", typeof(string), typeof(NavBar), PlatformHelper.AdvancesImageSource, BindingMode.OneWay);
		public static BindableProperty DelayCommandProperty = BindableProperty.Create("DelayCommand", typeof(ICommand), typeof(NavBar), null, BindingMode.OneWay);
		public static BindableProperty DelayIconProperty = BindableProperty.Create("DelayIcon", typeof(string), typeof(NavBar), PlatformHelper.DelayImageSource, BindingMode.OneWay);
		public static BindableProperty CameraCommandProperty = BindableProperty.Create("CameraCommand", typeof(ICommand), typeof(NavBar), null, BindingMode.OneWay);
		public static BindableProperty CameraIconProperty = BindableProperty.Create("CameraIcon", typeof(string), typeof(NavBar), PlatformHelper.CameraImageSource, BindingMode.OneWay);

		public NavBar (): base()
		{
			this.BackgroundColor = Color.Transparent;
			this.HorizontalOptions = LayoutOptions.Fill;

			this.HeightRequest = PlatformHelper.ActionBarHeight;

			this.ColumnDefinitions.Add (new ColumnDefinition{ Width = new GridLength (1, GridUnitType.Star) });
			this.ColumnDefinitions.Add (new ColumnDefinition{ Width = new GridLength (1, GridUnitType.Star) });
			this.ColumnDefinitions.Add (new ColumnDefinition{ Width = new GridLength (1, GridUnitType.Star) });
			this.ColumnDefinitions.Add (new ColumnDefinition{ Width = new GridLength (1, GridUnitType.Star) });
			this.ColumnDefinitions.Add (new ColumnDefinition{ Width = new GridLength (1, GridUnitType.Star) });

			var messageButton = new ToolButton 
			{
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
			};
			messageContent = new ContentView {
				Padding = new Thickness (20, 15),
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				Content = messageButton,
			};
			messageButton.SetBinding(ToolButton.ImageSourceNameProperty, new Binding("MessagesIcon", BindingMode.OneWay, null ,null, null, this));

			var mapButton = new ToolButton { 
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
			};
			mapButton.SetBinding(ToolButton.ImageSourceNameProperty, new Binding("MapIcon", BindingMode.OneWay, null ,null, null, this));
			mapButton.SetBinding (ToolButton.CommandProperty, new Binding ("MapCommand", BindingMode.OneWay, null, null, null, this));
				
			mapContent = new ContentView {
				Padding = new Thickness (20, 15),
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				Content = mapButton
			};

			var advancesButton = new ToolButton();
			advancesButton.SetBinding(ToolButton.ImageSourceNameProperty, new Binding("AdvancesIcon", BindingMode.OneWay, null ,null, null, this));
			advancesContent = new ContentView {
				Padding = new Thickness (20, 15),
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				Content = advancesButton
			};

			var delayButton = new ToolButton ();
			delayButton.SetBinding(ToolButton.ImageSourceNameProperty, new Binding("DelayIcon", BindingMode.OneWay, null ,null, null, this));
			delayContent = new ContentView {
				Padding = new Thickness (20, 15),
				Content = delayButton
			};

			var cameraButton = new ToolButton ();
			cameraButton.SetBinding(ToolButton.ImageSourceNameProperty, new Binding("CameraIcon", BindingMode.OneWay, null ,null, null, this));
			cameraContent = new ContentView {
				Padding = new Thickness(20, 15),
				Content = cameraButton
			};

			this.Children.Add (messageContent, 0, 0);
			this.Children.Add (mapContent, 1, 0);
			this.Children.Add (advancesContent, 2, 0);
			this.Children.Add (delayContent, 3, 0);
			this.Children.Add (cameraContent, 4, 0);
		}

		protected override void OnPropertyChanged (string propertyName)
		{
			base.OnPropertyChanged (propertyName);
			/*if (propertyName == "MessagesCommand")
				messageContent.IsVisible = (this.MessagesCommand != null);*/
		}

		public ICommand MessagesCommand 
		{
			get { return (ICommand)this.GetValue (MessagesCommandProperty); }
			set { this.SetValue (MessagesCommandProperty, value); }
		}

		public string MessagesIcon
		{
			get { return (string)this.GetValue (MessagesIconProperty); }
			set { this.SetValue (MessagesIconProperty, value); }
		}

		public ICommand MapCommand
		{
			get { return (ICommand)this.GetValue (MapCommandProperty); }
			set { this.SetValue (MapCommandProperty, value); }
		}

		public string MapIcon 
		{
			get { return (string)this.GetValue (MapIconProperty); }
			set { this.SetValue (MapIconProperty, value); }
		}

		public ICommand AdvancesCommand
		{
			get { return (ICommand)this.GetValue (AdvancesCommandProperty); }
			set { this.SetValue (AdvancesCommandProperty, value); }
		}
		public string AdvancesIcon
		{
			get { return (string)this.GetValue (AdvancesIconProperty); }
			set { this.SetValue (AdvancesIconProperty, value); }
		}

		public ICommand DelayCommand
		{
			get { return (ICommand)this.GetValue (DelayCommandProperty); }
			set { this.SetValue (DelayCommandProperty, value); }
		}

		public string DelayIcon
		{
			get { return (string)this.GetValue (DelayIconProperty); }
			set { this.SetValue (DelayIconProperty, value); }
		}

		public ICommand CameraCommand
		{
			get { return (ICommand)this.GetValue (CameraCommandProperty); }
			set { this.SetValue (CameraCommandProperty, value); }
		}

		public string CameraIcon
		{
			get { return (string)this.GetValue (CameraIconProperty); }
			set { this.SetValue (CameraIconProperty, value); }
		}
	}
}


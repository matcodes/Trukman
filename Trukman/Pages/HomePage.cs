using System;

using Xamarin.Forms;
using System.Collections.Generic;
using Trukman.ViewModels.Pages;

namespace Trukman
{
	public class HomePage : TrukmanPage
	{
		public HomePage () : base()
		{
			this.BindingContext = new HomeViewModel ();
		}

		protected override View CreateContent ()
		{
			var titleBar = new TitleBar
			{
				RightIcon = PlatformHelper.HomeImageSource
			};
			titleBar.SetBinding(TitleBar.TitleProperty, "Title", BindingMode.OneWay);
			titleBar.SetBinding(TitleBar.LeftCommandProperty, "ShowHomePageCommand", BindingMode.OneWay);
			titleBar.SetBinding(TitleBar.RightCommandProperty, "ShowPrevPageCommand", BindingMode.OneWay);

			var menu = new MenuList ();

			var layout = new StackLayout {
				HorizontalOptions = LayoutOptions.Fill,
				Padding = new Thickness(10, 10, 10, 0),
				Children = {
					menu
				}
			};

			var navBar = new NavBar ();

			navBar.SetBinding (NavBar.MapCommandProperty, "MapPageCommand", BindingMode.OneWay); 

			var content = new Grid {
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill,
				ColumnSpacing = 0,
				RowSpacing = 0,
				RowDefinitions = {
					new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
					new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
					new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) }
				}
			};

			content.Children.Add (titleBar, 0, 0);
			content.Children.Add (layout, 0, 1);
			content.Children.Add (navBar, 0, 2);

			return content;
		}

		public new HomeViewModel ViewModel
		{
			get { return (base.ViewModel as HomeViewModel); }
		}

		/*void OnItemSelected (object sender, SelectedItemChangedEventArgs e)
		{
			var item = e.SelectedItem as Trukman.MenuList.ItemsMenu;
			if (item != null)
			{
				if (item.TargetType != null) {
					Navigation.PushAsync((Page)Activator.CreateInstance (item.TargetType));
				}
				//((MenuList)sender).SelectedItem = null;
				//IsPresented = false;
			}
		}*/
	}
}

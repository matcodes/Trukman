using System;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections;
using Trukman.ViewModels.Pages;

namespace Trukman
{
	public class DriverListPage : TrukmanPage
	{
		Timer timerForLoadDrivers;
		int refreshTime = 15 * 1000; // Обновление карты каждые 15 сек.

		Map map;
		ListView listView;
		IList<Trukman.User> driverList;

		public DriverListPage ()
		{
			this.BindingContext = new DriverListViewModel();

		}

		protected override View CreateContent ()
		{
			var titleBar = new TitleBar 
			{
				RightIcon = PlatformHelper.HomeImageSource,
				HorizontalOptions = LayoutOptions.FillAndExpand
			};
			titleBar.SetBinding(TitleBar.TitleProperty, "Title", BindingMode.OneWay);
			titleBar.SetBinding(TitleBar.LeftCommandProperty, "ShowHomePageCommand", BindingMode.OneWay);
			titleBar.SetBinding(TitleBar.RightCommandProperty, "ShowPrevPageCommand", BindingMode.OneWay);

			/*Label lblTitle = new Label {
				Text = "Driver list",
				FontSize = Device.GetNamedSize (NamedSize.Large, typeof(Label)),
				HorizontalOptions = LayoutOptions.Center
			};*/

			var template = new DataTemplate (typeof(MenuItemCell));
			//listView.Style = new Style (){ };
			template.SetBinding (MenuItemCell.UserNameProperty, "UserName");
			template.SetBinding (MenuItemCell.EmailProperty, "Email");

			listView = new ListView ();
			listView.SeparatorVisibility = SeparatorVisibility.Default;
			listView.IsPullToRefreshEnabled = true;
			listView.ItemTemplate = template;

			var listContent = new StackLayout {
				HorizontalOptions = LayoutOptions.Fill,
				Padding = new Thickness(10, 10, 10, 0),
				Children = {
					listView
				}
			};

			map = new Map() //MapSpan.FromCenterAndRadius(new Position(37,-122), Distance.FromMiles(0.3))) 
			{
				IsShowingUser = true,
				//HeightRequest = 800,
				//WidthRequest = 960,
				VerticalOptions = LayoutOptions.FillAndExpand,
				MapType = MapType.Satellite					
			};

			var mapContent = new ContentView {
				VerticalOptions = LayoutOptions.Fill,
				//HorizontalOptions = LayoutOptions.Fill,
				//Padding = new Thickness(50, 0, 50, 0),
				Content = map
			};

			TimerCallback callback = new TimerCallback (LoadDriverList);
			timerForLoadDrivers = new Timer (callback, null, 0, refreshTime);

			var content = new Grid {
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill,
				ColumnSpacing = 0,
				RowSpacing = 0,
				RowDefinitions = {
					new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
					new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
					new RowDefinition { Height = new GridLength(3, GridUnitType.Star) }
				}
			};

			content.Children.Add (titleBar, 0, 0);
			content.Children.Add (listContent, 0, 1);
			content.Children.Add (mapContent, 0, 2);

			/*var relativeLayout = new RelativeLayout ();
			relativeLayout.Children.Add (titleBar, 
				Constraint.RelativeToParent (parent => 0)
			);
			relativeLayout.Children.Add (listView, 
				Constraint.RelativeToParent (parent => 0),
				Constraint.RelativeToView (titleBar, (parent, view) => view.Y + view.Height)
			);

			relativeLayout.Children.Add(map,
				Constraint.RelativeToParent (parent => 0),
				Constraint.RelativeToView(listView, (Parent, View) => View.Y + View.Height),
				Constraint.RelativeToParent(parent => parent.Width),
				Constraint.RelativeToView(listView, (parent, View) => parent.Height - View.Y - View.Height)
			);*/

			return content;
		}

		async void LoadDriverList(object state)
		{
			string company = SettingsServiceHelper.GetCompany ();
			// Закачиваем список водителей
			driverList = await App.ServerManager.GetDriverList (company);

			// Очищаем карту
			Device.BeginInvokeOnMainThread (() => {
				map.Pins.Clear ();
			});

			// Рисуем пины с водителями
			foreach (var driver in driverList) {
				if (driver.location != null) {

					Device.BeginInvokeOnMainThread (() => {
						var position = new Position (driver.location.Latitude, driver.location.Longitude);
						var pin = new Pin () {
							Type = PinType.Place,
							Position = position,
							Label = driver.UserName // + ": time " + driver.location.updatedAt.ToString("F"),
						};

						map.Pins.Add (pin);
					});
				}
			}

			// Переводим карту на регион, где находится первый пин
			Device.BeginInvokeOnMainThread (() => {
				if (map.Pins.Count > 0)
					map.MoveToRegion (MapSpan.FromCenterAndRadius (map.Pins [0].Position, Distance.FromMiles (100)));

				// TODO: убрать потом, когда переделаю на Command'ы
				MenuItemCell.SourceList = driverList;
				listView.ItemsSource = driverList;
			});		
		}

		protected override void OnDisappearing ()
		{
			base.OnDisappearing ();

			timerForLoadDrivers.Dispose ();
		}
	}
}



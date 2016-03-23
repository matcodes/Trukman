using System;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections;

namespace Trukman
{
	public class DriverListPage : BasePage
	{
		Timer timerForLoadDrivers;
		int refreshTime = 15 * 1000; // Обновление карты каждые 15 сек.

		Map map;
		ListView listView;
		IList<Trukman.User> driverList;

		public DriverListPage ()
		{
			Title = "Drivers";

			Label lblTitle = new Label {
				Text = "Driver list",
				FontSize = Device.GetNamedSize (NamedSize.Large, typeof(Label))
			};

			map = new Map() //MapSpan.FromCenterAndRadius(new Position(37,-122), Distance.FromMiles(0.3))) 
			{
				IsShowingUser = true,
				HeightRequest = 300,
				WidthRequest = 960,
				VerticalOptions = LayoutOptions.FillAndExpand,
				MapType = MapType.Satellite					
			};

			var template = new DataTemplate (typeof(CustomCell));
			//listView.Style = new Style (){ };
			template.SetBinding (CustomCell.UserNameProperty, "UserName");
			template.SetBinding (CustomCell.EmailProperty, "Email");

			listView = new ListView ();
			/*listView.Header = new StackLayout () {
				Children = {
					new Label {
						Text = "Driver list",
						FontSize = Device.GetNamedSize (NamedSize.Large, typeof(Label))
					}
				}
			};*/
			//listView.ite
			listView.SeparatorVisibility = SeparatorVisibility.Default;
			listView.IsPullToRefreshEnabled = true;
			listView.ItemTemplate = template;

			TimerCallback callback = new TimerCallback (LoadDriverList);
			timerForLoadDrivers = new Timer (callback, null, 0, refreshTime);

			var relativeLayout = new RelativeLayout ();
			//relativeLayout.Children.Add (lblTitle, Constraint.RelativeToParent (parent => 0));
			relativeLayout.Children.Add (map, Constraint.RelativeToParent (parent => 0),
				Constraint.RelativeToParent (parent => 0),
				Constraint.RelativeToParent (parent => parent.Width),
				Constraint.RelativeToParent (parent => parent.Height)
			);
				/*Constraint.RelativeToView (lblTitle, (parent, view) => 0),
				Constraint.RelativeToView(lblTitle, (parent, view) => view.Height)
			);*/
			/*relativeLayout.Children.Add (listView, 
				Constraint.RelativeToView (map, (parent, view) => 0),
				Constraint.RelativeToView (map, (parent, view) => view.Y + view.Height)
			);*/

			Content = relativeLayout;
			/*
			Content = new StackLayout {
				Children =
				{
					map
				}
			};*/
		}

		async void LoadDriverList(object state)
		{
			// Закачиваем список водителей
			driverList = await App.ServerManager.GetDriverList ();

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
							Label = driver.UserName + ": time " + driver.location.updatedAt.ToString("F"),
						};

						map.Pins.Add (pin);
					});
				}
			}

			// Переводим карту на регион, где находится первый пин
			Device.BeginInvokeOnMainThread (() => {
				if (map.Pins.Count > 0)
					map.MoveToRegion (MapSpan.FromCenterAndRadius (map.Pins [0].Position, Distance.FromMiles (1)));

				// TODO: убрать потом, когда переделаю на Command'ы
				CustomCell.SourceList = driverList;
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



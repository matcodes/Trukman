using System;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Trukman
{
	public class DriverListPage : ContentPage
	{
		Map map;
		Timer timerForLoadDrivers;
		int refreshTime = 15 * 1000; // 1 минута

		public DriverListPage ()
		{
			Title = "Drivers";

			map = new Map() //MapSpan.FromCenterAndRadius(new Position(37,-122), Distance.FromMiles(0.3))) 
			{
				IsShowingUser = true,
				HeightRequest = 500,
				WidthRequest = 960,
				VerticalOptions = LayoutOptions.FillAndExpand,
				MapType = MapType.Satellite					
			};

			TimerCallback callback = new TimerCallback (LoadDriverList);
			timerForLoadDrivers = new Timer (callback, null, 0, refreshTime);

			Content = new StackLayout { 
				Children = {
					new Label {
						Text = "Drivers map",
						HorizontalOptions = LayoutOptions.Center,
						VerticalOptions = LayoutOptions.CenterAndExpand
					},
					map
				}
			};
		}

		async void LoadDriverList(object state)
		{
			// Очищаем карту
			Device.BeginInvokeOnMainThread (() => {
				map.Pins.Clear ();
			});

			// Закачиваем список водителей
			var driverList = await App.ServerManager.GetDriverList ();

			// Рисуем пины с водителями
			foreach (var driver in driverList) {
				if (driver.location != null) {

					Device.BeginInvokeOnMainThread (() => {
						var position = new Position (driver.location.Latitude, driver.location.Longitude);
						var pin = new Pin () {
							Type = PinType.Place,
							Position = position,
							Label = driver.Name
							//Address = "custom detail info"
						};

						map.Pins.Add (pin);
					});
				}
			}

			// Переводим карту на регион, где находится первый пин
			Device.BeginInvokeOnMainThread (() => {
				if (map.Pins.Count > 0)
					map.MoveToRegion (MapSpan.FromCenterAndRadius (map.Pins [0].Position, Distance.FromMiles (1)));
			});		
		}

		protected override void OnDisappearing ()
		{
			base.OnDisappearing ();

			timerForLoadDrivers.Dispose ();
		}
	}
}



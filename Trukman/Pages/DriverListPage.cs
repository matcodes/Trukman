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

		public DriverListPage ()
		{
			Title = "Drivers";

			map = new Map(MapSpan.FromCenterAndRadius(new Position(37,-122), Distance.FromMiles(0.3))) 
			{
				IsShowingUser = true,
				HeightRequest = 400,
				WidthRequest = 960,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			//Task.Factory.StartNew(() => 

			LoadDriverList();

			if (map.Pins.Count > 0)
				map.MoveToRegion (MapSpan.FromCenterAndRadius (map.Pins[0].Position, Distance.FromMiles (0.3)));

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

		void LoadDriverList()
		{
			var driverList = App.ServerManager.GetDriverList ();
			foreach (var driver in driverList) {
				if (driver.location != null) {
					var position = new Position (driver.location.Latitude, driver.location.Longitude);
					var pin = new Pin () {
						Type = PinType.Place,
						Position = position,
						Label = "custom pin",
						Address = "custom detail info"
					};

					map.Pins.Add (pin);
				}
			}
		}
	}
}



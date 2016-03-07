using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Preferences;

using Android.OS;
using Android.Locations;
using System.Linq;
using System.Collections.Generic;

namespace Trukman.Droid
{
	[Activity (Label = "Trukman.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity, ILocationListener
	{
		LocationManager _locationManager;

		string _locationProvider;
		long minTime = 1*60*1000; // раз в минуту
		float minDistance = 100; // каждые 100 метров

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Xamarin.FormsMaps.Init (this, bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);

			LoadApplication (new App ());

			// Менеджер сервера
			//App.ServerManager = new ServerManager();
			//App.ServerManager.Init ();

			// Местоположение по GPS
			InitializeLocationManager();

			//IGeolocator
		}

		void InitializeLocationManager()
		{
			_locationManager = (LocationManager) GetSystemService(LocationService);
			Criteria criteriaForLocationService = new Criteria
			{
				Accuracy = Accuracy.Fine
			};
			IList<string> acceptableLocationProviders = _locationManager.GetProviders(criteriaForLocationService, true);

			if (acceptableLocationProviders.Any())
			{
				_locationProvider = acceptableLocationProviders.First();
			}
			else
			{
				_locationProvider = string.Empty;
			}
		}

		protected override void OnDestroy ()
		{
			base.OnDestroy ();

			App.ServerManager.LogOut ();
		}

		protected override void OnResume ()
		{
			base.OnResume ();

			//App.GpsManager.Resume ();

			if (!string.IsNullOrEmpty (_locationProvider)) {
				_locationManager.RequestLocationUpdates (_locationProvider, minTime, minDistance, this);
				var location = _locationManager.GetLastKnownLocation (LocationManager.GpsProvider);
				if (location != null)
					App.ServerManager.SaveDriverLocation (new GPSLocation {
						Longitude = location.Longitude,
						Latitude = location.Latitude
					});
			}
		}

		protected override void OnPause ()
		{
			base.OnPause ();

			//App.GpsManager.Pause ();

			if (!string.IsNullOrEmpty (_locationProvider))
				_locationManager.RemoveUpdates (this); 
		}

		public void OnLocationChanged(Location location) 
		{
			App.ServerManager.SaveDriverLocation (new GPSLocation{ Longitude = location.Longitude, Latitude = location.Latitude });
		}

		public void OnProviderDisabled(string provider) {}

		public void OnProviderEnabled(string provider) {}

		public void OnStatusChanged(string provider, Availability status, Bundle extras) {}
	}
}


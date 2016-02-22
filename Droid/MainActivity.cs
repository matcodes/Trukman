using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Android.OS;
using Android.Locations;
using System.Linq;
using System.Collections.Generic;

namespace Trukman.Droid
{
	[Activity (Label = "Trukman.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity, ILocationListener
	{
		//TextView _addressText;
		//Location _currentLocation;
		LocationManager _locationManager;

		string _locationProvider;
		long minTime = 5*60*1000;
		float minDistance = 100;
		//TextView _locationText;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Xamarin.FormsMaps.Init (this, bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);

			LoadApplication (new App ());

			App.ServerManager = new ServerManager();
			App.ServerManager.Init ();

			//App.ServerManager.GetDriversInternal();
			//task.Wait ();

			InitializeLocationManager();
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
			//Log.Debug(TAG, "Using " + _locationProvider + ".");
		}

		protected override void OnDestroy ()
		{
			base.OnDestroy ();

			App.ServerManager.LogOut ();
		}

		protected override void OnResume ()
		{
			base.OnResume ();
			_locationManager.RequestLocationUpdates (_locationProvider, minTime, minDistance, this);
			var location = _locationManager.GetLastKnownLocation (LocationManager.GpsProvider);
			if (location != null)
				App.ServerManager.SaveDriverLocation (new GPSLocation {
					Longitude = location.Longitude,
					Latitude = location.Latitude
				});
		}

		protected override void OnPause ()
		{
			base.OnPause ();
			_locationManager.RemoveUpdates(this);
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


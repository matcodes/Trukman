using System;
using Trukman;
using Xamarin.Forms;
using Android.Content;
using Android.Locations;
using Trukman.Droid;
using System.Collections.Generic;
using System.Linq;
using Android.Provider;
using Android.App;
using Android.OS;

[assembly: Dependency(typeof(GPSManager))]

namespace Trukman.Droid
{
	[Activity (Name="com.trukman.ui.GPSManageractivity", Label = "GPSManageractivity", Icon = "@drawable/icon")]
	public class GPSManager: /*Activity, */IGPSManager//, ILocationListener 
	{
		/*LocationManager _locationManager;
		string _locationProvider;
		long minTime = 5*60*1000;
		float minDistance = 100;
*/
		public GPSManager ()
		{			
		}

/*		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
		}
*/
		public bool IsTurnOnGPSLocation()
		{
			LocationManager locationManager = (LocationManager)Forms.Context.GetSystemService (Context.LocationService);
			return locationManager.IsProviderEnabled (LocationManager.GpsProvider);
		}

		public GPSLocation GetCurrentLocation()
		{
			LocationManager locationManager = (LocationManager)Forms.Context.GetSystemService (Context.LocationService);
			Location location = locationManager.GetLastKnownLocation (LocationManager.GpsProvider);
			if (location != null)
				return new GPSLocation{ Latitude = location.Latitude, Longitude = location.Longitude };
			else
				return new GPSLocation{ };
		}

		public void TryTurnOnGps()
		{
			Intent gpsSettingIntent = new Intent (Settings.ActionLocationSourceSettings);
			Forms.Context.StartActivity (gpsSettingIntent);
		}

		public void InitializeLocationManager()
		{
			/*_locationManager = (LocationManager) Forms.Context.GetSystemService(Context.LocationService);
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
			}*/
		}
		/*
		public void OnLocationChanged (Location location)
		{
			App.ServerManager.SaveDriverLocation (new GPSLocation{ Longitude = location.Longitude, Latitude = location.Latitude });
		}

		public void OnProviderDisabled (string provider)
		{
		}

		public void OnProviderEnabled (string provider)
		{
		}

		public void OnStatusChanged (string provider, Availability status, Android.OS.Bundle extras)
		{
		}*/

		public void Resume()
		{
			/*if (!string.IsNullOrEmpty (_locationProvider)) {
				_locationManager.RequestLocationUpdates (_locationProvider, minTime, minDistance, this);
				var location = _locationManager.GetLastKnownLocation (LocationManager.GpsProvider);
				if (location != null)
					App.ServerManager.SaveDriverLocation (new GPSLocation {
						Longitude = location.Longitude,
						Latitude = location.Latitude
					});
			}*/
		}

		public void Pause()
		{
			/*if (!string.IsNullOrEmpty (_locationProvider))
				_locationManager.RemoveUpdates (this);*/
		}
	}
}


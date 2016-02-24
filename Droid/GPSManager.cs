using System;
using Trukman;
using Xamarin.Forms;
using Android.Content;
using Android.Locations;
using Trukman.Droid;

[assembly: Dependency(typeof(GPSManager))]

namespace Trukman.Droid
{
	public class GPSManager: Java.Lang.Object, IGPSManager//, ILocationListener 
	{
		public GPSManager ()
		{
		}

		public bool IsTurnOnGPSLocation()
		{
			//Context context = Forms.Context;

			LocationManager locationManager = (LocationManager)Forms.Context.GetSystemService (Context.LocationService);
			return locationManager.IsProviderEnabled(LocationManager.GpsProvider);
		}

		public GPSLocation GetCurrentLocation()
		{
			LocationManager locationManager = (LocationManager)Forms.Context.GetSystemService (Context.LocationService);
			//var provider = locationManager.GetProvider (LocationManager.GpsProvider);
			Location location = locationManager.GetLastKnownLocation (LocationManager.GpsProvider);
			if (location != null)
				return new GPSLocation{ Latitude = location.Latitude, Longitude = location.Longitude };
			else
				return new GPSLocation{ };
		}
	}
}


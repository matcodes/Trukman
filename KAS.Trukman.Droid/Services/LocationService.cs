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
using Trukman.Helpers;
using Trukman.Interfaces;
using Xamarin.Forms.Maps;

[assembly: Dependency(typeof(LocationService))]

namespace Trukman.Droid
{
	[Service]
	public class LocationService: Service, ILocationService, ILocationListener 
	{
		public event EventHandler<LocationChangedEventArgs> LocationChanged = delegate { };
		public event EventHandler<ProviderDisabledEventArgs> ProviderDisabled = delegate { };
		public event EventHandler<ProviderEnabledEventArgs> ProviderEnabled = delegate { };
		public event EventHandler<StatusChangedEventArgs> StatusChanged = delegate { };

		public LocationService ()
		{			
		}

		IBinder binder;

		LocationManager _locationManager = (LocationManager)Android.App.Application.Context.GetSystemService (Context.LocationService);
		string _locationProvider;
		float minDistance = 100;
        long minTime = 1 * 60 * 1000; // Каждую 1 мин.

        public override void OnCreate()
		{
			base.OnCreate ();
		}

		public override IBinder OnBind (Intent intent)
		{
			binder = new LocationServiceBinder (this);
			return binder;
		}

		public override StartCommandResult OnStartCommand (Intent intent, StartCommandFlags flags, int startId)
		{
			return StartCommandResult.Sticky;
		}

		/*public bool CheckGPS()
		{
			return _locationManager.IsProviderEnabled (LocationManager.GpsProvider);
		}*/
		
		public Position GetCurrentLocation()
		{
			LocationManager locationManager = (LocationManager)Forms.Context.GetSystemService (Context.LocationService);
			Location location = locationManager.GetLastKnownLocation (LocationManager.GpsProvider);
			if (location != null)
				return new Position (location.Latitude, location.Longitude);
			else
				return new Position();
		}

		/*public void TryTurnOnGps()
		{
			Intent gpsSettingIntent = new Intent (Settings.ActionLocationSourceSettings);
			Forms.Context.StartActivity (gpsSettingIntent);
		}*/

		public void StartLocationUpdates()
		{
			Criteria criteriaForLocationService = new Criteria
			{
				Accuracy = Accuracy.Fine
			};
			IList<string> acceptableLocationProviders = _locationManager.GetProviders(criteriaForLocationService, true);

			if (acceptableLocationProviders.Any())
			{
				_locationProvider = acceptableLocationProviders.First();
				_locationManager.RequestLocationUpdates (_locationProvider, minTime, minDistance, this);
			}
			else
			{
				_locationProvider = string.Empty;
			}
		}

		public override void OnDestroy ()
		{
			base.OnDestroy ();

			_locationManager.RemoveUpdates (this);
		}

		#region ILocationListener implementation
		public void OnLocationChanged (Location location)
		{
			this.LocationChanged (this, new LocationChangedEventArgs (location));
		}

		public void OnProviderDisabled (string provider)
		{
			this.ProviderDisabled (this, new ProviderDisabledEventArgs (provider));
		}

		public void OnProviderEnabled (string provider)
		{
			this.ProviderEnabled(this, new ProviderEnabledEventArgs (provider));
		}

		public void OnStatusChanged (string provider, Availability status, Android.OS.Bundle extras)
		{
			this.StatusChanged (this, new StatusChangedEventArgs (provider, status, extras));
		}
		#endregion
	}
}


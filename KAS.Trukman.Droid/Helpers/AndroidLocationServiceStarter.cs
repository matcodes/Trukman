using System;
using Trukman.Droid;
using Xamarin.Forms;
using Android.Locations;
using Trukman.Helpers;
using Trukman.Droid.Services;
using KAS.Trukman;
using Xamarin.Forms.Maps;

[assembly: Dependency(typeof(AndroidLocationServiceStarter))]

namespace Trukman.Droid
{
	public class AndroidLocationServiceStarter: ILocationServicePlatformStarter
	{
		object tag = null;

		public AndroidLocationServiceStarter ()
		{
		}

		#region ILocationServicePlatformStarter implementation

		public void StartService (object _tag)
		{
			tag = _tag;

			LocationServiceStarter.Current.LocationServiceConnected += (object sender, ServiceConnectedEventArgs e) => {
				LocationServiceStarter.Current.LocationService.LocationChanged += HandleLocationChanged;
				LocationServiceStarter.Current.LocationService.ProviderDisabled += HandleProviderDisabled;
				LocationServiceStarter.Current.LocationService.ProviderEnabled += HandleProviderEnabled;
				LocationServiceStarter.Current.LocationService.StatusChanged += HandleStatusChanged;
			};
		}

		public void StopService()
		{
			LocationServiceStarter.StopLocationService ();
		}

		#endregion

		#region Android Location Service methods
		public void HandleLocationChanged(object sender, LocationChangedEventArgs e)
		{
			string TripId = (tag != null ? (string)tag : null);
			if (!string.IsNullOrEmpty (TripId))
				App.ServerManager.SaveDriverLocation (TripId, new Position (e.Location.Latitude, e.Location.Longitude));
		}

		public void HandleProviderDisabled(object sender, ProviderDisabledEventArgs e)
		{
		}

		public void HandleProviderEnabled(object sender, ProviderEnabledEventArgs e)
		{
		}

		public void HandleStatusChanged(object sender, StatusChangedEventArgs e)
		{
		}

		#endregion

	}
}


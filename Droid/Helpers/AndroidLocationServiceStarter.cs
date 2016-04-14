using System;
using Trukman.Droid;
using Xamarin.Forms;
using Trukman.Droid.Services;
using Android.Locations;

[assembly: Dependency(typeof(AndroidLocationServiceStarter))]

namespace Trukman.Droid
{
	public class AndroidLocationServiceStarter: ILocationServicePlatformStarter
	{
		public AndroidLocationServiceStarter ()
		{
		}

		#region ILocationServicePlatformStarter implementation

		public void StartService ()
		{
			LocationServiceStarter.Current.LocationServiceConnected += (object sender, ServiceConnectedEventArgs e) => {
				LocationServiceStarter.Current.LocationService.LocationChanged += HandleLocationChanged;
				LocationServiceStarter.Current.LocationService.ProviderDisabled += HandleProviderDisabled;
				LocationServiceStarter.Current.LocationService.ProviderEnabled += HandleProviderEnabled;
				LocationServiceStarter.Current.LocationService.StatusChanged += HandleStatusChanged;
			};
		}

		#endregion

		#region Android Location Service methods
		public void HandleLocationChanged(object sender, LocationChangedEventArgs e)
		{
			Android.Locations.Location location = e.Location;
			App.ServerManager.SaveDriverLocation (new UserLocation{ Longitude = location.Longitude, Latitude = location.Latitude });
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


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
using Trukman.Droid.Services;

namespace Trukman.Droid
{
	[Activity (Label = "Trukman.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			//LocationClient 

			base.OnCreate (bundle);

			Xamarin.FormsMaps.Init (this, bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);

			LoadApplication (new App ());

			LocationServiceStarter.Current.LocationServiceConnected += (object sender, ServiceConnectedEventArgs e) => {
				LocationServiceStarter.Current.LocationService.LocationChanged += HandleLocationChanged;
				//LocationServiceStarter.Current.LocationService.OnProviderDisabled += HandleProviderDisabled;
				//LocationServiceStarter.Current.LocationService.OnProviderEnabled += HandleProviderEnabled;
				//LocationServiceStarter.Current.LocationService.OnStatusChanged += HandleStatusChanged;
			};
		}

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


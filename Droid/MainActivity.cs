using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Preferences;

using Android.OS;
using System.Linq;
using System.Collections.Generic;
using Trukman.Droid.Services;
using Android.Net;
using Trukman.Droid.Helpers;

namespace Trukman.Droid
{
	[Activity (Label = "Trukman.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Xamarin.FormsMaps.Init (this, bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);

			var platformHelper = new AndroidPlatformHelper(this);
			PlatformHelper.Initialize(platformHelper);

			var settingsService = new SettingsService ();
			SettingsServiceHelper.Initialize (settingsService);

			LoadApplication (new App ());

			/*ConnectivityManager connectivityManager = (ConnectivityManager) GetSystemService(ConnectivityService);
			NetworkInfo wifiInfo = connectivityManager.GetNetworkInfo(ConnectivityType.Wifi);
			var networks = connectivityManager.GetAllNetworks();

			NetworkInfo activeConnection = connectivityManager.ActiveNetworkInfo;
			bool isOnline = (activeConnection != null) && activeConnection.IsConnected;*/
		}

		protected override void OnStart ()
		{
			base.OnStart ();
		}

		protected override void OnResume ()
		{
			base.OnResume ();
		}

		protected override void OnPause ()
		{
			base.OnPause ();
		}

		protected override void OnStop ()
		{
			base.OnStop ();
		}

		protected override void OnDestroy ()
		{
			base.OnDestroy ();
		}

	}
}


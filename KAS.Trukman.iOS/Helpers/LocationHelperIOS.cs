﻿using System;
using CoreLocation;
using UIKit;
using KAS.Trukman.Helpers;
using Plugin.Geolocator.Abstractions;
namespace KAS.Trukman.iOS
{
	public class LocationUpdatedEventArgs : EventArgs
	{
		CLLocation location;

		public LocationUpdatedEventArgs(CLLocation location)
		{
			this.location = location;
		}

		public CLLocation Location
		{
			get { return location; }
		}
	}

	public class LocationHelperIOS
	{

		protected CLLocationManager locMgr;

		public event EventHandler<LocationUpdatedEventArgs> LocationUpdated = delegate { };

		public LocationHelperIOS ()
		{
			this.locMgr = new CLLocationManager ();
			this.locMgr.PausesLocationUpdatesAutomatically = false;

			// iOS 8 has additional permissions requirements
			if (UIDevice.CurrentDevice.CheckSystemVersion (8, 0)) {
				locMgr.RequestAlwaysAuthorization (); // works in background
				//locMgr.RequestWhenInUseAuthorization (); // only in foreground
			}

			// iOS 9 requires the following for background location updates
			// By default this is set to false and will not allow background updates
			if (UIDevice.CurrentDevice.CheckSystemVersion (9, 0)) {
				locMgr.AllowsBackgroundLocationUpdates = true;
			}

			LocationUpdated += SendLocation;
			StartLocationUpdates ();
		}

		public CLLocationManager LocMgr {
			get { return this.locMgr; }
		}

		public void StartLocationUpdates ()
		{

			// We need the user's permission for our app to use the GPS in iOS. This is done either by the user accepting
			// the popover when the app is first launched, or by changing the permissions for the app in Settings
			if (CLLocationManager.LocationServicesEnabled) {

				//set the desired accuracy, in meters
				LocMgr.DesiredAccuracy = 20;

				LocMgr.LocationsUpdated += (object sender, CLLocationsUpdatedEventArgs e) => {
					// fire our custom Location Updated event
					LocationUpdated (this, new LocationUpdatedEventArgs (e.Locations [e.Locations.Length - 1]));
				};

				LocMgr.StartUpdatingLocation ();
			}
		}

		//This will keep going in the background and the foreground
		public void SendLocation (object sender, LocationUpdatedEventArgs e)
		{
			CLLocation location = e.Location;
			Console.WriteLine ("Altitude: " + location.Altitude + " meters");
			Console.WriteLine ("Longitude: " + location.Coordinate.Longitude);
			Console.WriteLine ("Latitude: " + location.Coordinate.Latitude);
			Console.WriteLine ("Course: " + location.Course);
			Console.WriteLine ("Speed: " + location.Speed);

			Position pos = new Position ();
			pos.Latitude = location.Coordinate.Latitude;
			pos.Longitude = location.Coordinate.Longitude;

			LocationHelper.Update (pos);
		}
	}
}

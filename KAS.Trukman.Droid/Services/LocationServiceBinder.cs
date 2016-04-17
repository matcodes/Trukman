using System;
using Android.OS;

namespace Trukman.Droid
{
	public class LocationServiceBinder: Binder
	{
		public LocationService Service
		{
			get {return this.service; }
		} protected LocationService service;

		public bool IsBound {get;set;}

		public LocationServiceBinder (LocationService _service)
		{
			this.service = _service;
		}
	}
}


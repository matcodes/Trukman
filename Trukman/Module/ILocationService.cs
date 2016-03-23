using System;

using Xamarin.Forms;

namespace Trukman
{
	public class UserLocation
	{
		public double Latitude{get;set;}
		public double Longitude{get;set;}
		public DateTime updatedAt{get;set;}
	}

	public interface ILocationService
	{
		void StartLocationUpdates();
		bool IsTurnOnGPSLocation();
		void TryTurnOnGps();
		//UserLocation GetCurrentLocation();
	}
}



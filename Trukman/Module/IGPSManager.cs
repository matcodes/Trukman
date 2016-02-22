using System;

using Xamarin.Forms;

namespace Trukman
{
	public class GPSLocation
	{
		public double Latitude{get;set;}
		public double Longitude{get;set;}
	}

	public interface IGPSManager 
	{
		bool IsTurnOnGPSLocation();
		GPSLocation GetCurrentLocation();
	}
}



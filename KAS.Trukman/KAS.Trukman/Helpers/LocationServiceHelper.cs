using System;

using Xamarin.Forms;

namespace Trukman
{
	public interface ILocationServicePlatformStarter
	{
		void StartService();
	}

	public interface ILocationService
	{
		void StartLocationUpdates();
		bool IsTurnOnGPSLocation();
		void TryTurnOnGps();
		//UserLocation GetCurrentLocation();
	}
}



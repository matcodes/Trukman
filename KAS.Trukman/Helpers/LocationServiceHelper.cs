using System;

using Xamarin.Forms;

namespace Trukman.Helpers
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



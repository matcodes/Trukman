using System;

using Xamarin.Forms;
using Trukman.Interfaces;
using Xamarin.Forms.Maps;

namespace Trukman.Helpers
{
	public interface ILocationServicePlatformStarter
	{
		void StartService(object tag);
		void StopService();
	}

	public interface ILocationService
	{
		void StartLocationUpdates();
		//bool CheckGPS();
		//void TryTurnOnGps();
		Position GetCurrentLocation();
	}
}



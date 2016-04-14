using System;
using KAS.Trukman.Data.Interfaces;

namespace Trukman.Data.Interfaces
{
	public interface IUserLocation : IMainData
	{
		double Latitude{get;set;}
		double Longitude{get;set;}
	}
}


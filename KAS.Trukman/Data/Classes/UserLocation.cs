using System;
using KAS.Trukman.Data.Classes;
using Trukman.Interfaces;

namespace Trukman.Classes
{
	public class UserLocation : MainData, IUserLocation
	{
		public double Latitude { get; set; }
		public double Longitude{get;set;}
		//public DateTime updatedAt{get;set;}

		public UserLocation ()
		{
		}
	}
}


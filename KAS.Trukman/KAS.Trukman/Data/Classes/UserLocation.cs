using System;
using Trukman.Data.Interfaces;
using KAS.Trukman.Data.Classes;

namespace Trukman.Data.Classes
{
	public class UserLocation : MainData, IUserLocation
	{
		public UserLocation (): base()
		{
		}

		#region IUserLocation implementation

		public double Latitude {
			get { return (double)this.GetValue ("Latitude"); }
			set { this.SetValue ("Latitude", value); }
		}

		public double Longitude {
			get { return (double)this.GetValue ("Longitude"); }
			set { this.SetValue ("Longitude", value); }
		}

		#endregion

	}
}


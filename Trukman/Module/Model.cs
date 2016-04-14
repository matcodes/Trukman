using System;
using System.Collections.Generic;

namespace Trukman
{
	public class User
	{
		public string UserName{ get; set; }
		public string Email{ get; set; }
		public UserRole Role{ get; set; }
		public UserLocation location{ get; set; }
	}

	public class Job
	{
		public string Id{ get; set; }
		public string Name{ get; set; }
		public string Description { get; set; }
		public string ShipperAddress{ get; set; }
		public string ReceiverAddress{ get; set; }
		public DateTime PickupDateTime{ get; set; }
		public DateTime DeliveryDateTime{ get; set; }
		public int DriverOnTimePickup{ get; set; }
		public int DriverOnTimeDelivery{ get; set; }
		public bool Completed{ get; set; }
		public int Price{ get; set; }
		public bool DriverAccepted{ get; set; }
		public string DeclineReason{ get; set; }
		public bool Cancelled{ get; set; }
		public List<JobDestinations> destinations{ get; set; }
	}

	public class JobDestinations
	{
		public string FromAddress{ get; set; }
		public string ToAddress{ get; set; }
		public DateTime PickupDateTime{ get; set; }
		public DateTime DeliveryDateTime{ get; set; }
		public int DriverOnTimePickup{ get; set; }
		public int DriverOnTimeDelivery{ get; set; }
	}

	public class ComcheckRequest
	{
		public User Driver{ get; set; }
		public User Dispatch{ get; set; }
		public ComcheckRequestState State{ get; set; }
		public DateTime RequestDatetime{ get; set; }
		public ComcheckRequestType RequestType{ get; set; }
		public string Comcheck{ get; set;}
	}
}


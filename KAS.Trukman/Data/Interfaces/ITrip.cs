using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.Interfaces
{
    #region ITrip
    public interface ITrip
    {
        IShipper Shipper { get; set; }

        IReceiver Receiver { get; set; }

        //DateTime Time { get; set; }

        int Points { get; set; }

		DateTime PickupDatetime { get; set; }

		DateTime DeliveryDatetime { get; set; }

		int DriverOnTimePickup { get; set; }

		int DriverOnTimeDelivery { get; set; }

		bool JobCompleted { get; set; }

		bool? DriverAccepted { get; set; }

		string DeclineReason { get; set; }

		bool JobCancelled { get; set; }
    }
    #endregion
}

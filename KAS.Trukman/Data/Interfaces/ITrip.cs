using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace KAS.Trukman.Data.Interfaces
{
    #region ITrip
    public interface ITrip : IMainData
    {
        IContractor Shipper { get; set; }

        IContractor Receiver { get; set; }

        int Points { get; set; }

		DateTime PickupDatetime { get; set; }

		DateTime DeliveryDatetime { get; set; }

		bool IsPickup { get; set; }

		bool IsDelivery { get; set; }

		bool JobCompleted { get; set; }

		bool DriverAccepted { get; set; }

		string DeclineReason { get; set; }

		bool JobCancelled { get; set; }

        string DriverDisplayName { get; set; }

        Position Location { get; set; }
    }
    #endregion
}

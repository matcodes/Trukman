using KAS.Trukman.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.Classes
{
    #region Trip
    public class Trip : MainData, ITrip
    {
        public Trip() : base()
        {
        }

        #region ITrip
        public IReceiver Receiver
        {
            get { return (this.GetValue("Receiver") as IReceiver); }
            set { this.SetValue("Receiver", value); }
        }

        public IShipper Shipper
        {
            get { return (this.GetValue("Shipper") as IShipper); }
            set { this.SetValue("Shipper", value); }
        }

        /*public DateTime Time
        {
            get { return (DateTime)this.GetValue("Time", DateTime.Now); }
            set { this.SetValue("Time", value); }
        }*/

        public int Points
        {
            get { return (int)this.GetValue("Points", (int)0); }
            set { this.SetValue("Points", value); }
        }

		public DateTime PickupDatetime 
		{
			get { return (DateTime)this.GetValue("PickupDatetime", DateTime.MinValue); }
			set { this.SetValue("PickupDatetime", value); }
		}

		public DateTime DeliveryDatetime { 
			get { return (DateTime)this.GetValue("DeliveryDatetime", DateTime.MinValue); }
			set { this.SetValue("DeliveryDatetime", value); }
			}

		public int DriverOnTimePickup 
		{
			get { return (int)this.GetValue("DriverOnTimePickup", 0); }
			set { this.SetValue("DriverOnTimePickup", value); }
		}

		public int DriverOnTimeDelivery 
		{
			get { return (int)this.GetValue("DriverOnTimeDelivery", 0); }
			set { this.SetValue("DriverOnTimeDelivery", value); }
		}

		public bool JobCompleted 
		{
			get { return (bool)this.GetValue("JobCompleted", false); }
			set { this.SetValue("JobCompleted", value); }
		}

		public bool DriverAccepted 
		{
			get { return (bool)this.GetValue("DriverAccepted", false); }
			set { this.SetValue("DriverAccepted", value); }
		}

		public string DeclineReason 
		{
			get { return (string)this.GetValue("DeclineReason", null); }
			set { this.SetValue("DeclineReason", value); }
		}

		public bool JobCancelled 
		{
			get { return (bool)this.GetValue("JobCancelled", false); }
			set { this.SetValue("JobCancelled", value); }
		}
		#endregion
    }
    #endregion
}

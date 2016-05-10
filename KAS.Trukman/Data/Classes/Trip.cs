using KAS.Trukman.Data.Interfaces;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace KAS.Trukman.Data.Classes
{
    #region Trip
    public class Trip : MainData, ITrip
    {
        public Trip() : base()
        {
        }

        #region ITrip
        [Ignore]
        public IContractor Receiver
        {
            get { return (this.GetValue("Receiver") as IContractor); }
            set { this.SetValue("Receiver", value); }
        }

        [Ignore]
        public IContractor Shipper
        {
            get { return (this.GetValue("Shipper") as IContractor); }
            set { this.SetValue("Shipper", value); }
        }

        [Ignore]
        public Position Location
        {
            get { return (Position)this.GetValue("Location", new Position(0, 0)); }
            set { this.SetValue("Location", value); }
        }

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

        public bool IsPickup
        {
            get { return (bool)this.GetValue("IsPickup", false); }
            set { this.SetValue("IsPickup", value); }
        }

        public bool IsDelivery 
		{
			get { return (bool)this.GetValue("IsDelivery", false); }
			set { this.SetValue("IsDelivery", value); }
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

        public string DriverDisplayName
        {
            get { return (string)this.GetValue("DriverDisplayName"); }
            set { this.SetValue("DriverDisplayName", value); }
        }

        public bool IsDeleted
        {
            get { return (bool)this.GetValue("IsDeleted"); }
            set { this.SetValue("IsDeleted", value); }
        }
		#endregion

        public string ShipperID { get; set; }

        public string ReceiverID { get; set; }
    }
    #endregion
}

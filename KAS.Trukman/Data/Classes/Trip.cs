using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace KAS.Trukman.Data.Classes
{
    #region Trip
    public class Trip : MainData
    {
        public Trip() : base()
        {
            this.Shipper = new Contractor();
            this.Receiver = new Contractor();
        }

        #region ITrip
        [Ignore]
        public Contractor Receiver
        {
            get { return (this.GetValue("Receiver") as Contractor); }
            set { this.SetValue("Receiver", value); }
        }

        [Ignore]
        public Contractor Shipper
        {
            get { return (this.GetValue("Shipper") as Contractor); }
            set { this.SetValue("Shipper", value); }
        }

        [Ignore]
        public Position Location
        {
            get { return (Position)this.GetValue("Location", new Position(0, 0)); }
            set { this.SetValue("Location", value); }
        }

		[Ignore]
		public Company Company
		{
			get { return (this.GetValue ("Company") as Company); }
			set { this.SetValue ("Company", value); }
		}

		[Ignore]
		public User Driver
		{
			get { return (this.GetValue ("Driver") as User); }
			set { this.SetValue ("Driver", value); }
		}

		[Ignore]
		public User Broker
		{
			get { return (this.GetValue ("Brocker") as User); }
			set { this.SetValue ("Brocker", value); }
		}

		[Ignore]
		public string FromAddress
		{
			get { return (string)this.GetValue ("FromAddress"); }
			set { this.SetValue ("FromAddress", value); }
		}

		[Ignore]
		public string ToAddress
		{
			get { return (string)this.GetValue ("ToAddress"); }
			set { this.SetValue ("ToAddress", value); }
		}

		[Ignore]
		public int Weight
		{
			get { return (int)this.GetValue ("Weight", (int)0); }
			set { this.SetValue ("Weight", value); }
		}

        [Ignore]
        public string InvoiceUri
        {
            get { return (string)this.GetValue("InvoiceUri"); }
            set { this.SetValue("InvoiceUri", value); }
        }

        public string ReportUri
        {
            get { return (string)this.GetValue("ReportUri"); }
            set { this.SetValue("ReportUri", value); }
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
            get { return (bool)this.GetValue("IsDeleted", false); }
            set { this.SetValue("IsDeleted", value); }
        }

		public string JobRef
		{
			get { return (string)this.GetValue ("JobRef"); }
			set { this.SetValue ("JobRef", value); }
		}

        public decimal Price
        {
            get { return (decimal)this.GetValue("Price", (decimal)0); }
            set { this.SetValue("Price", value); }
        }
        #endregion

        public string ShipperID { get; set; }

        public string ReceiverID { get; set; }
    }
    #endregion
}

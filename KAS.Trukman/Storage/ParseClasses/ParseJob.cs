using Parse;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Storage.ParseClasses
{
    #region ParseJob
    [ParseClassName("Job")]
    public class ParseJob : ParseObject
    {
        [ParseFieldName("Location")]
        public ParseGeoPoint Location
        {
            get { return this.GetProperty<ParseGeoPoint>(); }
            set { this.SetProperty<ParseGeoPoint>(value); }
        }

		[ParseFieldName("JobRef")]
		public string JobRef
		{
			get { return this.GetProperty<string>(); }
			set { this.SetProperty<string>(value); }
		}

        [ParseFieldName("DeclineReason")]
        public string DeclineReason
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty<string>(value); }
        }

        [ParseFieldName("DeliveryDatetime")]
        public DateTime DeliveryDatetime
        {
            get { return this.GetProperty<DateTime>(); }
            set { this.SetProperty<DateTime>(value); }
        }

        [ParseFieldName("Description")]
        public string Description
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty<string>(value); }
        }

        [ParseFieldName("DriverAccepted")]
        public bool DriverAccepted
        {
            get { return this.GetProperty<bool>(false); }
            set { this.SetProperty<bool>(value); }
        }

        [ParseFieldName("DriverOnTimeDelivery")]
        public int DriverOnTimeDelivery
        {
            get { return this.GetProperty<int>((int)0); }
            set { this.SetProperty<int>(value); }
        }

        [ParseFieldName("DriverOnTimePickup")]
        public int DriverOnTimePickup
        {
            get { return this.GetProperty<int>((int)0); }
            set { this.SetProperty<int>(value); }
        }

        [ParseFieldName("FromAddress")]
        public string FromAddress
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty<string>(value); }
        }

        [ParseFieldName("JobCancelled")]
        public bool JobCancelled
        {
            get { return this.GetProperty<bool>(false); }
            set { this.SetProperty<bool>(value); }
        }

        [ParseFieldName("JobCompleted")]
        public bool JobCompleted
        {
            get { return this.GetProperty<bool>(false); }
            set { this.SetProperty<bool>(value); }
        }

        [ParseFieldName("PickupDatetime")]
        public DateTime PickupDatetime
        {
            get { return this.GetProperty<DateTime>(); }
            set { this.SetProperty<DateTime>(value); }
        }

        [ParseFieldName("Price")]
        public int Price
        {
            get { return this.GetProperty<int>((int)0); }
            set { this.SetProperty<int>(value); }
        }

        [ParseFieldName("ToAddress")]
        public string ToAddress
        {
            get { return this.GetProperty<string>(); }
            set { this.SetProperty<string>(value); }
        }

        [ParseFieldName("Weight")]
        public int Weight
        {
            get { return this.GetProperty<int>((int)0); }
            set { this.SetProperty<int>(value); }
        }

        [ParseFieldName("Shipper")]
        public ParseContractor Shipper
        {
            get { return this.GetProperty<ParseContractor>(); }
            set { this.SetProperty<ParseContractor>(value); }
        }

        [ParseFieldName("Receiver")]
        public ParseContractor Receiver
        {
            get { return this.GetProperty<ParseContractor>(); }
            set { this.SetProperty<ParseContractor>(value); }
        }

        [ParseFieldName("Driver")]
        public ParseUser Driver
        {
            get { return this.GetProperty<ParseUser>(); }
            set { this.SetProperty<ParseUser>(value); }
        }

		[ParseFieldName("Broker")]
		public ParseUser Broker
		{
			get { return this.GetProperty<ParseUser>(); }
			set { this.SetProperty<ParseUser>(value); }
		}


        [ParseFieldName("Dispatcher")]
        public ParseUser Dispatcher
        {
            get { return this.GetProperty<ParseUser>(); }
            set { this.SetProperty<ParseUser>(value); }
        }

        [ParseFieldName("Company")]
        public ParseCompany Company
        {
            get { return this.GetProperty<ParseCompany>(); }
            set { this.SetProperty<ParseCompany>(value); }
        }

        [ParseFieldName("JobDeleted")]
        public bool IsDeleted
        {
            get { return this.GetProperty<bool>(false); }
            set { this.SetProperty<bool>(value); }
        }

        [ParseFieldName("LocationHistory")]
        public ParseRelation<ParseGeoLocation> Locations
        {
            get { return this.GetRelationProperty<ParseGeoLocation>(); }
        }

        [ParseFieldName("photos")]
        public ParseRelation<ParsePhoto> Photos
        {
            get { return this.GetRelationProperty<ParsePhoto>(); }
        }

        [ParseFieldName("Advances")]
        public ParseRelation<ParseComcheck> Advances
        {
            get { return this.GetRelationProperty<ParseComcheck>(); }
        }

        [ParseFieldName("notifications")]
        public ParseRelation<ParseNotification> Notifications
        {
            get { return this.GetRelationProperty<ParseNotification>(); }
        }
    }
    #endregion
}

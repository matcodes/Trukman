using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.Classes
{
    #region Advance
    public class Advance : MainData
    {
        public int State
        {
            get { return (int)this.GetValue("State", (int)0); }
            set { this.SetValue("State", value); }
        }

        public DateTime RequestDateTime
        {
            get { return (DateTime)this.GetValue("RequestDateTime", DateTime.Now); }
            set { this.SetValue("RequestDateTime", value); }
        }

        public int RequestType
        {
            get { return (int)this.GetValue("RequestType", (int)0); }
            set { this.SetValue("RequestType", value); }
        }

        public string Comcheck
        {
            get { return (string)this.GetValue("Comcheck"); }
            set { this.SetValue("Comcheck", value); }
        }

        public User Driver
        {
            get { return (this.GetValue("Driver") as User); }
            set { this.SetValue("Driver", value); }
        }

        public Trip Trip
        {
            get { return (this.GetValue("Trip") as Trip); }
            set { this.SetValue("Trip", value); }
        }

        public Company Company
        {
            get { return (this.GetValue("Company") as Company); }
            set { this.SetValue("Company", value); }
        }

        public string JobNumber
        {
            get { return ((this.Trip != null && !String.IsNullOrEmpty(this.Trip.JobRef)) ? this.Trip.JobRef : "-----"); }
        }

        public string DriverName
        {
            get { return ((this.Driver != null) ? String.Format("{0} {1}", this.Driver.FirstName, this.Driver.LastName) : "-----"); }
        }
    }
    #endregion
}

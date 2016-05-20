using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.Classes
{
    #region Contractor
    public class Contractor : MainData
    { 
        public Contractor()
        {
            this.Address = "";
            this.Fax = "";
            this.Name = "";
            this.Phone = "";
        }

        #region IContractor
        public string Address
        {
            get { return (string)this.GetValue("AddressLineFirst"); }
            set { this.SetValue("AddressLineFirst", value); }
        }

        public string Fax
        {
            get { return (string)this.GetValue("Fax"); }
            set { this.SetValue("Fax", value); }
        }

        public string Name
        {
            get { return (string)this.GetValue("Name"); }
            set { this.SetValue("Name", value); }
        }

        public string Phone
        {
            get { return (string)this.GetValue("Phone"); }
            set { this.SetValue("Phone", value); }
        }
        #endregion
    }
    #endregion
}

using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.Classes
{
    #region BrokerUser
    public class BrokerUser : User
    {
        public string Address
        {
            get { return (string)this.GetValue("Address"); }
            set { this.SetValue("Address", value); }
        }

        public string State
        {
            get { return (string)this.GetValue("State"); }
            set { this.SetValue("State", value); }
        }

        public string ZIP
        {
            get { return (string)this.GetValue("ZIP"); }
            set { this.SetValue("ZIP", value); }
        }

        public string ContactTitle
        {
            get { return (string)this.GetValue("ContactTitle"); }
            set { this.SetValue("ContactTitle", value); }
        }

        public string ContactName
        {
            get { return (string)this.GetValue("ContactName"); }
            set { this.SetValue("ContactName", value); }
        }

        public string DocketNumber
        {
            get { return (string)this.GetValue("DocketNumber"); }
            set { this.SetValue("DocketNumber", value); }
        }
    }
    #endregion
}

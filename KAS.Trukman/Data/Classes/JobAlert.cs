using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.Classes
{
    #region JobAlert
    public class JobAlert : MainData
    {
        public int AlertType
        {
            get { return (int)this.GetValue("AlertType"); }
            set { this.SetValue("AlertType", value); }
        }

        public string AlertText
        {
            get { return (string)this.GetValue("AlertText"); }
            set { this.SetValue("AlertText", value); }
        }

        public Company Company
        {
            get { return (this.GetValue("Company") as Company); }
            set { this.SetValue("Company", value); }
        }

        public Trip Job
        {
            get { return (this.GetValue("Job") as Trip); }
            set { this.SetValue("Job", value); }
        }

        public bool IsViewed
        {
            get { return (bool)this.GetValue("IsViewed", false); }
            set { this.SetValue("IsViewed", value); }
        }
    }
    #endregion
}

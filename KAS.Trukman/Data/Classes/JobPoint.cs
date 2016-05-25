using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.Classes
{
    #region JobPoint
    public class JobPoint : MainData
    {
        public string Text
        {
            get { return (string)this.GetValue("Text"); }
            set { this.SetValue("Text", value); }
        }

        public int Value
        {
            get { return (int)this.GetValue("Value", (int)0); }
            set { this.SetValue("Value", value); }
        }

        public Trip Job
        {
            get { return (this.GetValue("Job") as Trip); }
            set { this.SetValue("Job", value); }
        }

        public User Driver
        {
            get { return (this.GetValue("Driver") as User); }
            set { this.SetValue("Driver", value); }
        }

        public Company Company
        {
            get { return (this.GetValue("Company") as Company); }
            set { this.SetValue("Company", value); }
        }
    }
    #endregion
}

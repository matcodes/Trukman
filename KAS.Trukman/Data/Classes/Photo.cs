using KAS.Trukman.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.Classes
{
    #region Photo
    public class Photo : MainData
    {
        public string PhotoID
        {
            get { return (string)this.GetValue("PhotoID"); }
            set { this.SetValue("PhotoID", value); }
        }

        public string TripID
        {
            get { return (string)this.GetValue("TripID"); }
            set { this.SetValue("TripID", value); }
        }

        public string Type
        {
            get { return (string)this.GetValue("Type"); }
            set { this.SetValue("Type", value); }
        }

        public byte[] Data
        {
            get { return (byte[])this.GetValue("Data"); }
            set { this.SetValue("Data", value); }
        }
    }
    #endregion
}

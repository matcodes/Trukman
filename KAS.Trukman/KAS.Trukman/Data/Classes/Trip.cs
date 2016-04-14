using KAS.Trukman.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.Classes
{
    #region Trip
    public class Trip : MainData, ITrip
    {
        public Trip() 
            : base()
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

        public DateTime Time
        {
            get { return (DateTime)this.GetValue("Time", DateTime.Now); }
            set { this.SetValue("Time", value); }
        }

        public int Points
        {
            get { return (int)this.GetValue("Points", (int)0); }
            set { this.SetValue("Points", value); }
        }
        #endregion
    }
    #endregion
}

using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.Interfaces
{
    #region ITrip
    public interface ITrip
    {
        IShipper Shipper { get; set; }

        IReceiver Receiver { get; set; }

        DateTime Time { get; set; }

        int Points { get; set; }
    }
    #endregion
}

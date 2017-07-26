using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API
{
    #region Driver
    public class Driver : BaseEntity
    {
        public override string ToString()
        {
            return String.Format("{0} {1} ({2})", this.FirstName, this.LastName, (this.IsActive ? "Enabled" : "Disabled"));
        }

        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("Phone")]
        public string Phone { get; set; }

        [JsonProperty("DriverLock")]
        public DriverLock DriverLock { get; set; }

        public bool IsActive
        {
            get { return (this.DriverLock == null); }
        }
    }
    #endregion
}

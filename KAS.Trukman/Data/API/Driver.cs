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
            return String.Format("{0} {1}", this.FirstName, this.LastName);
        }

        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("Phone")]
        public string Phone { get; set; }
    }
    #endregion
}

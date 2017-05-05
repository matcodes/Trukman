using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API
{
    #region Owner
    public class Owner : BaseEntity
    {
        public Owner() : base()
        {
        }

        public override string ToString()
        {
            return this.Name;
        }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("DBA")]
        public string DBA { get; set; }

        [JsonProperty("Address")]
        public string Address { get; set; }

        [JsonProperty("Phone")]
        public string Phone { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("FeetSize")]
        public int FeetSize { get; set; }
    }
    #endregion
}

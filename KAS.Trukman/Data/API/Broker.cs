using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API
{
    #region Broker
    public class Broker : BaseEntity
    {
        [JsonProperty("BrokerId")]
        public Guid BrokerId { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("Address")]
        public string Address { get; set; }

        [JsonProperty("State")]
        public string State { get; set; }

        [JsonProperty("ZIP")]
        public string ZIP { get; set; }

        [JsonProperty("Phone")]
        public string Phone { get; set; }

        [JsonProperty("ContactTitle")]
        public string ContactTitle { get; set; }

        [JsonProperty("ContactName")]
        public string ContactName { get; set; }

        [JsonProperty("DocketNumber")]
        public string DocketNumber { get; set; }
    }
    #endregion
}

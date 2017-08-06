using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Requests
{
    #region AddLocationRequest
    public class AddLocationRequest : BaseRequest
    {
        [JsonProperty("DriverId")]
        public Guid DriverId { get; set; }

        [JsonProperty("Latitude")]
        public decimal Latitude { get; set; }

        [JsonProperty("Longtitude")]
        public decimal Longtitude { get; set; }

        [JsonProperty("Speed")]
        public int Speed { get; set; }

        [JsonProperty("CreateTime")]
        public DateTime CreateTime { get; set; }
    }
    #endregion
}
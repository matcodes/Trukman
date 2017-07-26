using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Requests
{
    #region TrukmanDirectionRequest
    public class TrukmanDirectionRequest : BaseRequest
    {
        [JsonProperty("StartLatitude")]
        public double StartLatitude { get; set; }

        [JsonProperty("StartLongitude")]
        public double StartLongitude { get; set; }

        [JsonProperty("EndLatitude")]
        public double EndLatitude { get; set; }

        [JsonProperty("EndLongitude")]
        public double EndLongitude { get; set; }

        [JsonProperty("MoveType")]
        public string MoveType { get; set; }
    }
    #endregion
}
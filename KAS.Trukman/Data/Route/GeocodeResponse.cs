using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.Route
{
    #region GeocodeResponse
    public class GeocodeResponse
    {
        [JsonProperty("results")]
        public GeocodeResult[] Results { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
    #endregion
}

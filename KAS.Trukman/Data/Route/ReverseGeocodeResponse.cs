using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.Route
{
    #region ReverseGeocodeResponse
    public class ReverseGeocodeResponse
    {
        [JsonProperty("results")]
        public GeocodeResult[] Results { get; set; }
    }
    #endregion
}

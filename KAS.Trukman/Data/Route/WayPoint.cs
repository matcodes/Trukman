using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace KAS.Trukman.Data.Route
{
    #region WayPoint
    public class WayPoint
    {
        public WayPoint() 
            : base()
        {
        }

        [JsonProperty("geocoder_status")]
        public string Status { get; set; }

        [JsonProperty("place_id")]
        public string PlaceID { get; set; }

        [JsonProperty("types")]
        public string[] Types { get; set; }
    }
    #endregion
}

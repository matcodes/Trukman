using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.Route
{
    #region RouteLeg
    public class RouteLeg
    {
        public RouteLeg() 
            : base()
        {
        }

        [JsonProperty("distance")]
        public RouteValue Distance { get; set; }

        [JsonProperty("duration")]
        public RouteValue Duration { get; set; }

        [JsonProperty("end_address")]
        public string EndAddress { get; set; }

        [JsonProperty("end_location")]
        public RoutePoint EndLocation { get; set; }

        [JsonProperty("start_address")]
        public string StartAddress { get; set; }

        [JsonProperty("start_location")]
        public RoutePoint StartLocation { get; set; }

        [JsonProperty("steps")]
        public RouteStep[] Steps { get; set; }
    }
    #endregion
}

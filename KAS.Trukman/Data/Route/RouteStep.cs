using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.Route
{
    #region RouteStep
    public class RouteStep
    {
        public RouteStep() 
            : base()
        {
        }

        [JsonProperty("distance")]
        public RouteValue Distance { get; set; }

        [JsonProperty("duration")]
        public RouteValue Duration { get; set; }

        [JsonProperty("end_location")]
        public RoutePoint EndLocation { get; set; }

        [JsonProperty("html_instructions")]
        public string HtmlInstructions { get; set; }

        [JsonProperty("polyline")]
        public RoutePolyline Polyline { get; set; }

        [JsonProperty("start_location")]
        public RoutePoint StartLocation { get; set; }

        [JsonProperty("maneuver")]
        public string Maneuver { get; set; }

        [JsonProperty("travel_mode")]
        public string TravelMode { get; set; }
    }
    #endregion
}

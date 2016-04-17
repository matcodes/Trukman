using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.Route
{
    #region Route
    public class Route
    {
        public Route() 
            : base()
        {
        }

        [JsonProperty("bounds")]
        public RouteBounds Bounds { get; set; }

        [JsonProperty("copyrights")]
        public string Copyrights { get; set; }

        [JsonProperty("legs")]
        public RouteLeg[] Legs { get; set; }

        [JsonProperty("overview_polyline")]
        public RoutePolyline OverviewPolyline { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }
    }
    #endregion
}

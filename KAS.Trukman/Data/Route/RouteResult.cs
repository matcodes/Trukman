using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.Route
{
    #region RouteResult
    public class RouteResult
    {
        public RouteResult()
        {
        }

        [JsonProperty("geocoded_waypoints")]
        public WayPoint[] WayPoints { get; set; }

        [JsonProperty("routes")]
        public Route[] Routes { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
    #endregion
}

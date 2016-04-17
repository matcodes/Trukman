using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.Route
{
    #region RouteBounds
    public class RouteBounds
    {
        public RouteBounds() 
            : base()
        {
        }

        [JsonProperty("northeast")]
        public RoutePoint NorthEast { get; set; }

        [JsonProperty("southwest")]
        public RoutePoint SouthWest { get; set; }
    }
    #endregion
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.Route
{
    #region RoutePolyline
    public class RoutePolyline
    {
        [JsonProperty("points")]
        public string Points { get; set; }
    }
    #endregion
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.Route
{
    #region RouteValue
    public class RouteValue
    {
        public RouteValue() 
            : base()
        {
        }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("value")]
        public long Value { get; set; }
    }
    #endregion
}

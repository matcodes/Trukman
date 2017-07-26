using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.Route
{
    #region ViewPort
    public class ViewPort
    {
        [JsonProperty("northeast")]
        public Location NorthEast { get; set; }

        [JsonProperty("southwest")]
        public Location SouthWest { get; set; }
    }
    #endregion
}

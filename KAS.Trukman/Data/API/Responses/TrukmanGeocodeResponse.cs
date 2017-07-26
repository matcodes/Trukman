using KAS.Trukman.Data.Route;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Responses
{
    #region TrukmanGeocodeResponse
    public class TrukmanGeocodeResponse : BaseResponse
    {
        [JsonProperty("Result")]
        public GeocodeResponse Result { get; set; }
    }
    #endregion
}
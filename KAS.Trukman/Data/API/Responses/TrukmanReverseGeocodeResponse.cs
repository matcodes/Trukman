using KAS.Trukman.Data.Route;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Responses
{
    #region TrukmanReverseGeocodeResponse
    public class TrukmanReverseGeocodeResponse : BaseResponse
    {
        [JsonProperty("Result")]
        public ReverseGeocodeResponse Result { get; set; }
    }
    #endregion
}
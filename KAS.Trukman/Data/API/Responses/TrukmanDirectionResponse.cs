using KAS.Trukman.Data.Route;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Responses
{
    #region TrukmanDirectionResponse
    public class TrukmanDirectionResponse : BaseResponse
    {
        [JsonProperty("Result")]
        public RouteResult Result { get; set; }
    }
    #endregion
}
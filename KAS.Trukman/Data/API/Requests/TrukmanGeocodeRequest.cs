using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Requests
{
    #region TrukmanGeocodeRequest
    public class TrukmanGeocodeRequest : BaseRequest
    {
        [JsonProperty("Address")]
        public string Address { get; set; }
    }
    #endregion
}
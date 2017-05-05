using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Responses
{
    #region DriverResponse
    public class DriverLoginResponse : BaseResponse
    {
        [JsonProperty("Driver")]
        public Driver Driver { get; set; }

        [JsonProperty("Token")]
        public string Token { get; set; }
    }
    #endregion
}

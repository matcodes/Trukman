using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Requests
{
    #region DriverLoginRequest
    public class DriverLoginRequest : BaseRequest
    {
        public DriverLoginRequest() : base()
        {
        }

        [JsonProperty("Driver")]
        public Driver Driver { get; set; }
    }
    #endregion
}

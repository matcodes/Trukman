using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Responses
{
    #region SaveBrokerResponse
    public class SaveBrokerResponse : BaseResponse
    {
        [JsonProperty("Broker")]
        public Broker Broker { get; set; }
    }
    #endregion
}
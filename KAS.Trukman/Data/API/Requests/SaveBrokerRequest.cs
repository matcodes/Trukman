using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Requests
{
    #region SaveBrokerRequest
    public class SaveBrokerRequest : BaseRequest
    {
        [JsonProperty("Broker")]
        public Broker Broker { get; set; }
    }
    #endregion
}
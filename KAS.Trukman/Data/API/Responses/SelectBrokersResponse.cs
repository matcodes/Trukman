using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Responses
{
    #region SelectBrokersResponse
    public class SelectBrokersResponse : BaseResponse
    {
        [JsonProperty("Brokers")]
        public Broker[] Brokers { get; set; }
    }
    #endregion
}
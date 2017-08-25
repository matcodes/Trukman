using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Responses
{
    #region DispatcherLoginResponse
    public class DispatcherLoginResponse : BaseResponse
    {
        [JsonProperty("Dispatcher")]
        public Dispatcher Dispatcher { get; set; }

        [JsonProperty("Owner")]
        public Owner Owner { get; set; }

        [JsonProperty("Settings")]
        public Setting Settings { get; set; }

        [JsonProperty("Token")]
        public string Token { get; set; }
    }
    #endregion
} 
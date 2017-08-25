using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Requests
{
    #region DispatchrLoginRequest
    public class DispatcherLoginRequest : BaseRequest
    {
        [JsonProperty("Dispatcher")]
        public Dispatcher Dispatcher { get; set; }
    }
    #endregion
}
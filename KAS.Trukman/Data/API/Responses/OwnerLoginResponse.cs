using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Responses
{
    #region OwnerResponse
    public class OwnerLoginResponse : BaseResponse
    {
        [JsonProperty("Owner")]
        public Owner Owner { get; set; }

        [JsonProperty("Token")]
        public string Token { get; set; }
    }
    #endregion
}

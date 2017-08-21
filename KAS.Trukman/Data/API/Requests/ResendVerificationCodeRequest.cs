using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Requests
{
    #region ResendVerificationCodeRequest
    public class ResendVerificationCodeRequest : BaseRequest
    {
        [JsonProperty("UserId")]
        public Guid UserId { get; set; }
    }
    #endregion
}
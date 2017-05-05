using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Requests
{
    #region VerificationRequest
    public class VerificationRequest : BaseRequest
    {
        [JsonProperty("AccountId")]
        public Guid AccountId { get; set; }

        [JsonProperty("Code")]
        public string Code { get; set; }
    }
    #endregion
}

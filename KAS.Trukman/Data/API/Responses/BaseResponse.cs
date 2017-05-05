using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Responses
{
    #region BaseResponse
    public class BaseResponse
    {
        [JsonProperty("RequestId")]
        public Guid RequestId { get; set; }

        [JsonProperty("ErrorText")]
        public string ErrorText { get; set; }
    }
    #endregion
}

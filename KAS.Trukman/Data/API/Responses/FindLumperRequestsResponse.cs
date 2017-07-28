using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Responses
{
    #region FindLumperRequestsResponse
    public class FindLumperRequestsResponse : BaseResponse
    {
        [JsonProperty("LumperRequests")]
        public LumperRequest[] LumperRequests { get; set; }
    }
    #endregion
}
using KAS.Trukman.Data.API.Requests;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Responses
{
    #region GetDriverRequestsResponse
    public class GetDriverRequestsResponse : BaseResponse
    {
        [JsonProperty("DriverRequests")]
        public DriverRequest[] DriverRequests { get; set; }
    }
    #endregion
}

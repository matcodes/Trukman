using KAS.Trukman.Data.API.Requests;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Responses
{
    #region GetDispatcherRequestsResponse
    public class GetDispatcherRequestsResponse : BaseResponse
    {
        [JsonProperty("DriverRequests")]
        public DispatcherRequest[] DispatcherRequests { get; set; }
    }
    #endregion
}
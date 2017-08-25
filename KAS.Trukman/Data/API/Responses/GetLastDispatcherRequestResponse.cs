using KAS.Trukman.Data.API.Requests;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Responses
{
    #region GetLastDispatcherRequestResponse
    public class GetLastDispatcherRequestResponse : BaseResponse
    {
        [JsonProperty("DispatcherRequest")]
        public DispatcherRequest DispatcherRequest { get; set; }
    }
    #endregion
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Requests
{
    #region GetLastDispatcherRequestRequest
    public class GetLastDispatcherRequestRequest : BaseRequest
    {
        [JsonProperty("OwnerId")]
        public Guid OwnerId { get; set; }

        [JsonProperty("DispatcherId")]
        public Guid DispatcherId { get; set; }
    }
    #endregion
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Requests
{
    #region AddDispatcherRequestRequest
    public class AddDispatcherRequestRequest : BaseRequest
    {
        [JsonProperty("OwnerId")]
        public Guid OwnerId { get; set; }

        [JsonProperty("DispatcherId")]
        public Guid DispatcherId { get; set; }
    }
    #endregion
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Requests
{
    #region SelectTasksByOwnerIdRequest
    public class SelectTasksByOwnerIdRequest : BaseRequest
    {
        [JsonProperty("OwnerId")]
        public Guid OwnerId { get; set; }

        [JsonProperty("Skip")]
        public int Skip { get; set; }

        [JsonProperty("Limit")]
        public int Limit { get; set; }
    }
    #endregion
}
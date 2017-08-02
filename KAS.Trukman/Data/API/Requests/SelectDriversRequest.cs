using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Requests
{
    #region SelectDriversRequest
    public class SelectDriversRequest : BaseRequest
    {
        [JsonProperty("OwnerId")]
        public Guid OwnerId { get; set; }
    }
    #endregion
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Requests
{
    #region AddDriverRequestRequest
    public class AddDriverRequestRequest : BaseRequest
    {
        [JsonProperty("OwnerId")]
        public Guid OwnerId { get; set; }

        [JsonProperty("DriverId")]
        public Guid DriverId { get; set; }
    }
    #endregion
}

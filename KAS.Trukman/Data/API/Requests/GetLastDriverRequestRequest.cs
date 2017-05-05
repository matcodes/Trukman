using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Requests
{
    #region GetLastDriverRequestRequest
    public class GetLastDriverRequestRequest : BaseRequest
    {
        [JsonProperty("OwnerId")]
        public Guid OwnerId { get; set; }

        [JsonProperty("DriverId")]
        public Guid DriverId { get; set; }
    }
    #endregion
}

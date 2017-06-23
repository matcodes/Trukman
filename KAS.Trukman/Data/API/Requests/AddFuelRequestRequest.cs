using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Requests
{
    #region AddFuelRequestRequest
    public class AddFuelRequestRequest : BaseRequest
    {
        [JsonProperty("TaskId")]
        public Guid TaskId { get; set; }

        [JsonProperty("RequestTime")]
        public DateTime RequestTime { get; set; }
    }
    #endregion
}
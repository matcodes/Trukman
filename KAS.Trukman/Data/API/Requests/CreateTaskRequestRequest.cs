using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Requests
{
    #region TaskRequestRequest
    public class CreateTaskRequestRequest : BaseRequest
    {
        [JsonProperty("TaskId")]
        public Guid TaskId { get; set; }

        [JsonProperty("DriverId")]
        public Guid DriverId { get; set; }
    }
    #endregion
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Requests
{
    #region TaskArrivalLoadingRequest
    public class TaskArrivalLoadingRequest : BaseRequest
    {
        [JsonProperty("TaskId")]
        public Guid TaskId { get; set; }

        [JsonProperty("ArrivalTime")]
        public DateTime ArrivalTime { get; set; }
    }
    #endregion
}

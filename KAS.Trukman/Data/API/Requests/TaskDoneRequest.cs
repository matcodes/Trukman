using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Requests
{
    #region TaskDoneRequest
    public class TaskDoneRequest : BaseRequest
    {
        [JsonProperty("TaskId")]
        public Guid TaskId { get; set; }

        [JsonProperty("DoneTime")]
        public DateTime DoneTime { get; set; }
    }
    #endregion
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API
{
    #region TaskRequest
    public class TaskRequest : BaseEntity
    {
        [JsonProperty("TaskId")]
        public Guid TaskId { get; set; }

        [JsonProperty("DriverId")]
        public Guid DriverId { get; set; }

        [JsonProperty("RequestTime")]
        public DateTime RequestTime { get; set; }

        [JsonProperty("Answer")]
        public int Answer { get; set; }

        [JsonProperty("AnswerTime")]
        public DateTime? AnswerTime { get; set; }

        [JsonProperty("IsCancelled")]
        public bool IsCancelled { get; set; }

        [JsonProperty("DeclineReason")]
        public int DeclineReason { get; set; }

        [JsonProperty("DeclineText")]
        public string DeclineText { get; set; }

        [JsonProperty("Task")]
        public TrukmanTask Task { get; set; }

        [JsonProperty("Driver")]
        public Driver Driver { get; set; }
    }
    #endregion
}

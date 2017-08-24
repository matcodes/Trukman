using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API
{
    #region FuelRequest
    public class FuelRequest : BaseEntity
    {
        [JsonProperty("TaskId")]
        public Guid TaskId { get; set; }

        [JsonProperty("RequestTime")]
        public DateTime RequestTime { get; set; }

        [JsonProperty("Comcheck")]
        public string Comcheck { get; set; }

        [JsonProperty("Answer")]
        public int Answer { get; set; }

        [JsonProperty("AnswerTime")]
        public DateTime AnswerTime { get; set; }

        [JsonProperty("IsCancelled")]
        public bool IsCancelled { get; set; }

        [JsonProperty("Task")]
        public TrukmanTask Task { get; set; }
    }
    #endregion
}

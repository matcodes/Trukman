using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Requests
{
    #region DispatcherRequest
    public class DispatcherRequest
    {
        public override string ToString()
        {
            return String.Format("{0} -> {1}", this.Dispatcher, this.Owner);
        }

        [JsonProperty("Id")]
        public Guid Id { get; set; }

        [JsonProperty("OwnerId")]
        public Guid OwnerId { get; set; }

        [JsonProperty("DispatcherId")]
        public Guid DispatcherId { get; set; }

        [JsonProperty("answer")]
        public int Answer { get; set; }

        [JsonProperty("RequestTime")]
        public DateTime RequestTime { get; set; }

        [JsonProperty("Owner")]
        public Owner Owner { get; set; }

        [JsonProperty("Dispatcher")]
        public Dispatcher Dispatcher { get; set; }

        [JsonProperty("AnswerTime")]
        public DateTime? AnswerTime { get; set; }
    }
    #endregion
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Requests
{
    #region DriverRequest
    public class DriverRequest
    {
        public override string ToString()
        {
            return String.Format("{0} -> {1}", this.Driver, this.Owner);
        }

        [JsonProperty("Id")]
        public Guid Id { get; set; }

        [JsonProperty("OwnerId")]
        public Guid OwnerId { get; set; }

        [JsonProperty("DriverId")]
        public Guid DriverId { get; set; }

        [JsonProperty("answer")]
        public int Answer { get; set; }

        [JsonProperty("RequestTime")]
        public DateTime RequestTime { get; set; }

        [JsonProperty("Owner")]
        public Owner Owner { get; set; }

        [JsonProperty("Driver")]
        public Driver Driver { get; set; }

        [JsonProperty("AnswerTime")]
        public DateTime? AnswerTime { get; set; }
    }
    #endregion
}

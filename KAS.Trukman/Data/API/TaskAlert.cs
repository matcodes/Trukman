using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API
{
    public class TaskAlert: BaseEntity
    {
        [JsonProperty("TaskId")]
        public Guid TaskId { get; set; }

        [JsonProperty("Time")]
        public DateTime Time { get; set; }

        [JsonProperty("Kind")]
        public int Kind { get; set; }

        [JsonProperty("Text")]
        public string Text { get; set; }
    }
}

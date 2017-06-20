using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API
{
    #region TaskPhoto
    public class TaskPhoto : BaseEntity
    {
        [JsonProperty("TaskId")]
        public Guid TaskId { get; set; }

        [JsonProperty("Kind")]
        public int Kind { get; set; }

        [JsonProperty("PhotoTime")]
        public DateTime PhotoTime { get; set; }

        [JsonProperty("Uri")]
        public string Uri { get; set; }
    }
    #endregion
}

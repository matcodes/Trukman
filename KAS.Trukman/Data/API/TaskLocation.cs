using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API
{
    #region TaskLocation
    public class TaskLocation : BaseEntity
    {
        [JsonProperty("TaskId")]
        public Guid TaskId { get; set; }

        [JsonProperty("Latitude")]
        public decimal Latitude { get; set; }

        [JsonProperty("Longitude")]
        public decimal Longitude { get; set; }

        [JsonProperty("Speed")]
        public int Speed { get; set; }

        [JsonProperty("CreateTime")]
        public DateTime CreateTime { get; set; }
    }
    #endregion
}

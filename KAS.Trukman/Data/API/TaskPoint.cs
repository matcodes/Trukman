using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API
{
    #region TaskPoint
    public class TaskPoint : BaseEntity
    {
        [JsonProperty("TaskId")]
        public Guid TaskId { get; set; }

        [JsonProperty("Kind")]
        public int Kind { get; set; }

        [JsonProperty("Points")]
        public int Points { get; set; }
    }
    #endregion
}

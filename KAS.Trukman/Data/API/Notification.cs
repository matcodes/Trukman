using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace KAS.Trukman.Data.API
{
    #region Notification
    public class Notification
    {
        [JsonProperty("Id")]
        public Guid Id { get; set; }

        [JsonProperty("TaskId")]
        public Guid TaskId { get; set; }

        [JsonProperty("ReceiverId")]
        public Guid ReceiverId { get; set; }

        [JsonProperty("Time")]
        public DateTime Time { get; set; }

        [JsonProperty("Text")]
        public string Text { get; set; }

        [JsonProperty("IsViewed")]
        public bool IsViewed { get; set; }

        [JsonProperty("Task")]
        public virtual TrukmanTask Task { get; set; }
    }
    #endregion
}

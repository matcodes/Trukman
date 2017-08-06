using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Requests
{
    #region AddTaskAlertRequest
    public class AddTaskAlertRequest : BaseRequest
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
    #endregion
}
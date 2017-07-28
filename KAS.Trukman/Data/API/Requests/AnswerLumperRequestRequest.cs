using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Requests
{
    #region AnswerLumperRequestRequest
    public class AnswerLumperRequestRequest : BaseRequest
    {
        [JsonProperty("TaskId")]
        public Guid TaskId { get; set; }

        [JsonProperty("Answer")]
        public int Answer { get; set; }

        [JsonProperty("AnswerTime")]
        public DateTime AnswerTime { get; set; }

        [JsonProperty("Comcheck")]
        public string Comcheck { get; set; }
    }
    #endregion
}
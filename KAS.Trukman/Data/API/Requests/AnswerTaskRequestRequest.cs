using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Requests
{
    #region AnswerTaskRequestRequest
    public class AnswerTaskRequestRequest : BaseRequest
    {
        [JsonProperty("TaskRequestId")]
        public Guid TaskRequestId { get; set; }

        [JsonProperty("Answer")]
        public int Answer { get; set; }

        [JsonProperty("DeclineReason")]
        public int DeclineReason { get; set; }

        [JsonProperty("DeclineText")]
        public string DeclineText { get; set; }
    }
    #endregion
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Requests
{
    #region SelectNotificationsRequest
    public class SelectNotificationsRequest : BaseRequest
    {
        [JsonProperty("ReceiverId")]
        public Guid ReceiverId { get; set; }

        [JsonProperty("Skip")]
        public int Skip { get; set; }

        [JsonProperty("Limit")]
        public int Limit { get; set; }
    }
    #endregion
}
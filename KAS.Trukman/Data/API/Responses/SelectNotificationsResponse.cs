using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Responses
{
    #region SelectNotificationsResponse
    public class SelectNotificationsResponse : BaseResponse
    {
        [JsonProperty("Notifications")]
        public Notification[] Notifications { get; set; }
    }
    #endregion
}
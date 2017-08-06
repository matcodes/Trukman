using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Responses
{
    #region SetNotificationIsViewedResponse
    public class SetNotificationIsViewedResponse : BaseResponse
    {
        [JsonProperty("Notification")]
        public Notification Notification { get; set; }
    }
    #endregion
}
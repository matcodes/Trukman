using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Requests
{
    #region SetNotificationIsViewedRequest
    public class SetNotificationIsViewedRequest : BaseRequest
    {
        [JsonProperty("NotificationId")]
        public Guid NotificationId { get; set; }
    }
    #endregion
}
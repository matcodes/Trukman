using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Requests
{
    #region CancelFuelRequestRequest
    public class CancelFuelRequestRequest : BaseRequest
    {
        [JsonProperty("TaskId")]
        public Guid TaskId { get; set; }
    }
    #endregion
}
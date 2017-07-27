using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Requests
{
    #region CancelLumperRequestRequest
    public class CancelLumperRequestRequest : BaseRequest
    {
        [JsonProperty("TaskId")]
        public Guid TaskId { get; set; }
    }
    #endregion
}
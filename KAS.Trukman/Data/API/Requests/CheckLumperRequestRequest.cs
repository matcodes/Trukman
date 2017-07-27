using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Requests
{
    #region CheckLumperRequestRequest
    public class CheckLumperRequestRequest : BaseRequest
    {
        [JsonProperty("TaskId")]
        public Guid TaskId { get; set; }
    }
    #endregion
}
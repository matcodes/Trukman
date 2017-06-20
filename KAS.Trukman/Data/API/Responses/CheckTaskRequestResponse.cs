using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Responses
{
    #region CheckTaskRequestResponse
    public class CheckTaskRequestResponse : BaseResponse
    {
        [JsonProperty("TaskRequest")]
        public TaskRequest TaskRequest { get; set; }
    }
    #endregion
}
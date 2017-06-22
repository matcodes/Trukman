using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Responses
{
    #region AddTaskLocationResponse
    public class AddTaskLocationResponse : BaseResponse
    {
        [JsonProperty("TaskRequest")]
        public TaskRequest TaskRequest { get; set; }
    }
    #endregion
}
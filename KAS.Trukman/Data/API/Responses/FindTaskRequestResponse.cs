using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Responses
{
    #region FindTaskRequestResponse
    public class FindTaskRequestResponse : BaseResponse
    {
        [JsonProperty("TaskRequest")]
        public TaskRequest TaskRequest { get; set; }
    }
    #endregion
}

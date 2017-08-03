using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Responses
{
    #region SelectTaskByIdResponse
    public class SelectTaskByIdResponse : BaseResponse
    {
        [JsonProperty("Task")]
        public TrukmanTask Task { get; set; }
    }
    #endregion
}
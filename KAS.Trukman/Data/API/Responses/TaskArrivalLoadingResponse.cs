using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Responses
{
    #region TaskArrivalLoadingResponse
    public class TaskArrivalLoadingResponse : BaseResponse
    {
        [JsonProperty("Task")]
        public TrukmanTask Task { get; set; }
    }
    #endregion
}

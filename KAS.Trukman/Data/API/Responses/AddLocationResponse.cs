using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Responses
{
    #region AddLocationResponse
    public class AddLocationResponse : BaseResponse
    {
        [JsonProperty("Task")]
        public TrukmanTask Task { get; set; }
    }
    #endregion
} 
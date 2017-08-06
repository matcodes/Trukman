using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Responses
{
    #region SelectTasksByDriverIdResponse
    public class SelectTasksByDriverIdResponse : BaseResponse
    {
        [JsonProperty("Tasks")]
        public TrukmanTask[] Tasks { get; set; }
    }
    #endregion
}
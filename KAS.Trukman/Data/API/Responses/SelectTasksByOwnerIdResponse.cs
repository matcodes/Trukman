using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Responses
{
    #region SelectTasksByOwnerIdResponse
    public class SelectTasksByOwnerIdResponse : BaseResponse
    {
        [JsonProperty("Tasks")]
        public TrukmanTask[] Tasks { get; set; }
    }
    #endregion
}
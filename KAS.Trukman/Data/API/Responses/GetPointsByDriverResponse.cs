using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Responses
{
    #region GetPointsByDriverResponse
    public class GetPointsByDriverResponse : BaseResponse
    {
        [JsonProperty("Points")]
        public int Points { get; set; }
    }
    #endregion
}
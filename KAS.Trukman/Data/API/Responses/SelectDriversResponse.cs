using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Responses
{
    #region SelectDriversResponse
    public class SelectDriversResponse : BaseResponse
    {
        [JsonProperty("Drivers")]
        public Driver[] Drivers { get; set; }
    }
    #endregion
}
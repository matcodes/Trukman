using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Responses
{
    #region CheckLumperRequestResponse
    public class CheckLumperRequestResponse : BaseResponse
    {
        [JsonProperty("LumperRequest")]
        public LumperRequest LumperRequest { get; set; }
    }
    #endregion
}
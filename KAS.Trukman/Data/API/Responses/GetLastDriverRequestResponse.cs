using KAS.Trukman.Data.API.Requests;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Responses
{
    #region GetLastDriverRequestResponse
    public class GetLastDriverRequestResponse : BaseResponse
    {
        [JsonProperty("DriverRequest")]
        public DriverRequest DriverRequest { get; set; }
    }
    #endregion
}

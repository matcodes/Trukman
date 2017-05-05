using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Responses
{
    #region GetDriverCompanyResponse
    public class GetDriverCompanyResponse : BaseResponse
    {
        [JsonProperty("Company")]
        public Owner Company { get; set; }
    }
    #endregion
}

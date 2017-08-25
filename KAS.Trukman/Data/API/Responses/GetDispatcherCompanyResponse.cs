using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Responses
{
    #region GetDispatcherCompanyResponse
    public class GetDispatcherCompanyResponse : BaseResponse
    {
        [JsonProperty("Company")]
        public Owner Company { get; set; }
    }
    #endregion
}
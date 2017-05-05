using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Responses
{
    #region SelectCompaniesByFilterResponse
    public class SelectCompaniesByFilterResponse : BaseResponse
    {
        [JsonProperty("Companies")]
        public Owner[] Companies { get; set; }
    }
    #endregion
}

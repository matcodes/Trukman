using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Requests
{
    #region SelectCompaniesByFilterRequest
    public class SelectCompaniesByFilterRequest : BaseRequest
    {
        [JsonProperty]
        public string Filter { get; set; }
    }
    #endregion
}

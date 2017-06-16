using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Requests
{
    #region GetCompanyRequest
    public class GetCompanyRequest : BaseRequest
    {
        [JsonProperty("CompanyId")]
        public Guid CompanyId { get; set; }
    }
    #endregion
}

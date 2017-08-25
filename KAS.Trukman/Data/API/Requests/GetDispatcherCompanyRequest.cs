using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Requests
{
    #region GetDispatcherCompanyRequest
    public class GetDispatcherCompanyRequest : BaseRequest
    {
        [JsonProperty("DispatcherId")]
        public Guid DispatcherId { get; set; }
    }
    #endregion
}
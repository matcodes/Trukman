using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Requests
{
    #region GetDriverCompanyRequest
    public class GetDriverCompanyRequest : BaseRequest
    {
        [JsonProperty("DriverId")]
        public Guid DriverId { get; set; }
    }
    #endregion
}

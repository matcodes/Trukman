using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Requests
{
    #region GetPointsByDriverRequest
    public class GetPointsByDriverRequest : BaseRequest
    {
        [JsonProperty("DriverId")]
        public Guid DriverId { get; set; }
    }
    #endregion
}
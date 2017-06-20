using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Requests
{
    #region FindTaskRequestRequest
    public class FindTaskRequestRequest : BaseRequest
    {
        [JsonProperty("DriverId")]
        public Guid DriverId { get; set; }
    }
    #endregion
}

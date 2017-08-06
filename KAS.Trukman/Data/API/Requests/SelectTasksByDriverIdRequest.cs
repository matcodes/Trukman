using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Requests
{
    #region SelectTasksByDriverIdRequest
    public class SelectTasksByDriverIdRequest : BaseRequest
    {
        [JsonProperty("DriverId")]
        public Guid DriverId { get; set; }

        [JsonProperty("Skip")]
        public int Skip { get; set; }

        [JsonProperty("Limit")]
        public int Limit { get; set; }
    }
    #endregion
}
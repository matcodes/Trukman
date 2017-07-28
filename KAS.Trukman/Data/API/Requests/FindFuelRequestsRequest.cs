using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Requests
{
    #region FindFuelRequestsRequest
    public class FindFuelRequestsRequest : BaseRequest
    {
        [JsonProperty]
        public Guid OwnerId { get; set; }
    }
    #endregion
}
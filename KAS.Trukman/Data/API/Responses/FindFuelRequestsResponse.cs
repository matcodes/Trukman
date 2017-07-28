using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Responses
{
    #region FindFuelRequestsResponse
    public class FindFuelRequestsResponse : BaseResponse
    {
        [JsonProperty("FuelRequests")]
        public FuelRequest[] FuelRequests { get; set; }
    }
    #endregion
}
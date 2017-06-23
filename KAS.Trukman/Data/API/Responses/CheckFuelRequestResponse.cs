using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Responses
{
    #region CheckFuelRequestResponse
    public class CheckFuelRequestResponse : BaseResponse
    {
        [JsonProperty("FuelRequest")]
        public FuelRequest FuelRequest { get; set; }
    }
    #endregion
}
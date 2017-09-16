using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Requests
{
    #region CreateTaskRequest
    public class CreateTaskRequest : BaseRequest
    {
        [JsonProperty("Task")]
        public TrukmanTask Task { get; set; }

        [JsonProperty("DriverId")]
        public Guid? DriverId { get; set; }
    }
    #endregion
}

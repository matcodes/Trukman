using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Requests
{
    #region BaseRequest
    public class BaseRequest
    {
        public BaseRequest()
        {
            this.Id = Guid.NewGuid();
        }

        [JsonProperty("Id")]
        public Guid Id { get; set; }
    }
    #endregion
}

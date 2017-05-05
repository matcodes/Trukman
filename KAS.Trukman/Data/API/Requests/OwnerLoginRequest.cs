using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Requests
{
    #region OwnerLoginRequest
    public class OwnerLoginRequest : BaseRequest
    {
        public OwnerLoginRequest() : base()
        {
        }

        [JsonProperty("Owner")]
        public Owner Owner { get; set; }
    }
    #endregion 
}

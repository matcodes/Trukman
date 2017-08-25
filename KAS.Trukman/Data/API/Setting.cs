using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API
{
    #region Setting
    public class Setting : BaseEntity
    {
        [JsonProperty("OwnerId")]
        public Guid OwnerId { get; set; }

        [JsonProperty("AllowManageBrockers")]
        public bool AllowManageBrockers { get; set; }

        [JsonProperty("AllowManageDrivers")]
        public bool AllowManageDrivers { get; set; }

        [JsonProperty("AllowSignRate")]
        public bool AllowSignRate { get; set; }

        [JsonProperty("AllowPayFuel")]
        public bool AllowPayFuel { get; set; }

        [JsonProperty("AllowPayLumper")]
        public bool AllowPayLumper { get; set; }

        [JsonProperty("AllowManageInvoices")]
        public bool AllowManageInvoices { get; set; }

        [JsonProperty("DispatcherId")]
        public Guid DispatcherId { get; set; }
    }
    #endregion
}

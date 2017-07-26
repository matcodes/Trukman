using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API
{
    public class DriverLock : BaseEntity
    {
        [JsonProperty("DriverLockId")]
        public Guid DriverLockId { get; set; }

        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("Phone")]
        public string Phone { get; set; }
    }
}

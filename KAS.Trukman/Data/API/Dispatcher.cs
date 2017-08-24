using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API
{
    #region Dispatcher
    public class Dispatcher : BaseEntity
    {
        public override string ToString()
        {
            return String.Format("{0} {1}", this.FirstName, this.LastName);
        }

        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("Phone")]
        public string Phone { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }
    }
    #endregion
}

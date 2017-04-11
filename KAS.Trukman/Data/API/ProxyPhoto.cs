using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API
{
    public class ProxyPhoto
    {
        public string Kind { get; set; }

        [JsonProperty("jobId")]
        public string JobId { get; set; }

        [JsonProperty("file")]
        public byte[] Data { get; set; }
    }
}

using Newtonsoft.Json;

namespace KAS.Trukman.Data.API
{
    public class ProxyJob
    {
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("driverAccepted")]
        public bool DriverAccepted { get; set; }

        [JsonProperty("jobCompleted")]
        public bool JobCompleted { get; set; }

        [JsonProperty("declineReason")]
        public string DeclineReason { get; set; }
    }
}

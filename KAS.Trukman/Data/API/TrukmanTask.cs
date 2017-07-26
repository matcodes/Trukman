using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API
{
    #region TrukmanTask
    public class TrukmanTask : BaseEntity
    {
        [JsonProperty("OwnerId")]
        public Guid OwnerId { get; set; }

        [JsonProperty("DriverId")]
        public Guid? DriverId { get; set; }

        [JsonProperty("CreateTime")]
        public DateTime CreateTime { get; set; }

        [JsonProperty("StartTime")]
        public DateTime? StartTime { get; set; }

        [JsonProperty("EndTime")]
        public DateTime? EndTime { get; set; }

        [JsonProperty("LoadingName")]
        public string LoadingName { get; set; }

        [JsonProperty("LoadingAddress")]
        public string LoadingAddress { get; set; }

        [JsonProperty("LoadingPlanTime")]
        public DateTime LoadingPlanTime { get; set; }

        [JsonProperty("LoadingRealTime")]
        public DateTime? LoadingRealTime { get; set; }

        [JsonProperty("LoadingDonePlanTime")]
        public DateTime LoadingDonePlanTime { get; set; }

        [JsonProperty("LoadingDoneRealTime")]
        public DateTime? LoadingDoneRealTime { get; set; }

        [JsonProperty("UnloadingName")]
        public string UnloadingName { get; set; }

        [JsonProperty("UnloadingAddress")]
        public string UnloadingAddress { get; set; }

        [JsonProperty("UnloadingPlanTime")]
        public DateTime UnloadingPlanTime { get; set; }

        [JsonProperty("UnloadingRealTime")]
        public DateTime? UnloadingRealTime { get; set; }

        [JsonProperty("UnloadingDonePlanTime")]
        public DateTime UnloadingDonePlanTime { get; set; }

        [JsonProperty("UnloadingDoneRealTime")]
        public DateTime? UnloadingDoneRealTime { get; set; }

        [JsonProperty("PlanPoints")]
        public int PlanPoints { get; set; }

        [JsonProperty("BrokerId")]
        public Guid? BrokerId { get; set; }

        [JsonProperty("Latitude")]
        public double Latitude { get; set; }

        [JsonProperty("Longitude")]
        public double Longitude { get; set; }

        [JsonProperty("CompleteTime")]
        public DateTime? CompleteTime { get; set; }

        [JsonProperty("CancelTime")]
        public DateTime? CancelTime { get; set; }

        [JsonProperty("Owner")]
        public Owner Owner { get; set; }

        [JsonProperty("Driver")]
        public Driver Driver { get; set; }

        [JsonProperty("FuelRequests")]
        public FuelRequest[] FuelRequests { get; set; }

        [JsonProperty("LumperRequest")]
        public LumperRequest[] LumperRequest { get; set; }

        [JsonProperty("TaskPhotos")]
        public TaskPhoto[] TaskPhotos { get; set; }

        [JsonProperty("TaskPoints")]
        public TaskPoint[] TaskPoints { get; set; }

        [JsonProperty("Broker")]
        public Broker Broker { get; set; }
    }
    #endregion
}

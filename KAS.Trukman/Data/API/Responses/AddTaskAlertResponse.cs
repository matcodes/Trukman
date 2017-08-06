﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API.Responses
{
    #region AddTaskAlertResponse
    public class AddTaskAlertResponse : BaseResponse
    {
        [JsonProperty("Task")]
        public TrukmanTask Task { get; set; }
    }
    #endregion
}
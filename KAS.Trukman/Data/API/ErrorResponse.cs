using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Data.API
{
    #region ErrorResponse
    public class ErrorResponse
    {
        public string GetDisplayText()
        {
            var lines = this.Stack.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var result = (lines.Length > 0 ? lines[0] : "");
            result = result.Replace("Error: ", "");
            return result;
        }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("stack")]
        public string Stack { get; set; }
    }
    #endregion
}


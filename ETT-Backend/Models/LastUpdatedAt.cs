using System;
using Newtonsoft.Json;

namespace ETT_Backend.Models 
{
    public class LastUpdatedAt
    {
        [JsonProperty("timeLastUpdated")]
        public string TimeLastUpdated;

        public LastUpdatedAt(DateTime timestamp)
        {
            TimeLastUpdated = timestamp.ToString("MM/dd/yyyy HH:mm:ss");
        }
    }
}
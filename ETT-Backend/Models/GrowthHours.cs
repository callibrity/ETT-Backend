using System;
using Newtonsoft.Json;

namespace ETT_Backend.Models 
{
    public class GrowthHours 
    {
        [JsonProperty("hoursUsed")]
        public double HoursUsed;//CurrentTrainingHours

        [JsonProperty("hoursRemaining")]
        public double HoursRemaining;//TargetTrainingHours - CurrentTrainingHours

        [JsonProperty("totalGrowth")]
        public double TotalGrowth; //TargetTrainingHours

        public GrowthHours(double currentHoursUsed, double hoursRemaining, double total)
        {
            HoursUsed = currentHoursUsed;
            HoursRemaining = hoursRemaining;
            TotalGrowth = total;
        }
    }
}
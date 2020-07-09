using System;
using Newtonsoft.Json;

namespace ETT_Backend.Models 
{
    public class BillableHours 
    {
        [JsonProperty("currentHours")]
        public double CurrentHours;//CurrentBillableHours

        [JsonProperty("currentTarget")]
        public double CurrentTarget; // nominal for now, zero
        
        [JsonProperty("totalTarget")]
        public double TotalTarget; //Employee.TargetBillableHours

        public BillableHours(double currentHours, double currentTarget, double total)
        {
            CurrentHours = currentHours;
            CurrentTarget = currentTarget;
            TotalTarget = total;
        }
    }
}
using System;
using Newtonsoft.Json;

namespace ETT_Backend.Models
{
    public class EmployeeResponse
    {
        [JsonProperty("employeeId")]
        public string EmployeeId;

        [JsonProperty("billable")]
        public BillableHours Billable;

        [JsonProperty("growth")]
        public GrowthHours Growth;

        [JsonProperty("updatedAt")]
        public string UpdatedAt;

        public EmployeeResponse(Employee emp)
        {
            EmployeeId = emp.EmployeeNumber;
            Billable = new BillableHours(emp.CurrentBillableHours, emp.BillableTargetToDate, emp.YearlyBillableTargetHours);
            Growth = new GrowthHours(emp.CurrentTrainingHours, emp.TargetTrainingHours - emp.CurrentTrainingHours, emp.TargetTrainingHours);
            UpdatedAt = emp.UpdatedAt.ToString("MM/dd/yyyy HH:mm:ss");
        }

        public EmployeeResponse()
        {
        }
    }
}
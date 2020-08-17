using System;
using Newtonsoft.Json;

namespace ETT_Backend.Models
{
    public class EmployeeResponse
    {
        [JsonProperty("employeeName")]
        public string EmployeeName;

        [JsonProperty("employeeId")]
        public string EmployeeId;

        [JsonProperty("billable")]
        public BillableHours Billable;

        [JsonProperty("growth")]
        public GrowthHours Growth;

        [JsonProperty("updatedAt")]
        public string UpdatedAt;

        public EmployeeResponse(EmployeeMetrics emp)
        {
            EmployeeName = $"{emp.FirstName} {emp.LastName}";
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
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

        [JsonProperty("dbUpdate")]
        public LastUpdatedAt DBUpdate;

        public EmployeeResponse(Employee emp)
        {
            EmployeeId = emp.EmployeeNumber;
            Billable = new BillableHours(emp.CurrentBillableHours, emp.BillableTargetToDate, emp.YearlyBillableTargetHours);
            Growth = new GrowthHours(emp.CurrentTrainingHours, emp.TargetTrainingHours - emp.CurrentTrainingHours, emp.TargetTrainingHours);
            DBUpdate = new LastUpdatedAt(emp.UpdatedAt);
        }

        public EmployeeResponse()
        {
        }
    }
}
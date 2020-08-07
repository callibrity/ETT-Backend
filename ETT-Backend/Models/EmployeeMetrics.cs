using System;

namespace ETT_Backend.Models
{
    public class EmployeeMetrics
    {

        public string EmployeeNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double YearlyBillableTargetHours { get; set; }
        public double BillableTargetToDate { get; set; }
        public double CurrentBillableHours { get; set; }
        public double TargetTrainingHours { get; set; }
        public double CurrentTrainingHours { get; set; }
        public DateTime UpdatedAt { get; set; }

        public EmployeeMetrics(string employeeNumber, string firstName, string lastName, double yearlyBillableTargetHours, double billableTargetToDate, double currentBillableHours,
                               double targetTrainingHours, double currentTrainingHours, DateTime updatedAt )
        {
            this.EmployeeNumber = employeeNumber;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.YearlyBillableTargetHours = yearlyBillableTargetHours;
            this.BillableTargetToDate = billableTargetToDate;
            this.CurrentBillableHours = currentBillableHours;
            this.TargetTrainingHours = targetTrainingHours;
            this.CurrentTrainingHours = currentTrainingHours;
            this.UpdatedAt = updatedAt;

        }
    }
}

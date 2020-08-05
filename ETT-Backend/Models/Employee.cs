using System;

namespace ETT_Backend.Models
{
    public class Employee
    {

        public string EmployeeNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double YearlyBillableTargetHours { get; set; }
        public double BillableTargetToDate { get; set; }
        public double CurrentBillableHours { get; set; }
        public double TargetTrainingHours { get; set; }
        public double CurrentTrainingHours { get; set; }
        public double TotalYearlyPTO { get; set; }
        public double OverflowPTO { get; set; }
        public double UsedPTO { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Employee(string employeeNumber, string firstName, string lastName, double yearlyBillableTargetHours, double billableTargetToDate, double currentBillableHours,
                        double targetTrainingHours, double currentTrainingHours, double totalYearlyPTO, double overflowPTO, double usedPTO, DateTime updatedAt )
        {
            this.EmployeeNumber = employeeNumber;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.YearlyBillableTargetHours = yearlyBillableTargetHours;
            this.BillableTargetToDate = billableTargetToDate;
            this.CurrentBillableHours = currentBillableHours;
            this.TargetTrainingHours = targetTrainingHours;
            this.CurrentTrainingHours = currentTrainingHours;
            this.TotalYearlyPTO = totalYearlyPTO;
            this.OverflowPTO = overflowPTO;
            this.UsedPTO = usedPTO;
            this.UpdatedAt = updatedAt;

        }
    }
}

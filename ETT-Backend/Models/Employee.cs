using System;

namespace ETT_Backend.Models
{
    public class Employee
    {

        public string EmployeeNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double YearlyBillableTargetHours { get; set; }
        public double CurrentBillableHours { get; set; }
        public double TargetTrainingHours { get; set; }
        public double CurrentTrainingHours { get; set; }
        public double TotalYearlyPTO { get; set; }
        public double OverflowPTO { get; set; }
        public double UsedPTO { get; set; }
        public double BillableTargetToDate { get; set; }

        public Employee(string employeeNumber, string firstName, string lastName, double targetBillableHours, double currentBillableHours,
                        double targetTrainingHours, double currentTrainingHours, double totalYearlyPTO, double overflowPTO, double usedPTO, double billableTargetToDate)
        {
            this.EmployeeNumber = employeeNumber;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.YearlyBillableTargetHours = targetBillableHours;
            this.CurrentBillableHours = currentBillableHours;
            this.TargetTrainingHours = targetTrainingHours;
            this.CurrentTrainingHours = currentTrainingHours;
            this.TotalYearlyPTO = totalYearlyPTO;
            this.OverflowPTO = overflowPTO;
            this.UsedPTO = usedPTO;
            this.BillableTargetToDate = billableTargetToDate;

        }
    }
}

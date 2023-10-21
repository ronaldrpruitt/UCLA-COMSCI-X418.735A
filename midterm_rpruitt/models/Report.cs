using System;
using System.Collections.Generic;

namespace midterm_rpruitt.models
{
    public class Report
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Enums.JobTitle Title { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime HireDate { get; set; }
        public Enums.AllowanceType AllowanceId { get; set; }
        public decimal AllowanceAmount { get; set; }
        public decimal MonthlySalary { get; set; }
        public decimal BonusRate { get; set; }
        public decimal Sales { get; set; }
        public decimal BonusAmount { get => CalculateBonusAmount(); }

        public decimal TotalMonthlyCompensation
        {
            get
            {
                return MonthlySalary + AllowanceAmount + BonusAmount;
            }
        }

        private Dictionary<int, string> JobTitles = new Dictionary<int, string>()
        {
            {1, "President"},
            {2, "Sales Manager"},
            {3, "Sales Associate"},
            {4, "Programmer"},
            {5, "Programmer Associate"}
        };

        public override string ToString()
        {
            return $"{EmployeeId,-5} {LastName,-15} {FirstName,-15} {JobTitles[(int)Title],-23} {Birthday.ToShortDateString(),-19}" +
                $"{HireDate.ToShortDateString(),-19} {MonthlySalary.ToString("C"),-21} {Sales.ToString("C"),-18} " +
                $"{BonusRate,-18} {AllowanceAmount.ToString("C"),-18} {TotalMonthlyCompensation.ToString("C"),-18} {BonusAmount.ToString("C"),-21}";
        }

        private decimal CalculateBonusAmount()
        {
            if (Title.Equals(Enums.JobTitle.SalesManager))
            {
                decimal totalSales = GreatRepository.GetAllSales(new DateTime(DateTime.Now.Year, 5, 1), DateTime.Now);
                return totalSales * (BonusRate / 100);
            }
            else if (Title.Equals(Enums.JobTitle.SalesAssociate))
            {
                return (Sales * (BonusRate / 100));
            }
            else
                return (MonthlySalary * (BonusRate / 100));
        }
    }
}
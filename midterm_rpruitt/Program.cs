using midterm_rpruitt.models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace midterm_rpruitt
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Get Report Data
            var report = GreatRepository.GetReportData();

            Console.WriteLine("Unsorted Report");
            PrintReportHeader();
            PrintReport(report);
            PrintGrandTotals(report);

            Console.WriteLine("Sorted Report By Title and Total Compensation");
            PrintReportHeader();
            report.Sort(new SortReportTitleCompensation());
            PrintReport(report);
            PrintGrandTotals(report);

            Console.WriteLine("Sorted Report By Title and Salary");
            PrintReportHeader();
            report.Sort(new SortReportTitleSalary());
            PrintReport(report);
            PrintGrandTotals(report);

            Console.WriteLine("Press Any Key To Exit");
            Console.ReadKey();
        }

        public static void PrintReport(IEnumerable reportData)
        {
            foreach (var item in reportData)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine("\n");
        }

        public static void PrintReportHeader()
        {
            Console.WriteLine($"\n{"ID",-5} {"Last Name",-15} {"First Name",-15} {"Job Title",-23} {"DOB",-18} {"Hire Date",-19} {"Monthly Salary",-21} {"Sales Amount",-18} {"Bonus Rate (%)",-18} {"Car Allowance",-18} {"Total Compensation",-18} {"Calculated Bonus Amount",-20}\n" +
                              $"{"==",-5} {"=========",-15} {"=========",-15} {"=========",-23} {"====",-18} {"=========",-19} {"==============",-21} {"============",-18} {"==============",-18} {"=============",-18} {"==================",-18} {"==================",-21}");
        }

        public static void PrintGrandTotals(IEnumerable<Report> report)
        {
            Console.WriteLine($"Grand Totals: \n    Monthly Salary: {report.Sum(a => a.MonthlySalary).ToString("C")}\n    Sales Amount: {report.Sum(a => a.Sales).ToString("C")}" +
                $"\n    Monthly Compensation: {report.Sum(a => a.TotalMonthlyCompensation).ToString("C")}\n    Car Allowance: {report.Sum(a => a.AllowanceAmount).ToString("C")}\n\n");
        }
    }
}
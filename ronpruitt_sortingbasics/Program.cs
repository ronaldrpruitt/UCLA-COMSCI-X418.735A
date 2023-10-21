using System;
using System.Collections;
using System.Collections.Generic;

namespace ronpruitt_sortingbasics
{
    internal class Program
    {
        public static void PrintReport(IEnumerable studata)
        {
            foreach (var student in studata)
            {
                Console.WriteLine(student.ToString());
            }
            Console.WriteLine("\n");
        }

        public static string PrintReportHeader()
        {
            return $"\n{"ID",-10} {"LastName",-15} {"FirstName",-15} {"CourseId",-18} {"CourseGrade",-18}\n" +
                $"{"==",-10} {"=========",-15} {"========",-15} {"========",-18} {"===========",-18}";
        }

        private static void Main(string[] args)
        {
            //Generate Test Data
            List<Student> studentData = Student.getTestStudents();

            //Not Sorted
            Console.WriteLine("Sort Type: Not Sorted");
            Console.WriteLine(PrintReportHeader());
            PrintReport(studentData);

            //Sorted last name -> first name
            Console.WriteLine("Sort Type: Sorted by last name and then first name");
            Console.WriteLine(PrintReportHeader());
            studentData.Sort();
            PrintReport(studentData);

            //Sorted course grade -> last name -> first name
            Console.WriteLine("Sort Type: Sorted by course grade, last name and then first name");
            Console.WriteLine(PrintReportHeader());
            studentData.Sort(new StuSortCourseGradeLastFirst());
            PrintReport(studentData);

            //Sorted last name -> first name -> course id
            Console.WriteLine("Sort Type: Sorted by last name, first name and then course id");
            Console.WriteLine(PrintReportHeader());
            studentData.Sort(new StuSortLastFirstCourseId());
            PrintReport(studentData);

            //Sorted last name -> first name -> course id > course grade
            studentData = Student.getTestStudents();
            Console.WriteLine("Sort Type: Sorted by last name, first name, course id and then course grade");
            Console.WriteLine(PrintReportHeader());
            studentData.Sort(new StuSortLastFirstCourseIdGrade());
            PrintReport(studentData);

            Console.WriteLine("\nPress <ENTER> to quit...");
            Console.ReadKey();
        }
    }
}
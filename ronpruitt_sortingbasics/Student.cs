using System;
using System.Collections.Generic;

namespace ronpruitt_sortingbasics
{
    public class Student : IComparable<Student>
    {
        private double courseGrade;
        private string courseID;
        private string firstName;
        private string lastName;
        private long studentID;

        public Student(long studentID, string lastName, string firstName, string courseID, double courseGrade)
        {
            this.studentID = studentID;
            this.lastName = lastName;
            this.firstName = firstName;
            this.courseID = courseID;
            this.courseGrade = courseGrade;
        }

        public double CourseGrade { get => courseGrade; set => courseGrade = value; }
        public string CourseID { get => courseID; set => courseID = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public long StudentID { get => studentID; set => studentID = value; }

        public static List<Student> getTestStudents()
        {
            List<Student> students = new List<Student>();
            students.Add(new Student(1, "Jones", "Joan", "art0024", 3.0));
            students.Add(new Student(2, "Einstein", "Jose", "math0001", 3.3));
            students.Add(new Student(5, "Gonzales", "Miranda", "cs0024", 2.7));
            students.Add(new Student(4, "Lee", "Kim", "bs0024", 2.7));
            students.Add(new Student(3, "Jaspers", "Rachel", "cs0001", 2.7));
            students.Add(new Student(6, "gates", "Bill", "cs0001", 4.0));
            students.Add(new Student(6, "Gates", "Bill", "art0024", 3.0));
            students.Add(new Student(6, "Gates", "bill", "art0024", 1.0));
            students.Add(new Student(7, "Allison", "George", "math0023", 2.7));
            students.Add(new Student(7, "Allison", "Alice", "cs0001", 2.7));
            students.Add(new Student(10, "Allison", "Alice", "cs0001", 3.7));
            students.Add(new Student(8, "Sills", "Carol", "cs0001", 1.7));
            students.Add(new Student(8, "Sills", "Albert", "cs0001", 2.7));
            students.Add(new Student(9, "Starr", "Bert", "chem0020", 3.7));
            students.Add(new Student(11, "Allison", "Alice", "cs0001", 3.5));

            return students;
        }

        public int CompareTo(Student other)
        {
            if (other == null)
            {
                return 1;
            }

            int compareValue = lastName.ToUpper().CompareTo(other.lastName.ToUpper());

            if (compareValue == 0)
            {
                return firstName.ToUpper().CompareTo(other.firstName.ToUpper());
            }

            return compareValue;
        }

        public override string ToString()
        {
            return $"{studentID,-10} {lastName,-15} {firstName,-15} {courseID,-18} {courseGrade,-18}";
        }
    }
}
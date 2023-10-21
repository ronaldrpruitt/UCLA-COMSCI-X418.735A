using System.Collections.Generic;

namespace ronpruitt_sortingbasics
{
    public class StuSortCourseGradeLastFirst : IComparer<Student>
    {
        public int Compare(Student x, Student y)
        {
            if (x == null || y == null)
            {
                return 1;
            }

            int compareValue = x.CourseGrade.CompareTo(y.CourseGrade);
            if (compareValue == 0)
            {
                compareValue = x.LastName.ToUpper().CompareTo(y.LastName.ToUpper());
                if (compareValue == 0)
                {
                    return x.FirstName.ToUpper().CompareTo(y.FirstName.ToUpper());
                }
            }
            return compareValue;
        }
    }
}
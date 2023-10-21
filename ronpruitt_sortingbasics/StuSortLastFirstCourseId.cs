using System.Collections.Generic;

namespace ronpruitt_sortingbasics
{
    public class StuSortLastFirstCourseId : IComparer<Student>
    {
        public int Compare(Student x, Student y)
        {
            if (x == null || y == null)
            {
                return 1;
            }

            int compareValue = x.LastName.ToUpper().CompareTo(y.LastName.ToUpper());
            if (compareValue == 0)
            {
                compareValue = x.FirstName.ToUpper().CompareTo(y.FirstName.ToUpper());
                if (compareValue == 0)
                {
                    return x.CourseID.ToUpper().CompareTo(y.CourseID.ToUpper());
                }
            }
            return compareValue;
        }
    }
}
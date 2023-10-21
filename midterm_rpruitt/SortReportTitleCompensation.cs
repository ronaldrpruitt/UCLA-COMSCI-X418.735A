using midterm_rpruitt.models;
using System.Collections.Generic;

namespace midterm_rpruitt
{
    public class SortReportTitleCompensation : IComparer<Report>
    {
        public int Compare(Report x, Report y)
        {
            if (x == null || y == null)
            {
                return 1;
            }

            int compareValue = y.Title.CompareTo(x.Title);
            if (compareValue == 0)
                return x.TotalMonthlyCompensation.CompareTo(y.TotalMonthlyCompensation);

            return compareValue;
        }
    }
}
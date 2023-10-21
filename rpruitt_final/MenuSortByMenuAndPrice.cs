using System.Collections.Generic;

namespace rpruitt_final
{
    public class MenuSortByMenuAndPrice : IComparer<MenuItem>
    {
        public int Compare(MenuItem x, MenuItem y)
        {
            if (x == null || y == null)
            {
                return 1;
            }

            int compareValue = x.MenuId.CompareTo(y.MenuId);
            if (compareValue == 0)
            {
                return compareValue = x.Price.CompareTo(y.Price);
            }
            return compareValue;
        }
    }
}
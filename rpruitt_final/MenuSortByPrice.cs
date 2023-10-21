using System.Collections.Generic;

namespace rpruitt_final
{
    public class MenuSortByPrice : IComparer<MenuItem>
    {
        public int Compare(MenuItem x, MenuItem y)
        {
            if (x == null || y == null)
            {
                return 1;
            }

            return x.Price.CompareTo(y.Price);
        }
    }
}

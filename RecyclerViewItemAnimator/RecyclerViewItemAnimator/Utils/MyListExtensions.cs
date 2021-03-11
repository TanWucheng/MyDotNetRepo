using System.Collections.Generic;

namespace RecyclerViewItemAnimator.Utils
{
    public static class MyListExtensions
    {
        public static bool IsEmpty<T>(this List<T> list)
        {
            return list.Count <= 0;
        }
    }
}
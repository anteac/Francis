using System;
using System.Collections.Generic;
using System.Linq;

namespace Francis.Toolbox.Extensions
{
    public static class ListExtensions
    {
        public static IEnumerable<List<T>> ToSublists<T>(this IEnumerable<T> enumerable, int size)
        {
            var list = enumerable.ToList();
            for (int i = 0; i < list.Count; i += size)
            {
                yield return list.GetRange(i, Math.Min(size, list.Count - i));
            }
        }
    }
}

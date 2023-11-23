using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Utils
{
    public static class IEnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> act)
        {
            if (collection == null)
                return;

            foreach (var item in collection)
                act(item);
        }
    }
}

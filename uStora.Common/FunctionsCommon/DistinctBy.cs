using System;
using System.Collections.Generic;

namespace uStora.Common.FunctionsCommon
{
    public static class DistinctBy
    {
        public static IEnumerable<TSource> Distinct<TSource, TKey>
    (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}
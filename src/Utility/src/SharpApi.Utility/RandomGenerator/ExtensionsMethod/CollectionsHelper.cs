using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpApi.Utility
{
    public static class CollectionsHelper
    {
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> list)
        {
            if (list == null) return new List<T>();

            var shuffledList = list
                .Select(x => new {Number = RandomGenerator.GetInt(), Item = x})
                .OrderBy(x => x.Number).Select(x => x.Item);
            return shuffledList.ToList();
        }
    }
}

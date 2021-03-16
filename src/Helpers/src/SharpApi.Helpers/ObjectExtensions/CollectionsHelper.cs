using System;
using System.Collections.Generic;
using System.Linq;
using SharpApi.Helpers.ValueTypeExtensions;

namespace SharpApi.Helpers.ObjectExtensions
{
    public static class CollectionsHelper
    {
        public static bool IsCountBetween<T>(this IEnumerable<T> list,int minValue,int maxValue)
        {
            return list.Count().IsWithin(minValue, maxValue);
        }
    }
}

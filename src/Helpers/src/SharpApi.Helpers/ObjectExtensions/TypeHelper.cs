using System;
using System.Collections.Generic;

namespace SharpApi.Helpers.ObjectExtensions
{
    public static class TypeHelper
    {
        public static bool IsNumericType(this Type type)
        {
            return NumericTypes.Contains(type) ||
                   NumericTypes.Contains(Nullable.GetUnderlyingType(type));
        }

        private static readonly HashSet<Type> NumericTypes = new HashSet<Type>
        {
            typeof(short),
            typeof(int),
            typeof(long),
            typeof(double),
            typeof(decimal),
            typeof(float),
            typeof(ulong),
            typeof(ushort),
            typeof(uint),
        };
    }
}

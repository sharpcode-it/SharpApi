using System;
using System.Collections.Generic;
using System.Text;

namespace SharpApi.Helpers.ValueTypeExtensions
{
    public static class NumericHelper
    {
        public static bool IsWithin(this int value, int minimum, int maximum)
        {
            return value >= minimum && value <= maximum;
        }

        public static bool IsWithin(this short value, short minimum, short maximum)
        {
            return value >= minimum && value <= maximum;
        }

        public static bool IsWithin(this long value, long minimum, long maximum)
        {
            return value >= minimum && value <= maximum;
        }
    }
}

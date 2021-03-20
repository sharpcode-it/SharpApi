using System;

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

        public static bool IsWithin(this double value, double minimum, double maximum)
        {
            return value >= minimum && value <= maximum;
        }

        public static bool IsOdd(this int value)
        {
            return (value % 2) != 0;
        }

        public static bool IsEven(this int value)
        {
            return !value.IsOdd();
        }

        public static bool IsPrime(this int value)
        {
            var limit = Math.Ceiling(Math.Sqrt(value));
            for (var i = 2; i <= limit; i++)
            {
                if (value % i == 0)
                    return false;
            }
            return true;
        }
    }
}

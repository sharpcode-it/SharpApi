using System;
using System.Globalization;

namespace SharpApi.Helpers.ValueTypeExtensions
{
    public static partial class StringHelpers
    {
        /// <summary>
        /// Returns an int from an input string.
        ///  </summary>
        /// <returns></returns>
        public static int ToInt32(this string value, CultureInfo? culture = null)
        {
            var isValidInt = int.TryParse(value, NumberStyles.AllowThousands |
                                                 NumberStyles.AllowParentheses |
                                                 NumberStyles.AllowCurrencySymbol |
                                                 NumberStyles.AllowLeadingSign, culture ?? CultureInfo.CurrentCulture,
                out var result);

            return isValidInt ? result : 0;
        }

        /// <summary>
        /// Returns a long from an input string.
        ///  </summary>
        /// <returns></returns>
        public static long ToLong(this string value, CultureInfo? culture = null)
        {
            var isValidLong = long.TryParse(value, NumberStyles.AllowThousands |
                                                   NumberStyles.AllowParentheses |
                                                   NumberStyles.AllowCurrencySymbol |
                                                   NumberStyles.AllowLeadingSign, culture ?? CultureInfo.CurrentCulture,
                out var result);

            return isValidLong ? result : 0;
        }

        /// <summary>
        /// Returns a Guid from  an input string.
        ///  </summary>
        /// <returns></returns>
        public static Guid ToGuid(this string value)
        {
            var isValidLong = Guid.TryParse(value, out var result);

            return isValidLong ? result : default;
        }
    }
}

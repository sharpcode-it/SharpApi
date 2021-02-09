using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using SharpApi.Helpers.ValueTypeExtensions;

namespace SharpApi.Utility
{
    public static class RandomGenerator
    {
        private static readonly RNGCryptoServiceProvider Generator = new RNGCryptoServiceProvider();
        const string charRange = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

        public static int GetInt()
        {
            var data = new byte[4];
            Generator.GetBytes(data);

            return Math.Abs(BitConverter.ToInt32(data, startIndex: 0));
        }

        public static int GetInt(int min, int max)
        {
            if (min > max) throw new ArgumentOutOfRangeException(nameof(min));
            if (min == max) return min;

            var data = new byte[4];
            Generator.GetBytes(data);

            var generatedValue = Math.Abs(BitConverter.ToInt32(data, startIndex: 0));

            var diff = max - min;
            var mod = generatedValue % diff;
            var normalizedNumber = min + mod;

            return normalizedNumber;
        }

        public static string GetString(int length,string extendRange="")
        {
            var charsArray = charRange.ToList();

            if (!extendRange.IsNullOrEmpty())
            {
                charsArray.AddRange(extendRange.ToList());
                charsArray = charsArray.Distinct().ToList();
            }
            var res = new StringBuilder();
            while (length-- > 0)
            {
                res.Append(charsArray[GetInt(0, charsArray.Count)]);
            }

            return res.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using SharpApi.Helpers.ValueTypeExtensions;

namespace SharpApi.Utility
{
    public static class RandomGenerator
    {
        private static readonly RNGCryptoServiceProvider Generator = new RNGCryptoServiceProvider();
        private const string CharRange = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

        public static int GetInt()
        {
            var data = new byte[4];
            Generator.GetBytes(data);

            return BitConverter.ToInt32(data, startIndex: 0);
        }

        public static bool GetBoolean()
        {
            var myInt = GetInt();

            return myInt <= 0;
        }

        public static short GetInt16()
        {
            var data = new byte[4];
            Generator.GetBytes(data);

            return BitConverter.ToInt16(data, startIndex: 0);
        }

        public static long GetInt64()
        {
            var data = new byte[8];
            Generator.GetBytes(data);

            return BitConverter.ToInt64(data, startIndex: 0);
        }

        public static ushort GetUInt16()
        {
            var data = new byte[2];
            Generator.GetBytes(data);

            return BitConverter.ToUInt16(data, startIndex: 0);
        }

        public static uint GetUInt32()
        {
            var data = new byte[4];
            Generator.GetBytes(data);

            return BitConverter.ToUInt32(data, startIndex: 0);
        }

        public static ulong GetUInt64()
        {
            var data = new byte[8];
            Generator.GetBytes(data);

            return BitConverter.ToUInt64(data, startIndex: 0);
        }

        public static int GetInt(int min, int max)
        {
            if (min > max) throw new ArgumentOutOfRangeException(nameof(min));
            if (min == max) return min;

            var randomNumber = new byte[4];

            Generator.GetBytes(randomNumber);
            var baseNum = (int)BitConverter.ToUInt32(randomNumber, 0);

            return new Random(baseNum).Next(min,max+1);
        }

        public static char GetChar()
        {
            return GetString(1,1).ToCharArray()[0];
        }

        private static string GetRandomString(int minLenght,int maxLength, IEnumerable<char> characterSet)
        {
            if (maxLength < 0)
                throw new ArgumentException("length must not be negative", "length");
            if (maxLength > int.MaxValue / 8) // 250 million chars ought to be enough for anybody
                throw new ArgumentException("length is too big", "length");
            if (characterSet == null)
                throw new ArgumentNullException(nameof(characterSet));
            var characterArray = characterSet.Distinct().ToArray();
            if (characterArray.Length == 0)
                throw new ArgumentException("characterSet must not be empty", nameof(characterSet));

            var length = GetInt(minLenght, maxLength);

            var bytes = new byte[length * 8];
            var result = new char[length];
            using (var cryptoProvider = new RNGCryptoServiceProvider())
            {
                cryptoProvider.GetBytes(bytes);
            }
            for (var i = 0; i < length; i++)
            {
                var value = BitConverter.ToUInt64(bytes, i * 8);
                result[i] = characterArray[value % (uint)characterArray.Length];
            }
            return new string(result);
        }
        public static string GetString(int minLenght,int length,string extendRange="")
        {
            var charsArray = CharRange.ToList();

            return GetRandomString(minLenght,length, charsArray);
        }

        public static DateTime GetDateTime()
        {
            //var now = DateTime.Today.Second;

            var newTicks = GetInt();

            return DateTime.Today.AddSeconds(newTicks);
        }

    }
}

using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using SharpApi.Utility.Crypting.Model;

namespace SharpApi.Utility.Crypting
{
    public static class HashingProvider
    {
        /// <summary>
        /// Computes the hash of the string using a specified hash algorithm
        /// </summary>
        /// <param name="input">The string to hash</param>
        /// <param name="hashType">The hash algorithm to use</param>
        /// <param name="saltValue"></param>
        /// <returns>The resulting hash or an empty string on error</returns>
        public static string ComputeHash(this string input, HashType hashType,string saltValue = null)
        {
            try
            {
                var hash = GetHash(input, hashType,saltValue);
                var ret = new StringBuilder();

                foreach (var t in hash)
                    ret.Append(t.ToString("x2"));

                return ret.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        private static IEnumerable<byte> GetHash(string input, HashType hash,string saltValue)
        {
            var inputBytes = Encoding.ASCII.GetBytes(input + saltValue);

            switch (hash)
            {
                case HashType.Hmac:
                    return HMAC.Create().ComputeHash(inputBytes);
                case HashType.Hmacmd5:
                    using (var hashProvider = new HMACMD5())
                        return hashProvider.ComputeHash(inputBytes);
                case HashType.Hmacsha1:
                    using (var hashProvider = new HMACSHA1())
                        return hashProvider.ComputeHash(inputBytes);
                case HashType.Hmacsha256:
                    using (var hashProvider = new HMACSHA256())
                        return hashProvider.ComputeHash(inputBytes);
                case HashType.Hmacsha384:
                    using (var hashProvider = new HMACSHA384())
                        return hashProvider.ComputeHash(inputBytes);
                case HashType.Hmacsha512:
                    using (var hashProvider = new HMACSHA512())
                        return hashProvider.ComputeHash(inputBytes);
                case HashType.Md5:
                    using (var hashProvider = new MD5CryptoServiceProvider())
                        return hashProvider.ComputeHash(inputBytes);
                case HashType.Sha1:
                    using (var hashProvider = new SHA1CryptoServiceProvider())
                        return hashProvider.ComputeHash(inputBytes);
                case HashType.Sha256:
                    using (var hashProvider = new SHA256CryptoServiceProvider())
                        return hashProvider.ComputeHash(inputBytes);
                case HashType.Sha384:
                    using (var hashProvider = new SHA384CryptoServiceProvider())
                        return hashProvider.ComputeHash(inputBytes);
                case HashType.Sha512:
                    using (var hashProvider = new SHA512CryptoServiceProvider())
                        return hashProvider.ComputeHash(inputBytes);
                default:
                    return inputBytes;
            }
        }
    }
}

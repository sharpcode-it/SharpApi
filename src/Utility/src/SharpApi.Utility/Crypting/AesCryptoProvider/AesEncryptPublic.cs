using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SharpApi.Utility.Crypting
{
    public partial class AesCryptoProvider
    {
        /// <summary>
        /// Encrypts a string value generating a base64-encoded string.
        /// </summary>
        /// <param name="plainText">
        /// Plain text string to be encrypted.
        /// </param>
        /// <returns>
        /// Cipher text formatted as a base64-encoded string.
        /// </returns>
        public string Encrypt(string plainText)
        {
            return Encrypt(Encoding.UTF8.GetBytes(plainText));
        }

        /// <summary>
        /// Encrypts a byte array generating a base64-encoded string.
        /// </summary>
        /// <param name="plainTextBytes">
        /// Plain text bytes to be encrypted.
        /// </param>
        /// <returns>
        /// Cipher text formatted as a base64-encoded string.
        /// </returns>
        private string Encrypt(byte[] plainTextBytes)
        {
            return Convert.ToBase64String(EncryptToBytes(plainTextBytes));
        }

        /// <summary>
        /// Encrypts a string value generating a byte array of cipher text.
        /// </summary>
        /// <param name="plainText">
        /// Plain text string to be encrypted.
        /// </param>
        /// <returns>
        /// Cipher text formatted as a byte array.
        /// </returns>
        public byte[] EncryptToBytes(string plainText)
        {
            return EncryptToBytes(Encoding.UTF8.GetBytes(plainText));
        }

        /// <summary>
        /// Encrypts a byte array generating a byte array of cipher text.
        /// </summary>
        /// <param name="plainTextBytes">
        /// Plain text bytes to be encrypted.
        /// </param>
        /// <returns>
        /// Cipher text formatted as a byte array.
        /// </returns>
        private byte[] EncryptToBytes(byte[] plainTextBytes)
        {
            // Add salt at the beginning of the plain text bytes (if needed).
            var plainTextBytesWithSalt = (UseSalt()) ? AddSalt(plainTextBytes) : plainTextBytes;

            // Let's make cryptographic operations thread-safe.
            lock (this)
            {
                // Encryption will be performed using memory stream.
                byte[] cipherTextBytes = null;
                using var memoryStream = new MemoryStream();
                // To perform encryption, we must use the Write mode.
                using (var cryptoStream = new CryptoStream(
                    memoryStream,
                    _encryptor,
                    CryptoStreamMode.Write))
                {

                    // Start encrypting data.
                    cryptoStream.Write(plainTextBytesWithSalt, 0, plainTextBytesWithSalt.Length);
                    // Finish the encryption operation.
                    cryptoStream.FlushFinalBlock();
                    // Move encrypted data from memory into a byte array.
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
                // Return encrypted data.
                return cipherTextBytes;
            }
        }
    }
}

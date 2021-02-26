using System;
using System.Security.Cryptography;
using System.Text;
using SharpApi.Utility.Crypting.Model;

namespace SharpApi.Utility.Crypting
{
    public partial class AesCryptoProvider
    {
        #region Private members
        // These members will be used to perform encryption and decryption.
        private readonly ICryptoTransform _encryptor = null;
        private readonly ICryptoTransform _decryptor = null;

        private readonly AesCryptOptions _options = null;
        #endregion

        #region Constructors
        /// <summary>
        /// Use this constructor to perform encryption/decryption with the following options:
        /// - 128/192/256/512-bit key (depending on passPhrase length in bits)
        /// - SHA-2 password hashing algorithm with 4-to-8 byte long password hash salt and 1 password iteration
        /// - hashing without salt
        /// - cipher block chaining (CBC) mode
        /// </summary>
        /// <param name="passPhrase">
        /// Passphrase (in string format) from which a pseudo-random password will be derived. The derived password will be used to generate the encryption key.
        /// </param>
        /// <param name="initVector">
        /// Initialization vector (IV). This value is required to encrypt the first block of plaintext data. IV must be exactly 16 ASCII characters long.
        /// </param>
        public AesCryptoProvider(string passPhrase,
            string initVector) :
            this(passPhrase, initVector, new AesCryptOptions())
        {
        }

        /// <summary>
        /// Use this constructor to perform encryption/decryption with custom options.
        /// See AESCryptOptions documentation for details.
        /// </summary>
        /// <param name="passPhrase">
        /// Passphrase (in string format) from which a pseudo-random password will be derived. The derived password will be used to generate the encryption key.
        /// </param>
        /// <param name="initVector">
        /// Initialization vector (IV). This value is required to encrypt the first block of plaintext data. IV must be exactly 16 ASCII characters long.
        /// </param>
        /// <param name="options">
        /// A set of customized (or default) options to use for the encryption/decryption: see AESCryptOptions documentation for details.
        /// </param>
        public AesCryptoProvider(string passPhrase, string initVector, AesCryptOptions options)
        {
            // store the options object locally.
            this._options = options;

            // Checks for the correct (or null) size of cryptographic key.
            if (_options.FixedKeySize.HasValue
                && _options.FixedKeySize != 128
                && _options.FixedKeySize != 192
                && _options.FixedKeySize != 256)
                throw new NotSupportedException("ERROR: options.FixedKeySize must be NULL (for auto-detect) or have a value of 128, 192 or 256");

            // Initialization vector converted to a byte array.

            // Salt used for password hashing (to generate the key, not during
            // encryption) converted to a byte array.

            // Get bytes of initialization vector.
            var initVectorBytes = initVector == null ? new byte[0] : Encoding.UTF8.GetBytes(initVector);

            // Gets the KeySize
            var keySize = _options.FixedKeySize ?? GetAesKeySize(passPhrase);

            // Get bytes of password (hashing it or not)
            byte[] keyBytes;
            if (_options.PasswordHash == AesHashType.None)
            {
                // Convert passPhrase to a byte array
                keyBytes = Encoding.UTF8.GetBytes(passPhrase);
            }
            else
            {
                // Get bytes of password hash salt
                var saltValueBytes = _options.PasswordHashSalt == null ? new byte[0] : Encoding.UTF8.GetBytes(options.PasswordHashSalt);

                // Generate password, which will be used to derive the key.
                //var password = new PasswordDeriveBytes(
                //    passPhrase,
                //    saltValueBytes,
                //    _options.PasswordHash.ToString().ToUpper().Replace("-", ""),
                //    _options.PasswordHashIterations);

                var password = new Rfc2898DeriveBytes(passPhrase, saltValueBytes, _options.PasswordHashIterations, HashAlgorithmName.SHA256);

                // Convert key to a byte array adjusting the size from bits to bytes.
                keyBytes = password.GetBytes(keySize / 8);
            }

            // Initialize AES key object.
            var symmetricKey = new AesManaged
            {
                Padding = _options.PaddingMode,
                Mode = (initVectorBytes.Length == 0)
                    ? CipherMode.ECB
                    : CipherMode.CBC
            };

            // Sets the padding mode

            // Use the unsafe ECB cypher mode (not recommended) if no IV has been provided, otherwise use the more secure CBC mode.

            // Create the encryptor and decryptor objects, which we will use for cryptographic operations.
            _encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            _decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
        }
        #endregion
    }
}

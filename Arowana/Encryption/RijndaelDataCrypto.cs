#region - Using Statements -

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Arowana.Encryption
{
    public class RijndaelDataCrypto : DataCryptoBase, IDisposable
    {

        #region - Constants -

        private const HashAlgorithm DEFAULT_HASH_ALGORITHM = HashAlgorithm.SHA1;
        private const KeySize DEFAULT_KEY_SIZE = KeySize.KeySize256;
        private const int DEFAULT_PASSWORD_ITERATIONS = 2;
        private static readonly Encoding DEFAULT_ENCODING = Encoding.UTF8;

        #endregion

        #region - Fields -

        private SymmetricAlgorithm _cryptoAlgorithm;
        private ICryptoTransform _encryptor;
        private ICryptoTransform _decryptor;

        #endregion

        #region - Constructor -

        public RijndaelDataCrypto(string password, string salt, string iv)
            : this(password, salt, new InitialVector(iv))
        {
        }

        public RijndaelDataCrypto(string password, string salt, InitialVector iv)
            : base(password, salt, iv, DEFAULT_HASH_ALGORITHM, DEFAULT_KEY_SIZE, DEFAULT_PASSWORD_ITERATIONS, DEFAULT_ENCODING)
        {
            _cryptoAlgorithm = GetCryptoAlgorithm();
            _encryptor = GetEncryptor(_cryptoAlgorithm);
            _decryptor = GetDecryptor(_cryptoAlgorithm);
        }

        #endregion

        #region - IDataCrypto Members -

        public override byte[] Encrypt(byte[] inputBytes)
        {
            if (inputBytes == null)
            {
                throw new ArgumentNullException(nameof(inputBytes));
            }

            byte[] cipherTextBytes = null;
            using (MemoryStream memStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memStream, _encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(inputBytes, 0, inputBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memStream.ToArray();
                }
            }

            return cipherTextBytes;
        }
        
        public override byte[] Decrypt(byte[] inputBytes)
        {
            if (inputBytes == null)
            {
                throw new ArgumentNullException(nameof(inputBytes));
            }

            byte[] decryptedBytes = new byte[inputBytes.Length];
            using (MemoryStream memStream = new MemoryStream(inputBytes))
            {
                using (CryptoStream cryptoStream = new CryptoStream(memStream, _decryptor, CryptoStreamMode.Read))
                {
                    cryptoStream.Read(decryptedBytes, 0, decryptedBytes.Length);
                }
            }

            return decryptedBytes;
        }

        #endregion

        #region - Private Methods -

        private SymmetricAlgorithm GetCryptoAlgorithm()
        {
            AesManaged crypto = new AesManaged();
            crypto.Mode = CipherMode.CBC;
            crypto.Padding = PaddingMode.Zeros;
            return crypto;
        }

        private ICryptoTransform GetEncryptor(SymmetricAlgorithm algorithm)
        {
            byte[] ivBytes = IV.ToByteArray();
            byte[] keyBytes = GetPasswordBytes();

            return algorithm.CreateEncryptor(keyBytes, ivBytes);
        }

        private ICryptoTransform GetDecryptor(SymmetricAlgorithm algorithm)
        {
            byte[] ivBytes = IV.ToByteArray();
            byte[] keyBytes = GetPasswordBytes();

            return algorithm.CreateDecryptor(keyBytes, ivBytes);
        }

        #endregion

        #region - IDisposable Members -
        
        public override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_cryptoAlgorithm != null)
                {
                    _cryptoAlgorithm.Dispose();
                    _cryptoAlgorithm = null;
                }

                if (_encryptor != null)
                {
                    _encryptor.Dispose();
                    _encryptor = null;
                }
            }
        }

        #endregion

    }
}

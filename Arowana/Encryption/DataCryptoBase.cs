#region - Using Statements -

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Arowana.Encryption
{
    public abstract class DataCryptoBase : IDisposable
    {

        #region - Constructor -

        public DataCryptoBase(string password, string salt, InitialVector iv, HashAlgorithm hashAlgorithm, KeySize keySize, int passwordIterations, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException("password");
            }
            if (string.IsNullOrWhiteSpace(salt))
            {
                throw new ArgumentNullException("salt");
            }
            if (iv == null)
            {
                throw new ArgumentNullException("iv");
            }

            this.Password = password;
            this.Salt = salt;
            this.IV = iv;
            this.HashAlgorithm = hashAlgorithm;
            this.KeySize = keySize;
            this.PasswordIterations = passwordIterations;
            this.Encoding = encoding;
        }

        #endregion

        #region - Properties -

        public string Password { get; private set; }
        public string Salt { get; private set; }
        public InitialVector IV { get; private set; }

        public HashAlgorithm HashAlgorithm { get; private set; }
        public KeySize KeySize { get; private set; }
        public int PasswordIterations { get; private set; }
        public Encoding Encoding { get; private set; }

        #endregion

        #region - Public Methods -
        
        public abstract byte[] Encrypt(byte[] inputBytes);
        public abstract byte[] Decrypt(byte[] inputBytes);
        public abstract void Dispose(bool disposing);

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion

        #region - Protected Methods -

        protected byte[] GetPasswordBytes()
        {
            byte[] saltBytes = Encoding.ASCII.GetBytes(Salt);
            string hashName = Enum.GetName(typeof(Encryption.HashAlgorithm), this.HashAlgorithm);
            int keySize = (int)this.KeySize;

            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password, saltBytes, hashName, this.PasswordIterations);
            return pdb.GetBytes(keySize);
        }

        protected byte[] ToByteArray<T>(T obj)
        {
            if (obj == null)
            {
                return null;
            }
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        protected T FromByteArray<T>(byte[] array)
        {
            if (array == null)
            {
                return default(T);
            }
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(array))
            {
                T obj = (T)bf.Deserialize(ms);
                return obj;
            }
        }

        #endregion

    }
}

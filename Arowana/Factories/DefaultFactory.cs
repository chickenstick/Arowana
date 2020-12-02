#region - Using Statements -

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arowana.Actions;
using Arowana.Compression;
using Arowana.Encryption;
using Arowana.Serialization;
using Arowana.Settings;
using Arowana.Storage;

#endregion

namespace Arowana.Factories
{
    public class DefaultFactory : IFactory
    {

        #region - Fields -

        private string _password;
        private string _iv;
        private string _salt;

        #endregion

        #region - Constructor -

        public DefaultFactory(string password, string iv, string salt)
        {
            _password = password;
            _iv = iv;
            _salt = salt;
        }

        #endregion

        #region - Public Methods -

        public ActionList GetActionList()
        {
            ISerializer serializer = GetSerializer();
            DataCryptoBase crypto = GetEncryptor();
            IStringSerializer stringSerializer = GetStringSerializer();

            EncryptionAction encryptionAction = new EncryptionAction(crypto);

            CompressorBase compressor = GetCompressor();
            CompressionAction compressionAction = new CompressionAction(compressor);

            return new ActionList(serializer, stringSerializer, encryptionAction, compressionAction);
        }

        public CompressorBase GetCompressor() => new GZipCompressor();

        public DataCryptoBase GetEncryptor()
        {
            ISettings settings = GetSettings();
            return new RijndaelDataCrypto(settings.Password, settings.Salt, settings.IV);
        }

        public ISerializer GetSerializer() => new JsonSerializer();

        public ISettings GetSettings() => new SettingsToken(_password, _iv, _salt);

        public virtual IStorage GetStorage()
        {
            IStringSerializer stringSerializer = GetStringSerializer();
            return new FileStorage(stringSerializer);
        }

        public IStringSerializer GetStringSerializer() => new Base64Serializer();

        #endregion

    }
}

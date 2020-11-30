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

        #endregion

        #region - Constructor -

        public DefaultFactory(string password)
        {
            _password = password;
        }

        #endregion

        #region - Public Methods -

        public ActionList GetActionList()
        {
            ISerializer serializer = this.GetSerializer();
            DataCryptoBase crypto = this.GetEncryptor();
            EncryptionAction encryptionAction = new EncryptionAction(crypto);
            CompressorBase compressor = this.GetCompressor();
            CompressionAction compressionAction = new CompressionAction(compressor);
            ActionList actionList = new ActionList(serializer, encryptionAction, compressionAction);
            return actionList;
        }

        public CompressorBase GetCompressor()
        {
            return new GZipCompressor();
        }

        public DataCryptoBase GetEncryptor()
        {
            ISettings settings = this.GetSettings();
            return new RijndaelDataCrypto(settings.Password, settings.Salt, settings.IV);
        }

        public ISerializer GetSerializer()
        {
            return new JsonSerializer();
        }

        public ISettings GetSettings()
        {
            SettingsToken settings = new SettingsToken(_password);
            return settings;
        }

        public IStorage GetStorage()
        {
            return new FileStorage();
        }

        #endregion

    }
}

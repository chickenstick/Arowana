#region - Using Statements -

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arowana.Actions;
using Arowana.Compression;
using Arowana.Encryption;
using Arowana.Factories;
using Arowana.Serialization;
using Arowana.Settings;
using Arowana.Storage;

#endregion

namespace Arowana.Test
{
    public class TestingFactory : IFactory
    {

        #region - Fields -

        private string _password;

        #endregion

        #region - Constructor -

        public TestingFactory(string password)
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
            return new RijndaelDataCrypto(_password, settings.Salt, settings.IV);
        }

        public ISerializer GetSerializer()
        {
            return new JsonSerializer();
        }

        public ISettings GetSettings()
        {
            return new TestingSettings();
        }

        public IStorage GetStorage()
        {
            return new TestingStorage();
        }

        #endregion

    }
}

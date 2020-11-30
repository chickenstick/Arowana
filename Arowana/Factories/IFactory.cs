#region - Using Statements -

using System;
using System.Collections.Generic;
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
    public interface IFactory
    {
        ActionList GetActionList();
        CompressorBase GetCompressor();
        DataCryptoBase GetEncryptor();
        ISerializer GetSerializer();
        ISettings GetSettings();
        IStorage GetStorage();
    }
}

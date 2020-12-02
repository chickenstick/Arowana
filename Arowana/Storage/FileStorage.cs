#region - Using Statements -

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Arowana.Serialization;

#endregion

namespace Arowana.Storage
{
    public class FileStorage : IStorage
    {

        #region - Fields -

        private IStringSerializer _stringSerializer;

        #endregion

        #region - Constructor -

        public FileStorage(IStringSerializer stringSerializer)
        {
            _stringSerializer = stringSerializer;
        }

        #endregion

        #region - Public Methods -

        public string RetrieveData(string path)
        {
            byte[] bytes = File.ReadAllBytes(path);
            return _stringSerializer.Stringify(bytes);
        }

        public void StoreData(string path, string data)
        {
            byte[] inputBytes = _stringSerializer.Destringify(data);
            using (FileStream fStream = new FileStream(path, FileMode.Create))
            {
                fStream.Write(inputBytes, 0, inputBytes.Length);
            }
        }

        #endregion

    }
}

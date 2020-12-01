#region - Using Statements -

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Arowana.Storage
{
    public class FileStorage : IStorage
    {

        #region - Constructor -

        public FileStorage()
        {
        }

        #endregion

        #region - Public Methods -

        public string RetrieveData(string path)
        {
            byte[] bytes = File.ReadAllBytes(path);
            return Convert.ToBase64String(bytes);
        }

        public void StoreData(string path, string data)
        {
            byte[] inputBytes = Convert.FromBase64String(data);
            using (FileStream fStream = new FileStream(path, FileMode.Create))
            {
                fStream.Write(inputBytes, 0, inputBytes.Length);
            }
        }

        #endregion

    }
}

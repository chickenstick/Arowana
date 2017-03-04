#region - Using Statements -

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Arowana.Storage;

#endregion

namespace Arowana.Test
{
    public class TestingStorage : IStorage
    {

        #region - Fields -

        private Dictionary<string, string> _dictionary;

        #endregion

        #region - Constructor -

        public TestingStorage()
        {
            _dictionary = new Dictionary<string, string>();
        }

        #endregion

        #region - Public Methods -

        public string RetrieveData(string path)
        {
            return _dictionary[path];
        }

        public void StoreData(string path, string data)
        {
            if (_dictionary.ContainsKey(path))
            {
                _dictionary[path] = data;
            }
            else
            {
                _dictionary.Add(path, data);
            }
        }

        #endregion

    }
}

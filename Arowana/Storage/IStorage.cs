#region - Using Statements -

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Arowana.Storage
{
    public interface IStorage
    {
        void StoreData(string path, string data);
        string RetrieveData(string path);
    }
}

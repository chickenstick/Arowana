#region - Using Statements -

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Arowana.Serialization
{
    public interface ISerializer
    {
        byte[] Serialize<T>(T input);
        T Deserialize<T>(byte[] output);
    }
}

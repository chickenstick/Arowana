#region - Using Statements -

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Arowana.Serialization
{
    public class BitSerializer : ISerializer
    {
        public T Deserialize<T>(byte[] output)
        {
            if (output == null)
            {
                return default(T);
            }
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(output))
            {
                T obj = (T)bf.Deserialize(ms);
                return obj;
            }
        }

        public byte[] Serialize<T>(T input)
        {
            if (input == null)
            {
                return null;
            }
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, input);
                return ms.ToArray();
            }
        }
    }
}

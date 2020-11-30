#region - Using Statements -

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

#endregion

namespace Arowana.Serialization
{
    public class JsonSerializer : ISerializer
    {

        #region - Constructor -

        public JsonSerializer()
        {

        }

        #endregion

        #region - Public Methods -

        public T Deserialize<T>(byte[] output)
        {
            string json = Encoding.UTF8.GetString(output);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public byte[] Serialize<T>(T input)
        {
            string json = JsonConvert.SerializeObject(input);
            return Encoding.UTF8.GetBytes(json);
        }

        #endregion

    }
}

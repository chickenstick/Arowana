using System;
using System.Collections.Generic;
using System.Text;

namespace Arowana.Serialization
{
    public class Base64Serializer : IStringSerializer
    {

        #region - Constructor -

        public Base64Serializer()
        {
        }

        #endregion

        #region - Public Methods -

        public string Stringify(byte[] output) => Convert.ToBase64String(output);

        public byte[] Destringify(string input) => Convert.FromBase64String(input);

        #endregion

    }
}

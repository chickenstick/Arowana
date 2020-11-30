#region - Using Statements -

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Arowana.Compression
{
    public abstract class CompressorBase
    {

        #region - Constructor -

        public CompressorBase()
        {

        }

        #endregion

        #region - Public Methods -

        public abstract byte[] Compress(byte[] inputBytes);
        public abstract byte[] Decompress(byte[] inputBytes);

        #endregion

    }
}

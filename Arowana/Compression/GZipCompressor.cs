#region - Using Statements -

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Arowana.Compression
{
    public class GZipCompressor : CompressorBase
    {

        #region - Constructor -

        public GZipCompressor()
        {

        }

        #endregion

        #region - Public Methods -

        public override byte[] Compress(byte[] inputBytes)
        {
            using (MemoryStream outputStream = new MemoryStream())
            {
                using (GZipStream zipStream = new GZipStream(outputStream, CompressionLevel.Optimal, false))
                {
                    using (MemoryStream inputStream = new MemoryStream(inputBytes))
                    {
                        inputStream.CopyTo(zipStream);
                    }
                }
                return outputStream.ToArray();
            }
        }

        public override byte[] Decompress(byte[] inputBytes)
        {
            using (MemoryStream inputStream = new MemoryStream(inputBytes))
            {
                using (GZipStream zipStream = new GZipStream(inputStream, CompressionMode.Decompress))
                {
                    using (MemoryStream outputStream = new MemoryStream())
                    {
                        zipStream.CopyTo(outputStream);
                        return outputStream.ToArray();
                    }
                }
            }
        }

        #endregion

    }
}

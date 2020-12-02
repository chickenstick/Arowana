#region - Using Statements -

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Arowana.Compression;

#endregion

namespace Arowana.Actions
{
    public sealed class CompressionAction : IAction
    {

        #region - Fields -

        private CompressorBase _compressor;

        #endregion

        #region - Constructor -

        public CompressionAction(CompressorBase compressor) 
            : base()
        {
            _compressor = compressor;
        }

        #endregion

        #region - Public Methods -

        public byte[] DoAction(byte[] inputBytes) => _compressor.Compress(inputBytes);

        public byte[] ReverseAction(byte[] inputBytes) => _compressor.Decompress(inputBytes);

        #endregion

    }
}

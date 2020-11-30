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
    public sealed class CompressionAction : ActionBase
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

        public override byte[] DoAction(byte[] inputBytes)
        {
            byte[] outputBytes = _compressor.Compress(inputBytes);

            if (NextAction != null)
            {
                return NextAction.DoAction(outputBytes);
            }
            return outputBytes;
        }

        public override byte[] ReverseAction(byte[] inputBytes)
        {
            byte[] outputBytes = _compressor.Decompress(inputBytes);

            if (PreviousAction != null)
            {
                return PreviousAction.ReverseAction(outputBytes);
            }
            return outputBytes;
        }

        #endregion

    }
}

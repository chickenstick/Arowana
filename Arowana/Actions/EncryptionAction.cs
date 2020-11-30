#region - Using Statements -

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Arowana.Encryption;

#endregion

namespace Arowana.Actions
{
    public sealed class EncryptionAction : ActionBase
    {

        #region - Fields -

        private DataCryptoBase _encryptor;

        #endregion

        #region - Constructor -

        public EncryptionAction(DataCryptoBase encryptor)
            : base()
        {
            _encryptor = encryptor;
        }

        #endregion

        #region - Public Methods -

        public override byte[] DoAction(byte[] inputBytes)
        {
            byte[] outputBytes = _encryptor.Encrypt(inputBytes);

            if (NextAction != null)
            {
                return NextAction.DoAction(outputBytes);
            }
            return outputBytes;
        }

        public override byte[] ReverseAction(byte[] inputBytes)
        {
            byte[] outputBytes = _encryptor.Decrypt(inputBytes);

            if (PreviousAction != null)
            {
                return PreviousAction.ReverseAction(outputBytes);
            }
            return outputBytes;
        }

        #endregion

    }
}

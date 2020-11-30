#region - Using Statements -

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Arowana.Actions
{
    public abstract class ActionBase
    {

        #region - Constructor -

        public ActionBase()
        {
            PreviousAction = null;
            NextAction = null;
        }

        #endregion

        #region - Properties -

        public ActionBase NextAction { get; set; }
        public ActionBase PreviousAction { get; set; }

        #endregion

        #region - Public Methods -

        public abstract byte[] DoAction(byte[] inputBytes);
        public abstract byte[] ReverseAction(byte[] inputBytes);

        #endregion

    }
}

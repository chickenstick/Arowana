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
    public interface IAction
    {
        #region - Methods -

        byte[] DoAction(byte[] inputBytes);
        byte[] ReverseAction(byte[] inputBytes);

        #endregion

    }
}

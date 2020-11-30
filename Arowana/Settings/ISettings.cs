#region - Using Statements -

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Arowana.Settings
{
    public interface ISettings
    {
        string Password { get; }
        string IV { get; }
        string Salt { get; }
    }
}

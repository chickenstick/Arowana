#region - Using Statements -

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Arowana.Settings
{
    public class SettingsToken : ISettings
    {

        #region - Constructor -

        public SettingsToken(string password, string iv, string salt)
        {
            this.Password = password;
            this.IV = iv;
            this.Salt = salt;
        }

        #endregion

        #region - Properties -
        
        public string IV { get; private set; }
        public string Password { get; private set; }
        public string Salt { get; private set; }

        #endregion

    }
}

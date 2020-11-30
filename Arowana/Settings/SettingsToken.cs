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

        public SettingsToken(string password)
        {
            this.Password = password;
            this.IV = ConfigurationManager.AppSettings["IV"];
            this.Salt = ConfigurationManager.AppSettings["Salt"];
        }

        #endregion

        #region - Properties -
        
        public string IV { get; private set; }
        public string Password { get; private set; }
        public string Salt { get; private set; }

        #endregion

    }
}

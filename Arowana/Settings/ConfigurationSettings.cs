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
    public class ConfigurationSettings : ISettings
    {

        #region - Constructor -

        public ConfigurationSettings()
        {
            Password = ConfigurationManager.AppSettings["Password"];
            IV = ConfigurationManager.AppSettings["IV"];
            Salt = ConfigurationManager.AppSettings["Salt"];
        }

        #endregion

        #region - Properties -

        public string IV { get; private set; }

        public string Password { get; private set; }

        public string Salt { get; private set; }

        #endregion

    }
}

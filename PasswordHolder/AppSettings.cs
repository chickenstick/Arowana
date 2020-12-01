using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.Configuration;

namespace PasswordHolder
{
    public class AppSettings
    {

        #region - Constructors -

        public AppSettings()
        {
        }

        #endregion

        #region - Properties -

        public string IV { get; set; }
        public string Salt { get; set; }

        #endregion

    }
}

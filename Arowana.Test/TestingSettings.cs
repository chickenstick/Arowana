#region - Using Statements -

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Arowana.Settings;

#endregion

namespace Arowana.Test
{
    public class TestingSettings : ISettings
    {

        #region - Constructor -

        public TestingSettings()
        {

        }

        #endregion

        #region - Properties -

        public string IV
        {
            get
            {
                return "FartedInPublic69";
            }
        }

        public string Password
        {
            get
            {
                return "buttsmells";
            }
        }

        public string Salt
        {
            get
            {
                return "jfdg8jlFJH89lkJsdf9jm8n*y6(*^jlkfdj";
            }
        }

        #endregion

    }
}

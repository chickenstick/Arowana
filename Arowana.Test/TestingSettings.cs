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

        public string IV => "FartedInPublic69";
        public string Password => "buttsmells";
        public string Salt => "jfdg8jlFJH89lkJsdf9jm8n*y6(*^jlkfdj";

        #endregion

    }
}

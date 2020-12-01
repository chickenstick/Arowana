#region - Using Statements -

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arowana.Actions;
using Arowana.Compression;
using Arowana.Encryption;
using Arowana.Factories;
using Arowana.Serialization;
using Arowana.Settings;
using Arowana.Storage;

#endregion

namespace Arowana.Test
{
    public class TestingFactory : DefaultFactory
    {

        #region - Constructor -

        public TestingFactory(string password, string iv, string salt)
            : base(password, iv, salt)
        {
        }

        #endregion

        #region - Public Methods -

        public override IStorage GetStorage() => new TestingStorage();

        #endregion

    }
}

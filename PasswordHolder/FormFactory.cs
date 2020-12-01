#region - Using Statements -

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Arowana.Factories;
using Arowana.Storage;

#endregion

namespace PasswordHolder
{
    public static class FormFactory
    {

        #region - Public Methods -

        public static IFactory GetFactory(string password, string iv, string salt)
        {
            return new DefaultFactory(password, iv, salt);
        }

        public static IStorage GetStorage()
        {
            return new FileStorage();
        }

        public static PasswordForm GetPasswordForm()
        {
            return new PasswordForm();
        }

        public static AccountForm GetAccountForm()
        {
            return new AccountForm();
        }

        #endregion

    }
}

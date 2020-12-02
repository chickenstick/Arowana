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

        public static IFactory GetFactory(string password, string iv, string salt) => new DefaultFactory(password, iv, salt);
        public static PasswordForm GetPasswordForm() => new PasswordForm();
        public static AccountForm GetAccountForm() => new AccountForm();

        #endregion

    }
}

#region - Using Statements -

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Arowana.Serialization
{
    public static class SerializationConstants
    {

        public static class Credential
        {
            public const string USER_NAME = "a";
            public const string PASSWORD = "b";
            public const string CREATE_DATE = "c";
        }

        public static class Account
        {
            public const string NAME = "d";
            public const string CREATE_DATE = "e";
            public const string ACTIVE_CREDENTIAL = "f";
            public const string CREDENTIAL_HISTORY = "g";
            public const string NOTES = "h";
        }

        public static class AccountCollection
        {
            public const string DICTIONARY = "i";
        }

    }
}

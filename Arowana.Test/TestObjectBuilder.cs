using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Arowana;

namespace Arowana.Test
{
    public static class TestObjectBuilder
    {

        public static Credential GetCredential1()
        {
            Credential cred1 = new Credential();
            cred1.CreatedDate = new DateTime(2017, 1, 3);
            cred1.UserName = "me@hotmail.com";
            cred1.Password = "farted";
            return cred1;
        }

        public static Credential GetCredential2()
        {
            Credential cred2 = new Credential();
            cred2.CreatedDate = new DateTime(2017, 1, 1);
            cred2.UserName = "me@hotmail.com";
            cred2.Password = "buttsmells";
            return cred2;
        }

        public static Credential GetCredential3()
        {
            Credential cred3 = new Credential();
            cred3.CreatedDate = new DateTime(2017, 1, 2);
            cred3.UserName = "me@yahoo.com";
            cred3.Password = "test";
            return cred3;
        }

        public static Credential GetCredentialWithNullProperties()
        {
            Credential cred = new Credential();
            cred.UserName = null;
            cred.Password = null;
            return cred;
        }

        public static Account GetAccount1()
        {
            Account hotmailAccount = new Account();
            hotmailAccount.Name = "Hotmail";
            hotmailAccount.Notes = "Test";
            hotmailAccount.CreateDate = new DateTime(2010, 1, 1);
            hotmailAccount.ActiveCredential = GetCredential1();
            hotmailAccount.CredentialHistory.Add(hotmailAccount.ActiveCredential);
            hotmailAccount.CredentialHistory.Add(GetCredential2());
            return hotmailAccount;
        }

        public static Account GetAccount2()
        {
            Account yahooAccount = new Account();
            yahooAccount.Name = "Yahoo!";
            yahooAccount.Notes = "none";
            yahooAccount.CreateDate = new DateTime(2011, 1, 1);
            yahooAccount.ActiveCredential = GetCredential3();
            yahooAccount.CredentialHistory.Add(yahooAccount.ActiveCredential);
            return yahooAccount;
        }

        public static Account GetAccountWithNullProperties()
        {
            Account account = new Account();
            account.ActiveCredential = null;
            account.CredentialHistory = null;
            account.Name = null;
            account.Notes = null;
            return account;
        }

        public static AccountCollection GetAccountCollection()
        {
            AccountCollection coll = new AccountCollection();
            coll.Add(GetAccount1());
            coll.Add(GetAccount2());
            return coll;
        }

        public static AccountCollection GetAccountCollection2()
        {
            AccountCollection coll = new AccountCollection();
            coll.Add(GetAccount1());

            Account account = GetAccount2();
            account.CredentialHistory.Add(GetCredential1());
            coll.Add(account);
            return coll;
        }

        public static AccountCollection GetAccountCollectionWithNoAccounts()
        {
            AccountCollection coll = new AccountCollection();
            return coll;
        }
    }
}

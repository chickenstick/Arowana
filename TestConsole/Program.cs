using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Arowana;
using Arowana.Actions;
using Arowana.Encryption;
using Arowana.Factories;
using Arowana.Serialization;
using Arowana.Settings;
using Arowana.Storage;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestSerialization();
            //TestEncryptionAndCompression();
            TestFileSave();

            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Press any key to exit...");
            Console.ReadKey(true);
        }

        private static AccountCollection GetTestCollection()
        {
            AccountCollection coll = new AccountCollection();

            Account hotmailAccount = new Account();
            hotmailAccount.Name = "Hotmail";
            hotmailAccount.Notes = "Test";
            hotmailAccount.CreateDate = new DateTime(2010, 1, 1);
            coll.Add(hotmailAccount);

            Credential cred1 = new Credential();
            cred1.CreatedDate = DateTime.Now;
            cred1.UserName = "me@hotmail.com";
            cred1.Password = "farted";
            hotmailAccount.ActiveCredential = cred1;
            hotmailAccount.CredentialHistory.Add(cred1);

            Credential cred2 = new Credential();
            cred2.CreatedDate = DateTime.Now;
            cred2.UserName = "me@hotmail.com";
            cred2.Password = "buttsmells";
            hotmailAccount.CredentialHistory.Add(cred2);

            Account yahooAccount = new Account();
            yahooAccount.Name = "Yahoo!";
            yahooAccount.Notes = "none";
            yahooAccount.CreateDate = new DateTime(2011, 1, 1);
            coll.Add(yahooAccount);

            Credential cred3 = new Credential();
            cred3.CreatedDate = DateTime.Now;
            cred3.UserName = "me@yahoo.com";
            cred3.Password = "test";
            yahooAccount.ActiveCredential = cred3;
            yahooAccount.CredentialHistory.Add(cred3);

            return coll;
        }

        //private static void TestSerialization()
        //{
        //    AccountCollection collection = GetTestCollection();
        //    Console.WriteLine(collection);

        //    ActionList actionList = new ActionList(new BitSerializer(), new ActionBase[0]);
        //    string serialized = actionList.DoActions(collection);
        //    AccountCollection collection1 = actionList.ReverseActions<AccountCollection>(serialized);
        //    Console.WriteLine(collection1);
        //}

        //private static void TestEncryption()
        //{
        //    AccountCollection collection = GetTestCollection();
        //    Console.WriteLine(collection);

        //    ConfigurationSettings configSettings = new ConfigurationSettings();
        //    DataCryptoBase crypto = new RijndaelDataCrypto(configSettings.Password, configSettings.Salt, configSettings.IV);
        //    EncryptionAction encryptionAction = new EncryptionAction(crypto);
        //    ActionList actionList = new ActionList(new BitSerializer(), encryptionAction);
        //    string serialized = actionList.DoActions(collection);
        //    AccountCollection collection1 = actionList.ReverseActions<AccountCollection>(serialized);
        //    Console.WriteLine(collection1);
        //}

        //private static void TestEncryptionAndCompression()
        //{
        //    AccountCollection collection = GetTestCollection();
        //    Console.WriteLine(collection);

        //    ConfigurationSettings configSettings = new ConfigurationSettings();
        //    DataCryptoBase crypto = new RijndaelDataCrypto(configSettings.Password, configSettings.Salt, configSettings.IV);
        //    EncryptionAction encryptionAction = new EncryptionAction(crypto);
        //    ActionList actionList = new ActionList(new BitSerializer(), encryptionAction, new CompressionAction());
        //    string serialized = actionList.DoActions(collection);
        //    AccountCollection collection1 = actionList.ReverseActions<AccountCollection>(serialized);
        //    Console.WriteLine(collection1);
        //}

        private static void TestFileSave()
        {
            AccountCollection collection = GetTestCollection();
            Console.WriteLine(collection);
            
            IFactory factory = new DefaultFactory("smellyourbeans");
            ActionList actionList = factory.GetActionList();
            IStorage storage = factory.GetStorage();

            string serialized = actionList.DoActions(collection);
            storage.StoreData("file.dat", serialized);

            string fromFile = storage.RetrieveData("file.dat");
            AccountCollection collection1 = actionList.ReverseActions<AccountCollection>(fromFile);
            Console.WriteLine(collection1);
        }

    }
}

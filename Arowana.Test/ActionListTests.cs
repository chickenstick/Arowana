﻿#region - Using Statements -

using System;
using NUnit.Framework;

using Arowana.Actions;
using Arowana.Exceptions;
using Arowana.Factories;
using Arowana.Storage;

#endregion

namespace Arowana.Test
{
    [TestFixture]
    public class ActionListTests
    {

        #region - Constants -

        private const string PASSWORD = "realpassword";
        private const string IV = "FartedInPublic69";
        private const string SALT = "jfdg8jlFJH89lkJsdf9jm8n*y6(*^jlkfdj";

        #endregion

        [Test]
        public void TestSerializeAndDeserialize()
        {
            IFactory factory = new TestingFactory(PASSWORD, IV, SALT);
            ActionList actionList = factory.GetActionList();
            IStorage storage = factory.GetStorage();

            AccountCollection collection = TestObjectBuilder.GetAccountCollection();
            string serialized = actionList.DoActions(collection);
            storage.StoreData("test", serialized);

            string fromStorage = storage.RetrieveData("test");
            AccountCollection deserialized = actionList.ReverseActions<AccountCollection>(fromStorage);
            Assert.AreEqual(collection, deserialized);
        }

        [Test]
        public void TestUnsuccessfulLoadFile()
        {
            IFactory factory = new TestingFactory(PASSWORD, IV, SALT);
            ActionList actionList = factory.GetActionList();
            IStorage storage = factory.GetStorage();

            AccountCollection collection = TestObjectBuilder.GetAccountCollection();
            string serialized = actionList.DoActions(collection);
            storage.StoreData("test", serialized);

            string fromStorage = storage.RetrieveData("test");

            IFactory factory2 = new TestingFactory("wrong", IV, SALT);
            ActionList actionList2 = factory2.GetActionList();

            try
            {
                AccountCollection deserialized = actionList2.ReverseActions<AccountCollection>(fromStorage);
            }
            catch (DeserializationException)
            {
                // Success
                return;
            }

            Assert.Fail("Exception should have been thrown because wrong password was used.");
        }
    }
}

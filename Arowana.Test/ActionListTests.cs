#region - Using Statements -

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Arowana.Actions;
using Arowana.Exceptions;
using Arowana.Factories;
using Arowana.Storage;

#endregion

namespace Arowana.Test
{
    [TestClass]
    public class ActionListTests
    {
        [TestMethod]
        public void TestSerializeAndDeserialize()
        {
            IFactory factory = new TestingFactory("realpassword");
            ActionList actionList = factory.GetActionList();
            IStorage storage = factory.GetStorage();

            AccountCollection collection = TestObjectBuilder.GetAccountCollection();
            string serialized = actionList.DoActions(collection);
            storage.StoreData("test", serialized);

            string fromStorage = storage.RetrieveData("test");
            AccountCollection deserialized = actionList.ReverseActions<AccountCollection>(fromStorage);
            Assert.AreEqual(collection, deserialized);
        }

        [TestMethod]
        public void TestUnsuccessfulLoadFile()
        {
            IFactory factory = new TestingFactory("realpassword");
            ActionList actionList = factory.GetActionList();
            IStorage storage = factory.GetStorage();

            AccountCollection collection = TestObjectBuilder.GetAccountCollection();
            string serialized = actionList.DoActions(collection);
            storage.StoreData("test", serialized);

            string fromStorage = storage.RetrieveData("test");

            IFactory factory2 = new TestingFactory("wrong");
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

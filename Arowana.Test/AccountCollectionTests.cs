#region - Using Statements -

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace Arowana.Test
{
    [TestClass]
    public class AccountCollectionTests
    {
        [TestMethod]
        public void TestEquality()
        {
            AccountCollection accountCol1 = TestObjectBuilder.GetAccountCollection();
            AccountCollection accountCol2 = TestObjectBuilder.GetAccountCollection();
            Assert.AreEqual(accountCol1, accountCol2);
        }

        [TestMethod]
        public void TestInequality()
        {
            AccountCollection accountCol1 = TestObjectBuilder.GetAccountCollection();
            AccountCollection accountCol2 = TestObjectBuilder.GetAccountCollection2();
            Assert.AreNotEqual(accountCol1, accountCol2);
        }

        [TestMethod]
        public void TestHashCodeEquality()
        {
            AccountCollection accountCol1 = TestObjectBuilder.GetAccountCollection();
            AccountCollection accountCol2 = TestObjectBuilder.GetAccountCollection();
            Assert.AreEqual(accountCol1.GetHashCode(), accountCol2.GetHashCode());
        }

        [TestMethod]
        public void TestHashCodeInequality()
        {
            AccountCollection accountCol1 = TestObjectBuilder.GetAccountCollection();
            AccountCollection accountCol2 = TestObjectBuilder.GetAccountCollection2();
            Assert.AreNotEqual(accountCol1.GetHashCode(), accountCol2.GetHashCode());
        }

        [TestMethod]
        public void TestGetHashCodeWithNoAccounts()
        {
            AccountCollection collection = TestObjectBuilder.GetAccountCollectionWithNoAccounts();
            try
            {
                int hashCode = collection.GetHashCode();
            }
            catch (Exception ex)
            {
                Assert.Fail("There was an exception when generating a hash code for a collection with no accounts:  " + ex.ToString());
            }
        }

        [TestMethod]
        public void TestEqualityWithNoAccounts()
        {
            AccountCollection coll1 = TestObjectBuilder.GetAccountCollectionWithNoAccounts();
            AccountCollection coll2 = TestObjectBuilder.GetAccountCollection();
            try
            {
                bool isEqual = coll1.Equals(coll2);
                isEqual = coll2.Equals(coll1);
            }
            catch (Exception ex)
            {
                Assert.Fail("An exception was thrown when checking equality with a collection with no accounts:  " + ex.ToString());
            }
        }
    }
}

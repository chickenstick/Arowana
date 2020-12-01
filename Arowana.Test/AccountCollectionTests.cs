#region - Using Statements -

using System;
using NUnit.Framework;

#endregion

namespace Arowana.Test
{
    [TestFixture]
    public class AccountCollectionTests
    {
        [Test]
        public void TestEquality()
        {
            AccountCollection accountCol1 = TestObjectBuilder.GetAccountCollection();
            AccountCollection accountCol2 = TestObjectBuilder.GetAccountCollection();
            Assert.AreEqual(accountCol1, accountCol2);
        }

        [Test]
        public void TestInequality()
        {
            AccountCollection accountCol1 = TestObjectBuilder.GetAccountCollection();
            AccountCollection accountCol2 = TestObjectBuilder.GetAccountCollection2();
            Assert.AreNotEqual(accountCol1, accountCol2);
        }

        [Test]
        public void TestHashCodeEquality()
        {
            AccountCollection accountCol1 = TestObjectBuilder.GetAccountCollection();
            AccountCollection accountCol2 = TestObjectBuilder.GetAccountCollection();
            Assert.AreEqual(accountCol1.GetHashCode(), accountCol2.GetHashCode());
        }

        [Test]
        public void TestHashCodeInequality()
        {
            AccountCollection accountCol1 = TestObjectBuilder.GetAccountCollection();
            AccountCollection accountCol2 = TestObjectBuilder.GetAccountCollection2();
            Assert.AreNotEqual(accountCol1.GetHashCode(), accountCol2.GetHashCode());
        }

        [Test]
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

        [Test]
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

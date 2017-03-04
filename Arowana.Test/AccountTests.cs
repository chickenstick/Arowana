#region - Using Statements -

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace Arowana.Test
{
    [TestClass]
    public class AccountTests
    {
        [TestMethod]
        public void TestEquality()
        {
            Account account1 = TestObjectBuilder.GetAccount1();
            Account account2 = TestObjectBuilder.GetAccount1();
            Assert.AreEqual(account1, account2);
        }

        [TestMethod]
        public void TestInequality()
        {
            Account account1 = TestObjectBuilder.GetAccount1();
            Account account2 = TestObjectBuilder.GetAccount2();
            Assert.AreNotEqual(account1, account2);
        }

        [TestMethod]
        public void TestHashCodeEquality()
        {
            Account account1 = TestObjectBuilder.GetAccount1();
            Account account2 = TestObjectBuilder.GetAccount1();
            Assert.AreEqual(account1.GetHashCode(), account2.GetHashCode());
        }

        [TestMethod]
        public void TestHashCodeInequality()
        {
            Account account1 = TestObjectBuilder.GetAccount1();
            Account account2 = TestObjectBuilder.GetAccount2();
            Assert.AreNotEqual(account1.GetHashCode(), account2.GetHashCode());
        }

        [TestMethod]
        public void TestGetHashCodeWithNullProperties()
        {
            Account account = TestObjectBuilder.GetAccountWithNullProperties();
            try
            {
                int hashCode = account.GetHashCode();
            }
            catch (Exception ex)
            {
                Assert.Fail("An exception was thrown when generating a hash code:  " + ex.ToString());
            }
        }

        [TestMethod]
        public void TestEqualityWithNullProperties()
        {
            Account account1 = TestObjectBuilder.GetAccountWithNullProperties();
            Account account2 = TestObjectBuilder.GetAccount1();
            try
            {
                bool isEqual = account1.Equals(account2);
                isEqual = account2.Equals(account1);
            }
            catch (Exception ex)
            {
                Assert.Fail("An exception was thrown when testing equality:  " + ex.ToString());
            }
        }
    }
}

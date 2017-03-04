#region - Using Statements -

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace Arowana.Test
{
    [TestClass]
    public class CredentialTests
    {
        [TestMethod]
        public void TestEquality()
        {
            Credential cred1 = TestObjectBuilder.GetCredential1();
            Credential cred2 = TestObjectBuilder.GetCredential1();
            Assert.AreEqual(cred1, cred2);
        }

        [TestMethod]
        public void TestInequality()
        {
            Credential cred1 = TestObjectBuilder.GetCredential1();
            Credential cred2 = TestObjectBuilder.GetCredential2();
            Assert.AreNotEqual(cred1, cred2);
        }

        [TestMethod]
        public void TestHashCodeEquality()
        {
            Credential cred1 = TestObjectBuilder.GetCredential1();
            Credential cred2 = TestObjectBuilder.GetCredential1();
            Assert.AreEqual(cred1.GetHashCode(), cred2.GetHashCode());
        }

        [TestMethod]
        public void TestHashCodeInequality()
        {
            Credential cred1 = TestObjectBuilder.GetCredential1();
            Credential cred2 = TestObjectBuilder.GetCredential2();
            Assert.AreNotEqual(cred1.GetHashCode(), cred2.GetHashCode());
        }

        [TestMethod]
        public void TestGetHashCodeWithNullProperties()
        {
            try
            {
                Credential cred = TestObjectBuilder.GetCredentialWithNullProperties();
                int hashCode = cred.GetHashCode();
            }
            catch (Exception ex)
            {
                Assert.Fail("An exception was thrown when generating a hash code:  " + ex.ToString());
            }
        }

        [TestMethod]
        public void TestEqualityCheckWithNullProperties()
        {
            Credential cred1 = TestObjectBuilder.GetCredentialWithNullProperties();
            Credential cred2 = TestObjectBuilder.GetCredential1();
            try
            {
                bool isEqual = cred1.Equals(cred2);
                isEqual = cred2.Equals(cred1);
            }
            catch (Exception ex)
            {
                Assert.Fail("An exception was thrown when testing equality:  " + ex.ToString());
            }
        }
    }
}

using NUnit.Framework;
using Rhino.Mocks;

namespace WebDAVSharp.Tests
{
    public class MockBasedTestBase
    {
        [SetUp]
        public void SetUp()
        {
            Mocks = new MockRepository();
        }

        [TearDown]
        public void TearDown()
        {
            Mocks.VerifyAll();
        }

        protected MockRepository Mocks
        {
            get;
            private set;
        }
    }
}
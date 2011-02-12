using System;
using NUnit.Framework;
using WebDAVSharp.Server.Adapters;
using WebDAVSharp.Server.Stores;
using WebDAVSharp.Tests;

namespace WebDAVSharp.Server.Tests
{
    [TestFixture]
    public class WebDAVServerTests : MockBasedTestBase
    {
        [Test]
        public void Constructor_NullListener_ThrowsArgumentNullException()
        {
            var store = Mocks.StrictMock<IWebDAVStore>();
            var logger = Mocks.StrictMock<ILogger>();
            Mocks.ReplayAll();

            Assert.Throws<ArgumentNullException>(() => new WebDAVServer(null, store, logger));
        }

        [Test]
        public void Constructor_NullStore_ThrowsArgumentNullException()
        {
            var listener = Mocks.StrictMock<IHttpListener>();
            var logger = Mocks.StrictMock<ILogger>();
            Mocks.ReplayAll();

            Assert.Throws<ArgumentNullException>(() => new WebDAVServer(listener, null, logger));
        }
    }
}
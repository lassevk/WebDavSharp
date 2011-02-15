using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using TypeMock.ArrangeActAssert;
using WebDAVSharp.Server.Adapters;
using WebDAVSharp.Server.MethodHandlers;
using WebDAVSharp.Server.Stores;

namespace WebDAVSharp.Server.Tests
{
    [TestFixture]
    public class WebDAVServerTests
    {
        private ILogger _Logger;
        private IWebDAVStore _Store;
        private IHttpListener _Listener;
        private List<IWebDAVMethodHandler> _MethodHandlers;

        [SetUp]
        public void SetUp()
        {
            _Logger = Isolate.Fake.Instance<ILogger>();
            _Store = Isolate.Fake.Instance<IWebDAVStore>();
            _Listener = Isolate.Fake.Instance<IHttpListener>();
            _MethodHandlers = WebDAVMethodHandlers.BuiltIn.ToList();
        }

        [Isolated]
        [Test]
        public void Constructor_NullStore_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new WebDAVServer(null, _Logger, _Listener, _MethodHandlers));
        }

        [Isolated]
        [Test]
        public void Constructor_EmptyVerbs_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new WebDAVServer(_Store, _Logger, _Listener, new List<IWebDAVMethodHandler>()));
        }

        [Isolated]
        [Test]
        public void Constructor_VerbsCollectionWithNullReferenceInside_ThrowsArgumentException()
        {
            var verbs = new List<IWebDAVMethodHandler>();
            verbs.Add(null);

            Assert.Throws<ArgumentException>(() => new WebDAVServer(_Store, _Logger, _Listener, verbs));
        }

        [Test]
        public void Start_WhenDisposed_ThrowsObjectDisposedException()
        {
            var server = new WebDAVServer(_Store, _Logger, _Listener, _MethodHandlers);
            server.Dispose();

            Assert.Throws<ObjectDisposedException>(() => server.Start());
        }

        [Test]
        public void Stop_WhenDisposed_ThrowsObjectDisposedException()
        {
            var server = new WebDAVServer(_Store, _Logger, _Listener, _MethodHandlers);
            server.Dispose();

            Assert.Throws<ObjectDisposedException>(() => server.Stop());
        }

        [Test]
        public void Start_WhenNotRunning_StartsServer()
        {
            using (var evt = new ManualResetEvent(false))
            {
                var server = new WebDAVServer(_Store, _Logger, _Listener, _MethodHandlers);
                Isolate.WhenCalled(() => _Listener.GetContext(null)).DoInstead(e =>
                {
                    evt.Set();
                    return null;
                });

                server.Start();

                evt.WaitOne(10000);
                server.Stop();

                Isolate.Verify.WasCalledWithAnyArguments(() => _Listener.GetContext(null));
            }
        }
    }
}
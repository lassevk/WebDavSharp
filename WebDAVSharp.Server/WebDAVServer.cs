using System;
using WebDAVSharp.Server.Adapters;
using WebDAVSharp.Server.Stores;

namespace WebDAVSharp.Server
{
    /// <summary>
    /// This class implements the core WebDAV server.
    /// </summary>
    public class WebDAVServer : WebDAVDisposableBase
    {
        private readonly IHttpListener _Listener;
        private readonly IWebDAVStore _Store;
        private readonly ILogger _Logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebDAVServer"/> class.
        /// </summary>
        /// <param name="listener">
        /// The <see cref="IHttpListener"/> object that will handle the web server portion of
        /// the WebDAV server.
        /// </param>
        /// <param name="store">
        /// The <see cref="IWebDAVStore"/> store object that will provide
        /// collections and documents for this <see cref="WebDAVServer"/>.
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/> object that log messages will be sent to.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="listener"/> is <c>null</c>.</para>
        /// <para>- or -</para>
        /// <para><paramref name="store"/> is <c>null</c>.</para>
        /// </exception>
        public WebDAVServer(IHttpListener listener, IWebDAVStore store, ILogger logger)
        {
            if (listener == null)
                throw new ArgumentNullException("listener");
            if (store == null)
                throw new ArgumentNullException("store");

            _Listener = listener;
            _Store = store;
            _Logger = logger ?? new VoidLogger();
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            // close listener
        }
    }
}
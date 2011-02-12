using System.Net;

namespace WebDAVSharp.Server.Adapters
{
    /// <summary>
    /// This <see cref="IHttpListener"/> implementation wraps around a
    /// <see cref="HttpListener"/> instance.
    /// </summary>
    public sealed class HttpListenerAdapter : WebDAVDisposableBase, IHttpListener
    {
        private readonly HttpListener _Listener;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpListenerAdapter"/> class.
        /// </summary>
        public HttpListenerAdapter()
        {
            _Listener = new HttpListener();
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (_Listener.IsListening)
                _Listener.Close();
        }
    }
}
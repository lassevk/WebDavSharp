using System;
using System.Net;

namespace WebDAVSharp.Server.Adapters
{
    /// <summary>
    /// This <see cref="IHttpListenerContext"/> implementation wraps around a
    /// <see cref="HttpListenerContext"/> instance.
    /// </summary>
    public sealed class HttpListenerContextAdapter : IHttpListenerContext, IAdapter<HttpListenerContext>
    {
        private readonly HttpListenerContext _Context;
        private readonly HttpListenerRequestAdapter _Request;
        private readonly HttpListenerResponseAdapter _Response;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpListenerContextAdapter"/> class.
        /// </summary>
        /// <param name="context">
        /// The <see cref="HttpListenerContext"/> to adapt for WebDAV#.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="context"/> is <c>null</c>.</para>
        /// </exception>
        public HttpListenerContextAdapter(HttpListenerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            _Context = context;
            _Request = new HttpListenerRequestAdapter(context.Request);
            _Response = new HttpListenerResponseAdapter(context.Response);
        }

        /// <summary>
        /// Gets the internal instance that was adapted for WebDAV#.
        /// </summary>
        public HttpListenerContext AdaptedInstance
        {
            get
            {
                return _Context;
            }
        }

        /// <summary>
        /// Gets the <see cref="IHttpListenerRequest"/> request adapter.
        /// </summary>
        public IHttpListenerRequest Request
        {
            get
            {
                return _Request;
            }
        }

        /// <summary>
        /// Gets the <see cref="IHttpListenerResponse"/> response adapter.
        /// </summary>
        public IHttpListenerResponse Response
        {
            get
            {
                return _Response;
            }
        }
    }
}
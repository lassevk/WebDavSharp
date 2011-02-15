using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;

namespace WebDAVSharp.Server.Adapters
{
    /// <summary>
    /// This <see cref="IHttpListenerRequest"/> implementation wraps around a
    /// <see cref="HttpListenerRequest"/> instance.
    /// </summary>
    public sealed class HttpListenerRequestAdapter : IHttpListenerRequest
    {
        private readonly HttpListenerRequest _Request;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpListenerRequestAdapter"/> class.
        /// </summary>
        /// <param name="request">
        /// The <see cref="HttpListenerRequest"/> to adapt for WebDAV#.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="request"/> is <c>null</c>.</para>
        /// </exception>
        public HttpListenerRequestAdapter(HttpListenerRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            _Request = request;
        }

        /// <summary>
        /// Gets the internal instance that was adapted for WebDAV#.
        /// </summary>
        public HttpListenerRequest AdaptedInstance
        {
            get
            {
                return _Request;
            }
        }

        /// <summary>
        /// Gets the client IP address and port number from which the request originated.
        /// </summary>
        public IPEndPoint RemoteEndPoint
        {
            get
            {
                return _Request.RemoteEndPoint;
            }
        }

        /// <summary>
        /// Gets the <see cref="Uri"/> object requested by the client.
        /// </summary>
        public Uri Url
        {
            get
            {
                return _Request.Url;
            }
        }

        /// <summary>
        /// Gets the HTTP method specified by the client.
        /// </summary>
        public string HttpMethod
        {
            get
            {
                return _Request.HttpMethod;
            }
        }

        /// <summary>
        /// Gets the collection of header name/value pairs sent in the request.
        /// </summary>
        public NameValueCollection Headers
        {
            get
            {
                return _Request.Headers;
            }
        }

        /// <summary>
        /// Gets a <see cref="Stream"/> that contains the body data sent by the client.
        /// </summary>
        public Stream InputStream
        {
            get
            {
                return _Request.InputStream;
            }
        }

        /// <summary>
        /// Gets the content <see cref="Encoding"/> that can be used with data sent with the request.
        /// </summary>
        public Encoding ContentEncoding
        {
            get
            {
                return _Request.ContentEncoding;
            }
        }

        /// <summary>
        /// Gets the length of the body data included in the request.
        /// </summary>
        public long ContentLength64
        {
            get
            {
                return _Request.ContentLength64;
            }
        }
    }
}
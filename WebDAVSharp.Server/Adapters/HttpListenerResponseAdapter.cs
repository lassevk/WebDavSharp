using System;
using System.IO;
using System.Net;
using System.Text;

namespace WebDAVSharp.Server.Adapters
{
    /// <summary>
    /// This <see cref="IHttpListenerResponse"/> implementation wraps around a
    /// <see cref="HttpListenerResponse"/> instance.
    /// </summary>
    public sealed class HttpListenerResponseAdapter : IHttpListenerResponse
    {
        private readonly HttpListenerResponse _Response;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpListenerResponseAdapter"/> class.
        /// </summary>
        /// <param name="Response">
        /// The <see cref="HttpListenerResponse"/> to adapt for WebDAV#.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="Response"/> is <c>null</c>.</para>
        /// </exception>
        public HttpListenerResponseAdapter(HttpListenerResponse Response)
        {
            if (Response == null)
                throw new ArgumentNullException("Response");

            _Response = Response;
        }

        /// <summary>
        /// Gets the internal instance that was adapted for WebDAV#.
        /// </summary>
        public HttpListenerResponse AdaptedInstance
        {
            get
            {
                return _Response;
            }
        }

        /// <summary>
        /// Gets or sets the HTTP status code to be returned to the client.
        /// </summary>
        public int StatusCode
        {
            get
            {
                return _Response.StatusCode;
            }

            set
            {
                _Response.StatusCode = value;
            }
        }

        /// <summary>
        /// Gets or sets a text description of the HTTP <see cref="StatusCode">status code</see> returned to the client.
        /// </summary>
        public string StatusDescription
        {
            get
            {
                return _Response.StatusDescription;
            }

            set
            {
                _Response.StatusDescription = value ?? string.Empty;
            }
        }

        /// <summary>
        /// Gets a <see cref="Stream"/> object to which a response can be written.
        /// </summary>
        public Stream OutputStream
        {
            get
            {
                return _Response.OutputStream;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Encoding"/> for this response's <see cref="OutputStream"/>.
        /// </summary>
        public Encoding ContentEncoding
        {
            get
            {
                return _Response.ContentEncoding;
            }

            set
            {
                _Response.ContentEncoding = value;
            }
        }

        /// <summary>
        /// Gets or sets the number of bytes in the body data included in the response.
        /// </summary>
        public long ContentLength64
        {
            get
            {
                return _Response.ContentLength64;
            }

            set
            {
                _Response.ContentLength64 = value;
            }
        }

        /// <summary>
        /// Sends the response to the client and releases the resources held by the adapted
        /// <see cref="HttpListenerResponse"/> instance.
        /// </summary>
        public void Close()
        {
            _Response.Close();
        }

        /// <summary>
        /// Appends a value to the specified HTTP header to be sent with the response.
        /// </summary>
        /// <param name="name">
        /// The name of the HTTP header to append the <paramref name="value"/> to.
        /// </param>
        /// <param name="value">
        /// The value to append to the <paramref name="name"/> header.
        /// </param>
        public void AppendHeader(string name, string value)
        {
            _Response.AppendHeader(name, value);
        }
    }
}
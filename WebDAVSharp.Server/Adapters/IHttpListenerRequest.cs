﻿using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;

namespace WebDAVSharp.Server.Adapters
{
    /// <summary>
    /// This is an interface-version of the parts of <see cref="HttpListenerRequest"/> that
    /// the <see cref="WebDAVServer"/> requires to operator.
    /// </summary>
    /// <remarks>
    /// The main purpose of this interface is to facilitate unit-testing.
    /// </remarks>
    public interface IHttpListenerRequest : IAdapter<HttpListenerRequest>
    {
        /// <summary>
        /// Gets the client IP address and port number from which the request originated.
        /// </summary>
        IPEndPoint RemoteEndPoint
        {
            get;
        }

        /// <summary>
        /// Gets the <see cref="Uri"/> object requested by the client.
        /// </summary>
        Uri Url
        {
            get;
        }

        /// <summary>
        /// Gets the HTTP method specified by the client.
        /// </summary>
        string HttpMethod
        {
            get;
        }

        /// <summary>
        /// Gets the collection of header name/value pairs sent in the request.
        /// </summary>
        NameValueCollection Headers
        {
            get;
        }

        /// <summary>
        /// Gets a <see cref="Stream"/> that contains the body data sent by the client.
        /// </summary>
        Stream InputStream
        {
            get;
        }

        /// <summary>
        /// Gets the content <see cref="Encoding"/> that can be used with data sent with the request.
        /// </summary>
        Encoding ContentEncoding
        {
            get;
        }

        /// <summary>
        /// Gets the length of the body data included in the request.
        /// </summary>
        long ContentLength64
        {
            get;
        }
    }
}
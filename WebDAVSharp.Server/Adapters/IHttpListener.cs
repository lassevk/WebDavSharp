using System;
using System.Net;

namespace WebDAVSharp.Server.Adapters
{
    /// <summary>
    /// This is an interface-version of the parts of <see cref="HttpListener"/> that
    /// the <see cref="WebDAVServer"/> requires to operator.
    /// </summary>
    /// <remarks>
    /// The main purpose of this interface is to facilitate unit-testing.
    /// </remarks>
    public interface IHttpListener : IDisposable
    {
    }
}
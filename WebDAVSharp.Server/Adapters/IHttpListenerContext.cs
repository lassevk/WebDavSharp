using System.Net;

namespace WebDAVSharp.Server.Adapters
{
    /// <summary>
    /// This is an interface-version of the parts of <see cref="HttpListenerContext"/> that
    /// the <see cref="WebDAVServer"/> requires to operator.
    /// </summary>
    /// <remarks>
    /// The main purpose of this interface is to facilitate unit-testing.
    /// </remarks>
    public interface IHttpListenerContext : IAdapter<HttpListenerContext>
    {
        /// <summary>
        /// Gets the <see cref="IHttpListenerRequest"/> request adapter.
        /// </summary>
        IHttpListenerRequest Request
        {
            get;
        }

        /// <summary>
        /// Gets the <see cref="IHttpListenerResponse"/> response adapter.
        /// </summary>
        IHttpListenerResponse Response
        {
            get;
        }
    }
}
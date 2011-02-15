using System.Collections.Generic;
using WebDAVSharp.Server.Adapters;
using WebDAVSharp.Server.Stores;

namespace WebDAVSharp.Server.MethodHandlers
{
    /// <summary>
    /// This interface must be implemented by a class that will respond
    /// to requests from a client by handling specific HTTP methods.
    /// </summary>
    public interface IWebDAVMethodHandler
    {
        /// <summary>
        /// Gets the collection of the names of the HTTP methods handled by this instance.
        /// </summary>
        IEnumerable<string> Names
        {
            get;
        }

        /// <summary>
        /// Processes the request.
        /// </summary>
        /// <param name="server">
        /// The <see cref="WebDAVServer"/> through which the request came in from the client.
        /// </param>
        /// <param name="context">
        /// The <see cref="IHttpListenerContext"/> object containing both the request and response
        /// objects to use.
        /// </param>
        /// <param name="store">
        /// The <see cref="IWebDAVStore"/> that the <see cref="WebDAVServer"/> is hosting.
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/> to log to.
        /// </param>
        void ProcessRequest(WebDAVServer server, IHttpListenerContext context, IWebDAVStore store, ILogger logger);
    }
}
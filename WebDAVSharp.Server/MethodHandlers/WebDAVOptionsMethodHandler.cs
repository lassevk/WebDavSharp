using System;
using System.Collections.Generic;
using WebDAVSharp.Server.Adapters;
using WebDAVSharp.Server.Stores;

namespace WebDAVSharp.Server.MethodHandlers
{
    /// <summary>
    /// This class implements the <c>OPTIONS</c> HTTP method for WebDAV#.
    /// </summary>
    public class WebDAVOptionsMethodHandler : IWebDAVMethodHandler
    {
        /// <summary>
        /// Gets the collection of the names of the HTTP methods handled by this instance.
        /// </summary>
        public string[] Names
        {
            get
            {
                return new[]
                {
                    "OPTIONS",
                };
            }
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
        public void ProcessRequest(WebDAVServer server, IHttpListenerContext context, IWebDAVStore store, ILogger logger)
        {
            var item = context.Request.Url.GetItem(server, store);
            var verbsAllowed = new List<string> { "OPTIONS" };

            foreach (var verb in verbsAllowed)
                context.Response.AppendHeader("Allow", verb);

            context.SendSimpleResponse(HttpStatusCodes.Successful.OK);
        }
    }
}

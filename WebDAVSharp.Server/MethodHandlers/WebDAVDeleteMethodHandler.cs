using System;
using System.Linq;
using WebDAVSharp.Server.Adapters;
using WebDAVSharp.Server.Exceptions;
using WebDAVSharp.Server.Stores;

namespace WebDAVSharp.Server.MethodHandlers
{
    /// <summary>
    /// This class implements the <c>DELETE</c> HTTP method for WebDAV#.
    /// </summary>
    public class WebDAVDeleteMethodHandler : IWebDAVMethodHandler
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
                    "DELETE",
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
            Uri parentCollectionUri = context.Request.Url.GetParentUri();
            var collection = parentCollectionUri.GetItem(server, store) as IWebDAVStoreCollection;
            if (collection == null)
                throw new HttpConflictException();

            IWebDAVStoreItem item = collection.GetItemByName(context.Request.Url.Segments.Last().TrimEnd('/', '\\'));
            if (item == null)
                throw new HttpNotFoundException();

            collection.Delete(item);
            context.SendSimpleResponse();
        }
    }
}
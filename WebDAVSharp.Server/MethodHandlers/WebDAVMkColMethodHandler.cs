using System;
using System.Linq;
using WebDAVSharp.Server.Adapters;
using WebDAVSharp.Server.Exceptions;
using WebDAVSharp.Server.Stores;

namespace WebDAVSharp.Server.MethodHandlers
{
    /// <summary>
    /// This class implements the <c>MKCOL</c> HTTP method for WebDAV#.
    /// </summary>
    public class WebDAVMkColMethodHandler : IWebDAVMethodHandler
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
                    "MKCOL",
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
            if (context.Request.ContentLength64 > 0)
                throw new HttpException(HttpStatusCodes.ClientErrors.UnsupportedMediaType);

            Uri uri = context.Request.Url.GetParentUri();
            var collection = uri.GetItem(server, store) as IWebDAVStoreCollection;
            if (collection == null)
                throw new HttpNotFoundException();

            string collectionName = context.Request.Url.Segments.Last().TrimEnd('/', '\\');
            if (collection.GetItemByName(collectionName) != null)
                throw new HttpException(HttpStatusCodes.ClientErrors.MethodNotAllowed);

            collection.CreateCollection(collectionName);

            context.SendSimpleResponse(HttpStatusCodes.Successful.OK);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using WebDAVSharp.Server.Adapters;
using WebDAVSharp.Server.Exceptions;
using WebDAVSharp.Server.Stores;

namespace WebDAVSharp.Server.MethodHandlers
{
    /// <summary>
    /// This class implements the <c>COPY</c> HTTP method for WebDAV#.
    /// </summary>
    public class WebDAVCopyMethodHandler : IWebDAVMethodHandler
    {
        /// <summary>
        /// Gets the collection of the names of the HTTP methods handled by this instance.
        /// </summary>
        public IEnumerable<string> Names
        {
            get
            {
                return new[]
                {
                    "COPY"
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
            IWebDAVStoreItem source = context.Request.Url.GetItem(server, store);
            if (source is IWebDAVStoreDocument || source is IWebDAVStoreCollection)
                CopyItem(server, context, store, source);
            else
                throw new HttpMethodNotAllowedException();
        }

        private void CopyItem(WebDAVServer server, IHttpListenerContext context, IWebDAVStore store, IWebDAVStoreItem source)
        {
            var destinationUri = new Uri(context.Request.Headers["Destination"]);
            var destinationParentCollection = destinationUri.GetParentUri().GetItem(server, store) as IWebDAVStoreCollection;
            if (destinationParentCollection == null)
                throw new HttpConflictException();

            bool isNew = true;

            string destinationName = destinationUri.Segments.Last().TrimEnd('/', '\\');
            IWebDAVStoreItem destination = destinationParentCollection.GetItemByName(destinationName);

            if (source == destination)
                throw new HttpConflictException();

            if (destination != null)
            {
                if (context.Request.Headers["Overwrite"] == "F")
                    throw new HttpException(HttpStatusCodes.ClientErrors.PreconditionFailed);
                if (destination is IWebDAVStoreCollection)
                    destinationParentCollection.Delete(destination);
                isNew = false;
            }

            destinationParentCollection.CopyItemHere(source, destinationName);

            if (isNew)
                context.SendSimpleResponse(HttpStatusCodes.Successful.Created);
            else
                context.SendSimpleResponse(HttpStatusCodes.Successful.NoContent);
        }
    }
}
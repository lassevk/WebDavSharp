using System;
using System.Collections.Generic;
using System.Linq;
using WebDAVSharp.Server.Adapters;
using WebDAVSharp.Server.Exceptions;
using WebDAVSharp.Server.Stores;

namespace WebDAVSharp.Server.MethodHandlers
{
    /// <summary>
    /// This class implements the <c>MOVE</c> HTTP method for WebDAV#.
    /// </summary>
    public class WebDAVMoveHttpHandler : IWebDAVMethodHandler
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
                    "MOVE",
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
            var source = context.Request.Url.GetItem(server, store);

            var document = source as IWebDAVStoreDocument;
            if (document != null)
            {
                MoveDocument(server, context, store, document);
                return;
            }

            var collection = source as IWebDAVStoreCollection;
            if (collection != null)
            {
                MoveCollection(server, context, store, collection);
                return;
            }

            throw new HttpMethodNotAllowedException();
        }

        private void MoveDocument(WebDAVServer server, IHttpListenerContext context, IWebDAVStore store, IWebDAVStoreDocument source)
        {
            var destinationUri = new Uri(context.Request.Headers["Destination"]);
            var destinationCollection = destinationUri.GetParentUri().GetItem(server, store) as IWebDAVStoreCollection;
            if (destinationCollection == null)
                throw new HttpConflictException();

            bool isNew = true;

            string destinationName = destinationUri.Segments.Last().TrimEnd('/', '\\');
            var destination = destinationCollection.GetItemByName(destinationName);
            if (destination != null)
            {
                if (context.Request.Headers["Overwrite"] == "F")
                    throw new HttpException(HttpStatusCodes.ClientErrors.PreconditionFailed);
                if (destination is IWebDAVStoreCollection)
                    destinationCollection.Delete(destination);
                isNew = false;
            }

            destinationCollection.MoveItemHere(source, destinationName);

            if (isNew)
                context.SendSimpleResponse(HttpStatusCodes.Successful.Created);
            else
                context.SendSimpleResponse(HttpStatusCodes.Successful.NoContent);
        }

        private void MoveCollection(WebDAVServer server, IHttpListenerContext context, IWebDAVStore store, IWebDAVStoreCollection source)
        {
            var destinationUri = new Uri(context.Request.Headers["Destination"]);
            var destinationParentCollection = destinationUri.GetParentUri().GetItem(server, store) as IWebDAVStoreCollection;
            if (destinationParentCollection == null)
                throw new HttpConflictException();

            string destinationName = destinationUri.Segments.Last().TrimEnd('/', '\\');
            var destination = destinationParentCollection.GetItemByName(destinationName);

            if (destination != null)
            {
                if (context.Request.Headers["Overwrite"] == "F")
                    throw new HttpException(HttpStatusCodes.ClientErrors.PreconditionFailed);

                destinationParentCollection.Delete(destination);
            }

            destinationParentCollection.MoveItemHere(source, destinationName);

            context.SendSimpleResponse(HttpStatusCodes.Successful.Created);
        }
    }
}

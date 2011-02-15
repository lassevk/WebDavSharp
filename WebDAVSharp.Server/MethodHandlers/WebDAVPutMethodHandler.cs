using System;
using System.Linq;
using WebDAVSharp.Server.Adapters;
using WebDAVSharp.Server.Exceptions;
using WebDAVSharp.Server.Stores;

namespace WebDAVSharp.Server.MethodHandlers
{
    /// <summary>
    /// This class implements the <c>PUT</c> HTTP method for WebDAV#.
    /// </summary>
    public class WebDAVPutMethodHandler : IWebDAVMethodHandler
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
                    "PUT",
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
            var parentCollection = context.Request.Url.GetParentUri().GetItem(server, store) as IWebDAVStoreCollection;
            if (parentCollection == null)
                throw new HttpConflictException();

            string itemName = context.Request.Url.Segments.Last().TrimEnd('/', '\\');
            var item = parentCollection.GetItemByName(itemName);
            IWebDAVStoreDocument doc;
            if (item != null)
            {
                doc = item as IWebDAVStoreDocument;
                if (doc == null)
                    throw new HttpMethodNotAllowedException();
            }
            else
            {
                doc = parentCollection.CreateDocument(itemName);
            }

            if (context.Request.ContentLength64 < 0)
                throw new HttpException(HttpStatusCodes.ClientErrors.LengthRequired);

            using (var stream = doc.OpenWriteStream(false))
            {
                long left = context.Request.ContentLength64;
                byte[] buffer = new byte[4096];
                while (left > 0)
                {
                    int toRead = Convert.ToInt32(Math.Min(left, buffer.Length));
                    int inBuffer = context.Request.InputStream.Read(buffer, 0, toRead);
                    stream.Write(buffer, 0, inBuffer);

                    left -= inBuffer;
                }
            }

            context.SendSimpleResponse(HttpStatusCodes.Successful.Created);
        }
    }
}

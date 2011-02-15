using System.IO;
using WebDAVSharp.Server.Adapters;
using WebDAVSharp.Server.Exceptions;
using WebDAVSharp.Server.Stores;

namespace WebDAVSharp.Server.MethodHandlers
{
    /// <summary>
    /// This class implements the <c>GET</c> HTTP method for WebDAV#.
    /// </summary>
    public sealed class WebDAVGetMethodHandler : IWebDAVMethodHandler
    {
        /// <summary>
        /// Gets the collection of the names of the verbs handled by this instance.
        /// </summary>
        public string[] Names
        {
            get
            {
                return new[]
                {
                    "GET"
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
        /// <exception cref="HttpNotFoundException">
        /// <para><paramref name="context"/> specifies a request for a store item that does not exist.</para>
        /// <para>- or -</para>
        /// <para><paramref name="context"/> specifies a request for a store item that is not a document.</para>
        /// </exception>
        /// <exception cref="HttpConflictException">
        /// <para><paramref name="context"/> specifies a request for a store item using a collection path that does not exist.</para>
        /// </exception>
        public void ProcessRequest(WebDAVServer server, IHttpListenerContext context, IWebDAVStore store, ILogger logger)
        {
            var doc = context.Request.Url.GetItem(server, store) as IWebDAVStoreDocument;
            if (doc == null)
                throw new HttpNotFoundException();

            long docSize = doc.Size;
            if (docSize == 0)
            {
                context.Response.StatusCode = HttpStatusCodes.Successful.OK;
                context.Response.ContentLength64 = 0;
                context.Response.Close();
            }

            using (Stream stream = doc.OpenReadStream())
            {
                context.Response.StatusCode = HttpStatusCodes.Successful.OK;

                if (docSize > 0)
                    context.Response.ContentLength64 = docSize;

                var buffer = new byte[4096];
                int inBuffer;
                while ((inBuffer = stream.Read(buffer, 0, buffer.Length)) > 0)
                    context.Response.OutputStream.Write(buffer, 0, inBuffer);
            }
            context.Response.Close();
        }
    }
}
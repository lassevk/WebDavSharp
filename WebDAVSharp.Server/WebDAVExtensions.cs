using System;
using WebDAVSharp.Server.Adapters;
using WebDAVSharp.Server.Exceptions;
using WebDAVSharp.Server.Stores;

namespace WebDAVSharp.Server
{
    /// <summary>
    /// This class holds extension methods for various types related to WebDAV#.
    /// </summary>
    public static class WebDAVExtensions
    {
        /// <summary>
        /// Gets the Uri to the parent object.
        /// </summary>
        /// <param name="uri">
        /// The <see cref="Uri"/> of a resource, for which the parent Uri should be retrieved.
        /// </param>
        /// <returns>
        /// The parent <see cref="Uri"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="uri"/> is <c>null</c>.</para>
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <para><paramref name="uri"/> has no parent, it refers to a root resource.</para>
        /// </exception>
        public static Uri GetParentUri(this Uri uri)
        {
            if (uri == null)
                throw new ArgumentNullException("uri");
            if (uri.Segments.Length == 1)
                throw new InvalidOperationException("Cannot get parent of root");

            string url = uri.ToString();
            int index = url.Length - 1;
            if (url[index] == '/')
                index--;
            while (url[index] != '/')
                index--;
            return new Uri(url.Substring(0, index + 1));
        }

        /// <summary>
        /// Sends a simple response with a specified HTTP status code but no content.
        /// </summary>
        /// <param name="context">
        /// The <see cref="IHttpListenerContext"/> to send the response through.
        /// </param>
        /// <param name="statusCode">
        /// The HTTP status code for the response.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="context"/> is <c>null</c>.</para>
        /// </exception>
        public static void SendSimpleResponse(this IHttpListenerContext context, int statusCode = HttpStatusCodes.Successful.OK)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            context.Response.StatusCode = statusCode;
            context.Response.StatusDescription = HttpStatusCodes.GetDescription(statusCode);
            context.Response.Close();
        }

        /// <summary>
        /// Gets the prefix <see cref="Uri"/> that matches the specified <see cref="Uri"/>.
        /// </summary>
        /// <param name="uri">
        /// The <see cref="Uri"/> to find the most specific prefix <see cref="Uri"/> for.
        /// </param>
        /// <param name="server">
        /// The <see cref="WebDAVServer"/> that hosts the WebDAV server and holds the collection
        /// of known prefixes.
        /// </param>
        /// <returns>
        /// The most specific <see cref="Uri"/> for the given <paramref name="uri"/>.
        /// </returns>
        /// <exception cref="HttpInternalServerException">
        /// <paramref name="uri"/> specifies a <see cref="Uri"/> that is not known to the <paramref name="server"/>.
        /// </exception>
        public static Uri GetPrefixUri(this Uri uri, WebDAVServer server)
        {
            string url = uri.ToString();
            foreach (string prefix in server.Listener.Prefixes)
            {
                if (url.StartsWith(uri.ToString()))
                {
                    return new Uri(prefix);
                }
            }

            throw new HttpInternalServerException("Unable to find correct server root");
        }

        /// <summary>
        /// Retrieves a store item through the specified <see cref="Uri"/> from the
        /// specified <see cref="WebDAVServer"/> and <see cref="IWebDAVStore"/>.
        /// </summary>
        /// <param name="uri">
        /// The <see cref="Uri"/> to retrieve the store item for.
        /// </param>
        /// <param name="server">
        /// The <see cref="WebDAVServer"/> that hosts the <paramref name="store"/>.
        /// </param>
        /// <param name="store">
        /// The <see cref="IWebDAVStore"/> from which to retrieve the store item.
        /// </param>
        /// <returns>
        /// The retrieved store item.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="uri"/> is <c>null</c>.</para>
        /// <para><paramref name="server"/> is <c>null</c>.</para>
        /// <para><paramref name="store"/> is <c>null</c>.</para>
        /// </exception>
        /// <exception cref="HttpConflictException">
        /// <para><paramref name="uri"/> refers to a document in a collection, where the collection does not exist.</para>
        /// </exception>
        /// <exception cref="HttpNotFoundException">
        /// <para><paramref name="uri"/> refers to a document that does not exist.</para>
        /// </exception>
        public static IWebDAVStoreItem GetItem(this Uri uri, WebDAVServer server, IWebDAVStore store)
        {
            if (uri == null)
                throw new ArgumentNullException("uri");
            if (server == null)
                throw new ArgumentNullException("server");
            if (store == null)
                throw new ArgumentNullException("store");

            Uri prefixUri = uri.GetPrefixUri(server);
            IWebDAVStoreCollection collection = store.Root;

            IWebDAVStoreItem item = null;
            if (prefixUri.Segments.Length == uri.Segments.Length)
                return collection;

            for (int index = prefixUri.Segments.Length; index < uri.Segments.Length; index++)
            {
                string segmentName = uri.Segments[index];
                IWebDAVStoreItem nextItem = collection.GetItemByName(segmentName.TrimEnd('/', '\\'));
                if (nextItem == null)
                    throw new HttpConflictException();

                if (index == uri.Segments.Length - 1)
                    item = nextItem;
                else
                {
                    collection = nextItem as IWebDAVStoreCollection;
                    if (collection == null)
                        throw new HttpNotFoundException();
                }
            }

            if (item == null)
                throw new HttpNotFoundException();
            return item;
        }
    }
}
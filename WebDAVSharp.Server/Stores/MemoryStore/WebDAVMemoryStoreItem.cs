using System;
using WebDAVSharp.Server.Stores.BaseClasses;

namespace WebDAVSharp.Server.Stores.MemoryStore
{
    /// <summary>
    /// This class implements a memory-based <see cref="IWebDAVStoreItem"/>.
    /// </summary>
    public class WebDAVMemoryStoreItem : WebDAVStoreItemBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebDAVMemoryStoreItem"/> class.
        /// </summary>
        /// <param name="parentCollection">The parent <see cref="IWebDAVStoreCollection"/> that contains this <see cref="IWebDAVStoreItem"/> implementation.</param>
        /// <param name="name">The name of this <see cref="IWebDAVStoreItem"/></param>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> is <c>null</c>.</exception>
        public WebDAVMemoryStoreItem(IWebDAVStoreCollection parentCollection, string name)
            : base(parentCollection, name)
        {
            // Do nothing here
        }
    }
}
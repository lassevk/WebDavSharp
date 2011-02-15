using WebDAVSharp.Server.Stores.BaseClasses;

namespace WebDAVSharp.Server.Stores.MemoryStore
{
    /// <summary>
    /// This class implements a memory-based <see cref="IWebDAVStore"/>.
    /// </summary>
    public class WebDAVMemoryStore : WebDAVStoreBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebDAVMemoryStore"/> class.
        /// </summary>
        public WebDAVMemoryStore()
            : base(new WebDAVMemoryStoreCollection(null, "/"))
        {
            // Do nothing here
        }
    }
}
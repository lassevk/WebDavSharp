namespace WebDAVSharp.Server.Stores
{
    /// <summary>
    /// This interface must be implemented by classes that will function as a store item,
    /// which is either a document (<see cref="IWebDAVStoreDocument"/>) or a
    /// collection of documents (<see cref="IWebDAVStoreCollection"/>.)
    /// </summary>
    public interface IWebDAVStoreItem
    {
        /// <summary>
        /// Gets the parent <see cref="IWebDAVStoreCollection"/> that owns this <see cref="IWebDAVStoreItem"/>.
        /// </summary>
        IWebDAVStoreCollection ParentCollection
        {
            get;
        }

        /// <summary>
        /// Gets or sets the name of this <see cref="IWebDAVStoreItem"/>.
        /// </summary>
        string Name
        {
            get;
            set;
        }
    }
}
namespace WebDAVSharp.Server.Stores
{
    /// <summary>
    /// This interface must be implemented by classes that serve as stores of collections and
    /// documents for the <see cref="WebDAVServer"/>.
    /// </summary>
    public interface IWebDAVStore
    {
        /// <summary>
        /// Gets the root collection of this <see cref="IWebDAVStore"/>.
        /// </summary>
        IWebDAVStoreCollection Root
        {
            get;
        }
    }
}
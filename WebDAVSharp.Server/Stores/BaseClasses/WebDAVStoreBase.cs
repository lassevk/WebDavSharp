using System;

namespace WebDAVSharp.Server.Stores.BaseClasses
{
    /// <summary>
    /// This class is a base class for <see cref="IWebDAVStore"/> implementations.
    /// </summary>
    public abstract class WebDAVStoreBase : IWebDAVStore
    {
        private readonly IWebDAVStoreCollection _Root;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebDAVStoreBase"/> class.
        /// </summary>
        /// <param name="root">
        /// The root <see cref="IWebDAVStoreCollection"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="root"/> is <c>null</c>.</para>
        /// </exception>
        protected WebDAVStoreBase(IWebDAVStoreCollection root)
        {
            if (root == null)
                throw new ArgumentNullException("root");

            _Root = root;
        }

        #region IWebDAVStore Members

        /// <summary>
        /// Gets the root collection of this <see cref="IWebDAVStore"/>.
        /// </summary>
        public IWebDAVStoreCollection Root
        {
            get
            {
                return _Root;
            }
        }

        #endregion
    }
}
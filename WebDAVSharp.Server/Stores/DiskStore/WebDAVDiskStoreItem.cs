using System;
using System.IO;

namespace WebDAVSharp.Server.Stores.DiskStore
{
    /// <summary>
    /// This class implements a disk-based <see cref="IWebDAVStoreItem"/> which can be either
    /// a folder on disk (<see cref="WebDAVDiskStoreCollection"/>) or a file on disk
    /// (<see cref="WebDAVDiskStoreDocument"/>).
    /// </summary>
    public class WebDAVDiskStoreItem : IWebDAVStoreItem
    {
        private readonly WebDAVDiskStoreCollection _ParentCollection;
        private readonly string _Path;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebDAVDiskStoreItem"/> class.
        /// </summary>
        /// <param name="parentCollection">
        /// The parent <see cref="WebDAVDiskStoreCollection"/> that contains this <see cref="WebDAVDiskStoreItem"/>;
        /// or <c>null</c> if this is the root <see cref="WebDAVDiskStoreCollection"/>.
        /// </param>
        /// <param name="path">
        /// The path that this <see cref="WebDAVDiskStoreItem"/> maps to.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="path"/> is <c>null</c> or empty.</para>
        /// </exception>
        protected WebDAVDiskStoreItem(WebDAVDiskStoreCollection parentCollection, string path)
        {
            if (StringEx.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException("path");

            _ParentCollection = parentCollection;
            _Path = path;
        }

        /// <summary>
        /// Gets the path to this <see cref="WebDAVDiskStoreItem"/>.
        /// </summary>
        public string ItemPath
        {
            get
            {
                return _Path;
            }
        }

        #region IWebDAVStoreItem Members

        /// <summary>
        /// Gets the parent <see cref="IWebDAVStoreCollection"/> that owns this <see cref="IWebDAVStoreItem"/>.
        /// </summary>
        public IWebDAVStoreCollection ParentCollection
        {
            get
            {
                return _ParentCollection;
            }
        }

        /// <summary>
        /// Gets or sets the name of this <see cref="IWebDAVStoreItem"/>.
        /// </summary>
        public string Name
        {
            get
            {
                return Path.GetFileName(_Path);
            }

            set
            {
                throw new InvalidOperationException("Unable to rename item");
            }
        }

        #endregion
    }
}
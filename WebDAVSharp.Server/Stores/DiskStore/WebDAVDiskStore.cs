using System;
using System.Diagnostics;
using System.IO;

namespace WebDAVSharp.Server.Stores.DiskStore
{
    /// <summary>
    /// This class implements a disk-based <see cref="IWebDAVStore"/>.
    /// </summary>
    [DebuggerDisplay("Disk Store ({RootPath})")]
    public sealed class WebDAVDiskStore : IWebDAVStore
    {
        private readonly string _RootPath;
        private readonly IWebDAVStoreCollection _RootCollection;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebDAVDiskStore"/> class.
        /// </summary>
        /// <param name="rootPath">
        /// The root path of a folder on disk to host in this <see cref="WebDAVDiskStore"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="rootPath"/> is <c>null</c>.</para>
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        /// <para><paramref name="rootPath"/> specifies a folder that does not exist.</para>
        /// </exception>
        public WebDAVDiskStore(string rootPath)
        {
            if (rootPath == null)
                throw new ArgumentNullException(rootPath);
            if (!Directory.Exists(rootPath))
                throw new DirectoryNotFoundException(rootPath);

            _RootPath = rootPath;
            _RootCollection = new WebDAVDiskStoreCollection(null, rootPath);
        }

        /// <summary>
        /// Gets the root path for the folder that is hosted in this <see cref="WebDAVDiskStore"/>.
        /// </summary>
        public string RootPath
        {
            get
            {
                return _RootPath;
            }
        }

        #region IWebDAVStore Members

        /// <summary>
        /// Gets the root collection of this <see cref="IWebDAVStore"/>.
        /// </summary>
        public IWebDAVStoreCollection Root
        {
            get
            {
                return _RootCollection;
            }
        }

        #endregion
    }
}
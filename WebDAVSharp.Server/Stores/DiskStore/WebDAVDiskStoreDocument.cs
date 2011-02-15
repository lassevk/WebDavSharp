using System;
using System.Diagnostics;
using System.IO;

namespace WebDAVSharp.Server.Stores.DiskStore
{
    /// <summary>
    /// This class implements a disk-based <see cref="WebDAVDiskStoreDocument"/> mapped to a file.
    /// </summary>
    [DebuggerDisplay("File ({Name})")]
    public sealed class WebDAVDiskStoreDocument : WebDAVDiskStoreItem, IWebDAVStoreDocument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebDAVDiskStoreDocument"/> class.
        /// </summary>
        /// <param name="parentCollection">The parent <see cref="WebDAVDiskStoreCollection"/> that contains this <see cref="WebDAVDiskStoreItem"/>;
        /// or <c>null</c> if this is the root <see cref="WebDAVDiskStoreCollection"/>.</param>
        /// <param name="path">The path that this <see cref="WebDAVDiskStoreItem"/> maps to.</param>
        /// <exception cref="ArgumentNullException"><paramref name="path"/> is <c>null</c> or empty.</exception>
        public WebDAVDiskStoreDocument(WebDAVDiskStoreCollection parentCollection, string path)
            : base(parentCollection, path)
        {
            // Do nothing here
        }

        #region IWebDAVStoreDocument Members

        /// <summary>
        /// Gets the size of the document in bytes.
        /// </summary>
        public long Size
        {
            get
            {
                using (var stream = new FileStream(ItemPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    return stream.Length;
                }
            }
        }

        #endregion

        #region IWebDAVStoreDocument Members

        /// <summary>
        /// Opens a <see cref="Stream"/> object for the document, in read-only mode.
        /// </summary>
        /// <returns>
        /// The <see cref="Stream"/> object that can be read from.
        /// </returns>
        public Stream OpenReadStream()
        {
            return new FileStream(ItemPath, FileMode.Open, FileAccess.Read, FileShare.None);
        }

        /// <summary>
        /// Opens a <see cref="Stream"/> object for the document, in write-only mode.
        /// </summary>
        /// <param name="append">
        /// A value indicating whether to append to the existing document;
        /// if <c>false</c>, the existing content will be dropped.
        /// </param>
        /// <returns>
        /// The <see cref="Stream"/> object that can be written to.
        /// </returns>
        public Stream OpenWriteStream(bool append)
        {
            if (append)
            {
                var result = new FileStream(ItemPath, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                result.Seek(0, SeekOrigin.End);
                return result;
            }

            return new FileStream(ItemPath, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
        }

        #endregion
    }
}
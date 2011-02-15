using System;
using System.Diagnostics;
using System.IO;

namespace WebDAVSharp.Server.Stores.MemoryStore
{
    /// <summary>
    /// This class implements a memory-based <see cref="IWebDAVStoreDocument"/>.
    /// </summary>
    [DebuggerDisplay("Document ({Name}, Size={Size})")]
    public class WebDAVMemoryStoreDocument : WebDAVMemoryStoreItem, IWebDAVStoreDocument
    {
        private byte[] _Content = new byte[0];

        /// <summary>
        /// Initializes a new instance of the <see cref="WebDAVMemoryStoreDocument"/> class.
        /// </summary>
        /// <param name="parentCollection">The parent <see cref="IWebDAVStoreCollection"/> that contains this <see cref="IWebDAVStoreItem"/> implementation.</param>
        /// <param name="name">The name of this <see cref="IWebDAVStoreItem"/></param>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> is <c>null</c>.</exception>
        public WebDAVMemoryStoreDocument(IWebDAVStoreCollection parentCollection, string name)
            : base(parentCollection, name)
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
                return _Content.Length;
            }
        }

        /// <summary>
        /// Opens a <see cref="Stream"/> object for the document, in read-only mode.
        /// </summary>
        /// <returns>
        /// The <see cref="Stream"/> object that can be read from.
        /// </returns>
        public Stream OpenReadStream()
        {
            return new MemoryStream(_Content);
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
            var stream = new MemoryStreamEx();
            if (append)
                stream.Write(_Content, 0, _Content.Length);
            stream.Closed += (s, e) => _Content = stream.ToArray();

            return stream;
        }

        #endregion
    }
}
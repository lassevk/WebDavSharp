using System;
using System.IO;

namespace WebDAVSharp.Server.Stores.MemoryStore
{
    /// <summary>
    /// This is a descendant of <see cref="MemoryStream"/> that just adds an event for when
    /// the stream is closed.
    /// </summary>
    internal class MemoryStreamEx : MemoryStream
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryStreamEx"/> class.
        /// </summary>
        public MemoryStreamEx()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryStreamEx"/> class.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        public MemoryStreamEx(byte[] buffer)
            : base(buffer)
        {
        }

        /// <summary>
        /// Closes the current stream and releases any resources (such as sockets and file handles) associated with the current stream.
        /// </summary>
        public override void Close()
        {
            base.Close();
            OnClosed();
        }

        /// <summary>
        /// This event is fired when the stream has been closed.
        /// </summary>
        public event EventHandler Closed;

        /// <summary>
        /// Called when the stream has been closed.
        /// </summary>
        protected void OnClosed()
        {
            if (Closed != null)
                Closed(this, EventArgs.Empty);
        }
    }
}
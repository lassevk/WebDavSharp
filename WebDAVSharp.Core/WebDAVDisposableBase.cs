﻿using System;

namespace WebDAVSharp
{
    /// <summary>
    /// This abstract base class implements the <see cref="IDisposable"/> pattern in a reusable way.
    /// </summary>
    public abstract class WebDAVDisposableBase : IDisposable
    {
        private bool _IsDisposed;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            _IsDisposed = true;

            Dispose(true);
        }

        /// <summary>
        /// This method will ensure that the object has not been disposed of through a call
        /// to <see cref="Dispose()"/>, and if it has, it will throw <see cref="ObjectDisposedException"/>
        /// </summary>
        /// <exception cref="ObjectDisposedException">
        /// <para>The object has been disposed of.</para>
        /// </exception>
        protected void EnsureNotDisposed()
        {
            if (_IsDisposed)
                throw new ObjectDisposedException(GetType().FullName);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected abstract void Dispose(bool disposing);
    }
}
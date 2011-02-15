﻿namespace WebDAVSharp.Server.Adapters
{
    /// <summary>
    /// This interface is implemented by other adapters in the WebDAV#
    /// project, to facilitate access to the underlying adapted object.
    /// </summary>
    /// <typeparam name="T">
    /// The type of internal instance that is adapted.
    /// </typeparam>
    public interface IAdapter<T>
    {
        /// <summary>
        /// Gets the internal instance that was adapted for WebDAV#.
        /// </summary>
        T AdaptedInstance
        {
            get;
        }
    }
}
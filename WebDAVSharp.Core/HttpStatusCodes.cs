using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;

namespace WebDAVSharp
{
    /// <summary>
    /// Constants for HTTP status codes.
    /// </summary>
    /// <remarks>
    /// <para>HTTP Status Codes Source: http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html </para>
    /// <para>WebDAV Status Codes Source: http://www.webdav.org/specs/rfc2518.html#status.code.extensions.to.http11 </para>
    /// </remarks>
    public static partial class HttpStatusCodes
    {
        /// <summary>
        /// Gets a collection of <see cref="KeyValuePair{TKey,TValue}"/> objects
        /// for all the known status codes and their short descriptions.
        /// </summary>
        /// <returns>
        /// A collection of <see cref="KeyValuePair{TKey,TValue}"/> objects.
        /// for all the known status codes and their short descriptions.
        /// </returns>
        public static IEnumerable<KeyValuePair<int, string>> GetStatusCodes()
        {
            return _Descriptions.ToArray();
        }

        /// <summary>
        /// Returns the short description belonging to a status code.
        /// </summary>
        /// <param name="statusCode">
        /// The status code to retrieve the short description for.
        /// </param>
        /// <returns>
        /// The short description for the status code.
        /// </returns>
        public static string GetDescription(int statusCode)
        {
            string description;
            if (_Descriptions.TryGetValue(statusCode, out description))
                return description;
            return String.Format("Unknown status code (#{0})", statusCode);
        }
    }
}
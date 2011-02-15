using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

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
        public static IEnumerable<StatusCode> StatusCodes
        {
            get
            {
                return _StatusCodes.Values.ToArray();
            }
        }

        /// <summary>
        /// Returns the short description belonging to a status code.
        /// </summary>
        /// <param name="id">
        /// The id of the status code to retrieve the short description for.
        /// </param>
        /// <returns>
        /// The short description for the status code.
        /// </returns>
        public static string GetName(int id)
        {
            StatusCode statusCode;
            if (_StatusCodes.TryGetValue(id, out statusCode))
                return statusCode.Name;
            return String.Format(CultureInfo.InvariantCulture, "Unknown status code (#{0})", id);
        }
    }
}
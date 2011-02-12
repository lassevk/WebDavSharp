using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;

namespace WebDAVSharp
{
    /// <summary>
    /// Constants for HTTP status codes.
    /// </summary>
    public static partial class HttpStatusCodes
    {
        private static readonly Dictionary<int, string> _Descriptions = new Dictionary<int, string>
        {
            // Informational 1xx: This class of status code indicates a provisional response, consisting only of the Status-Line and optional headers, and is terminated by an empty line.
            { 100, "Continue" },
            { 101, "Switching Protocols" },
            { 102, "Processing" },

            // Successful 2xx: This class of status code indicates that the client's request was successfully received, understood, and accepted.
            { 200, "OK" },
            { 201, "Created" },
            { 202, "Accepted" },
            { 203, "Non-Authoritative Information" },
            { 204, "No Content" },
            { 205, "Reset Content" },
            { 206, "Partial Content" },
            { 207, "Multi-Status" },

            // Redirection 3xx: This class of status code indicates that further action needs to be taken by the user agent in order to fulfill the request.
            { 300, "Multiple Choices" },
            { 301, "Moved Permanently" },
            { 302, "Found" },
            { 303, "See Other" },
            { 304, "Not Modified" },
            { 305, "Use Proxy" },
            { 307, "Temporary Redirect" },

            // The 4xx class of status code is intended for cases in which the client seems to have erred.

            // Response status codes beginning with the digit "5" indicate cases in which the server is aware that it has erred or is incapable of performing the request
        };

        /// <summary>
        /// Informational 1xx: This class of status code indicates a provisional response, consisting only of the Status-Line and optional headers, and is terminated by an empty line.
        /// </summary>
        /// <remarks>
        /// For more information, see <see href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html#sec10.1"/>.
        /// </remarks>
        public static class Informational
        {
            /// <summary>
            /// #100: The client SHOULD continue with its request.
            /// </summary>
            /// <remarks>
            /// For more information, see <see href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html#sec10.1.1"/>.
            /// </remarks>
            public const int Continue = 100;

            /// <summary>
            /// #101: The server understands and is willing to comply with the client's request, via the Upgrade message header field, for a change in the application protocol being used on this connection.
            /// </summary>
            /// <remarks>
            /// For more information, see <see href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html#sec10.1.2"/>.
            /// </remarks>
            public const int SwitchingProtocols = 101;

            /// <summary>
            /// #102: The 102 (Processing) status code is an interim response used to inform the client that the server has accepted the complete request, but has not yet completed it.
            /// </summary>
            /// <remarks>
            /// For more information, see <see href="http://www.webdav.org/specs/rfc2518.html#STATUS_102"/>.
            /// </remarks>
            public const int Processing = 102;
        }

        /// <summary>
        /// Successful 2xx: This class of status code indicates that the client's request was successfully received, understood, and accepted.
        /// </summary>
        /// <remarks>
        /// For more information, see <see href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html#sec10.2"/>.
        /// </remarks>
        public static class Successful
        {
            /// <summary>
            /// #200: The request has succeeded.
            /// </summary>
            /// <remarks>
            /// For more information, see <see href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html#sec10.2.1"/>.
            /// </remarks>
            public const int OK = 200;

            /// <summary>
            /// #201: The request has been fulfilled and resulted in a new resource being created.
            /// </summary>
            /// <remarks>
            /// For more information, see <see href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html#sec10.2.2"/>.
            /// </remarks>
            public const int Created = 201;

            /// <summary>
            /// #202: The request has been accepted for processing, but the processing has not been completed.
            /// </summary>
            /// <remarks>
            /// For more information, see <see href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html#sec10.2.3"/>.
            /// </remarks>
            public const int Accepted = 202;

            /// <summary>
            /// #203: The returned metainformation in the entity-header is not the definitive set as available from the origin server, but is gathered from a local or a third-party copy.
            /// </summary>
            /// <remarks>
            /// For more information, see <see href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html#sec10.2.4"/>.
            /// </remarks>
            public const int NonAuthoritativeInformation = 203;

            /// <summary>
            /// #204: The server has fulfilled the request but does not need to return an entity-body, and might want to return updated metainformation.
            /// </summary>
            /// <remarks>
            /// For more information, see <see href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html#sec10.2.5"/>.
            /// </remarks>
            public const int NoContent = 204;

            /// <summary>
            /// #205: The server has fulfilled the request and the user agent SHOULD reset the document view which caused the request to be sent.
            /// </summary>
            /// <remarks>
            /// For more information, see <see href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html#sec10.2.6"/>.
            /// </remarks>
            public const int ResetContent = 205;

            /// <summary>
            /// #206: The server has fulfilled the partial GET request for the resource.
            /// </summary>
            /// <remarks>
            /// For more information, see <see href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html#sec10.2.7"/>.
            /// </remarks>
            public const int PartialContent = 206;

            /// <summary>
            /// #207: The 207 (Multi-Status) status code provides status for multiple independent operations.
            /// </summary>
            /// <remarks>
            /// For more information, see <see href="http://www.webdav.org/specs/rfc2518.html#STATUS_207"/>.
            /// </remarks>
            public const int MultiStatus = 207;
        }

        /// <summary>
        /// Redirection 3xx: This class of status code indicates that further action needs to be taken by the user agent in order to fulfill the request.
        /// </summary>
        /// <remarks>
        /// For more information, see <see href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html#sec10.3"/>.
        /// </remarks>
        public static class Redirection
        {
            /// <summary>
            /// #300: The requested resource corresponds to any one of a set of representations.
            /// </summary>
            /// <remarks>
            /// For more information, see <see href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html#sec10.3.1"/>.
            /// </remarks>
            public const int MultipleChoices = 300;

            /// <summary>
            /// #301: The requested resource has been assigned a new permanent URI and any future references to this resource SHOULD use one of the returned URIs.
            /// </summary>
            /// <remarks>
            /// For more information, see <see href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html#sec10.3.2"/>.
            /// </remarks>
            public const int MovedPermanently = 301;

            /// <summary>
            /// #302: The requested resource resides temporarily under a different URI.
            /// </summary>
            /// <remarks>
            /// For more information, see <see href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html#sec10.3.3"/>.
            /// </remarks>
            public const int Found = 302;

            /// <summary>
            /// #303: The response to the request can be found under a different URI and SHOULD be retrieved using a GET method on that resource.
            /// </summary>
            /// <remarks>
            /// For more information, see <see href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html#sec10.3.4"/>.
            /// </remarks>
            public const int SeeOther = 303;

            /// <summary>
            /// #304: If the client has performed a conditional GET request and access is allowed, but the document has not been modified, the server SHOULD respond with this status code.
            /// </summary>
            /// <remarks>
            /// For more information, see <see href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html#sec10.3.5"/>.
            /// </remarks>
            public const int NotModified = 304;

            /// <summary>
            /// #305: The requested resource MUST be accessed through the proxy given by the Location field.
            /// </summary>
            /// <remarks>
            /// For more information, see <see href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html#sec10.3.6"/>.
            /// </remarks>
            public const int UseProxy = 305;

            /// <summary>
            /// #307: The requested resource resides temporarily under a different URI.
            /// </summary>
            /// <remarks>
            /// For more information, see <see href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html#sec10.3.8"/>.
            /// </remarks>
            public const int TemporaryRedirect = 307;
        }

        /// <summary>
        /// The 4xx class of status code is intended for cases in which the client seems to have erred.
        /// </summary>
        /// <remarks>
        /// For more information, see <see href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html#sec10.4"/>.
        /// </remarks>
        public static class ClientError
        {
        }

        /// <summary>
        /// Response status codes beginning with the digit "5" indicate cases in which the server is aware that it has erred or is incapable of performing the request
        /// </summary>
        /// <remarks>
        /// For more information, see <see href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html#sec10.5"/>.
        /// </remarks>
        public static class ServerError
        {
        }
    }
}

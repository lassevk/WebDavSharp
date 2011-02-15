using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WebDAVSharp
{
    /// <summary>
    /// Constants for HTTP status codes.
    /// </summary>
    public static partial class HttpStatusCodes
    {
        private static readonly Dictionary<int, StatusCode> _StatusCodes = new Dictionary<int, StatusCode>
        {
            // Informational 1xx: This class of status code indicates a provisional response, consisting only of the Status-Line and optional headers, and is terminated by an empty line.
            { 100, new StatusCode(100, "Continue") },
            { 101, new StatusCode(101, "Switching Protocols") },
            { 102, new StatusCode(102, "Processing") },

            // Successful 2xx: This class of status code indicates that the client's request was successfully received, understood, and accepted.
            { 200, new StatusCode(200, "OK") },
            { 201, new StatusCode(201, "Created") },
            { 202, new StatusCode(202, "Accepted") },
            { 203, new StatusCode(203, "Non-Authoritative Information") },
            { 204, new StatusCode(204, "No Content") },
            { 205, new StatusCode(205, "Reset Content") },
            { 206, new StatusCode(206, "Partial Content") },
            { 207, new StatusCode(207, "Multi-Status") },

            // Redirection 3xx: This class of status code indicates that further action needs to be taken by the user agent in order to fulfill the request.
            { 300, new StatusCode(300, "Multiple Choices") },
            { 301, new StatusCode(301, "Moved Permanently") },
            { 302, new StatusCode(302, "Found") },
            { 303, new StatusCode(303, "See Other") },
            { 304, new StatusCode(304, "Not Modified") },
            { 305, new StatusCode(305, "Use Proxy") },
            { 307, new StatusCode(307, "Temporary Redirect") },

            // The 4xx class of status code is intended for cases in which the client seems to have erred.
            { 400, new StatusCode(400, "Bad Request") },
            { 401, new StatusCode(401, "Unauthorized") },
            { 402, new StatusCode(402, "Bad PaymentRequired") },
            { 403, new StatusCode(403, "Forbidden") },
            { 404, new StatusCode(404, "Not Found") },
            { 405, new StatusCode(405, "Method Not Allowed") },
            { 406, new StatusCode(406, "Not Acceptable") },
            { 407, new StatusCode(407, "Proxy Authentication Required") },
            { 408, new StatusCode(408, "Request Timeout") },
            { 409, new StatusCode(409, "Conflict") },
            { 410, new StatusCode(410, "Gone") },
            { 411, new StatusCode(411, "Length Required") },
            { 412, new StatusCode(412, "Precondition Failed") },
            { 413, new StatusCode(413, "Request Entity Too Large") },
            { 414, new StatusCode(414, "Request-URI Too Long") },
            { 415, new StatusCode(415, "Unsupported Media Type") },
            { 416, new StatusCode(416, "Requested Range Not Satisfiable") },
            { 417, new StatusCode(417, "Expectation Failed") },
            { 422, new StatusCode(422, "Unprocessable Entity") },
            { 423, new StatusCode(423, "Locked") },
            { 424, new StatusCode(424, "Failed Dependency") },

            // Response status codes beginning with the digit "5" indicate cases in which the server is aware that it has erred or is incapable of performing the request
            { 500, new StatusCode(500, "Internal Server Error") },
            { 501, new StatusCode(501, "Not Implemented") },
            { 502, new StatusCode(502, "Bad Gateway") },
            { 503, new StatusCode(503, "Service Unavailable") },
            { 504, new StatusCode(504, "Gateway Timeout") },
            { 505, new StatusCode(505, "HTTP Version Not Supported") },
            { 507, new StatusCode(507, "Insufficient Storage") },
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
        public static class ClientErrors
        {
            /// <summary>
            /// #400: The request could not be understood by the server due to malformed syntax.
            /// </summary>
            /// <remarks>
            /// For more information, see <see href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html#sec10.4.1"/>.
            /// </remarks>
            public const int BadRequest = 400;

            /// <summary>
            /// #401: The request requires user authentication.
            /// </summary>
            /// <remarks>
            /// For more information, see <see href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html#sec10.4.2"/>.
            /// </remarks>
            public const int Unauthorized = 401;

            /// <summary>
            /// #402: This code is reserved for future use
            /// </summary>
            /// <remarks>
            /// For more information, see <see href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html#sec10.4.3"/>.
            /// </remarks>
            public const int PaymentRequired = 402;

            /// <summary>
            /// #403: The server understood the request, but is refusing to fulfill it.
            /// </summary>
            /// <remarks>
            /// For more information, see <see href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html#sec10.4.4"/>.
            /// </remarks>
            public const int Forbidden = 403;

            /// <summary>
            /// #404: The server has not found anything matching the Request-URI.
            /// </summary>
            /// <remarks>
            /// For more information, see <see href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html#sec10.4.5"/>.
            /// </remarks>
            public const int NotFound = 404;

            /// <summary>
            /// #405: The method specified in the Request-Line is not allowed for the resource identified by the Request-URI.
            /// </summary>
            /// <remarks>
            /// For more information, see <see href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html#sec10.4.6"/>.
            /// </remarks>
            public const int MethodNotAllowed = 405;

            /// <summary>
            /// #406: The resource identified by the request is only capable of generating response entities which have content characteristics not acceptable according to the accept headers sent in the request.
            /// </summary>
            /// <remarks>
            /// For more information, see <see href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html#sec10.4.7"/>.
            /// </remarks>
            public const int NotAcceptable = 406;

            /// <summary>
            /// #407: This code is similar to 401 (<see cref="Unauthorized"/>), but indicates that the client must first authenticate itself with the proxy.
            /// </summary>
            /// <remarks>
            /// For more information, see <see href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html#sec10.4.8"/>.
            /// </remarks>
            public const int ProxyAuthenticationRequired = 407;

            /// <summary>
            /// #408: The client did not produce a request within the time that the server was prepared to wait.
            /// </summary>
            /// <remarks>
            /// For more information, see <see href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html#sec10.4.9"/>.
            /// </remarks>
            public const int RequestTimeout = 408;

            /// <summary>
            /// #409: The request could not be completed due to a conflict with the current state of the resource.
            /// </summary>
            /// <remarks>
            /// For more information, see <see href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html#sec10.4.10"/>.
            /// </remarks>
            public const int Conflict = 409;

            /// <summary>
            /// #410: The requested resource is no longer available at the server and no forwarding address is known.
            /// </summary>
            /// <remarks>
            /// For more information, see <see href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html#sec10.4.11"/>.
            /// </remarks>
            public const int Gone = 410;

            /// <summary>
            /// #411: The server refuses to accept the request without a defined Content- Length.
            /// </summary>
            /// <remarks>
            /// For more information, see <see href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html#sec10.4.12"/>.
            /// </remarks>
            public const int LengthRequired = 411;

            /// <summary>
            /// #412: The precondition given in one or more of the request-header fields evaluated to false when it was tested on the server.
            /// </summary>
            /// <remarks>
            /// For more information, see <see href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html#sec10.4.13"/>.
            /// </remarks>
            public const int PreconditionFailed = 412;

            /// <summary>
            /// #413: The server is refusing to process a request because the request entity is larger than the server is willing or able to process.
            /// </summary>
            /// <remarks>
            /// For more information, see <see href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html#sec10.4.14"/>.
            /// </remarks>
            public const int RequestEntityTooLarge = 413;

            /// <summary>
            /// #414: The server is refusing to service the request because the Request-URI is longer than the server is willing to interpret.
            /// </summary>
            /// <remarks>
            /// For more information, see <see href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html#sec10.4.15"/>.
            /// </remarks>
            public const int RequestUriTooLong = 414;

            /// <summary>
            /// #415: The server is refusing to service the request because the entity of the request is in a format not supported by the requested resource for the requested method.
            /// </summary>
            /// <remarks>
            /// For more information, see <see href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html#sec10.4.16"/>.
            /// </remarks>
            public const int UnsupportedMediaType = 415;

            /// <summary>
            /// #416: A server SHOULD return a response with this status code if a request included a Range request-header field, and none of the range-specifier values in this field overlap the current extent of the selected resource, and the request did not include an If-Range request-header field.
            /// </summary>
            /// <remarks>
            /// For more information, see <see href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html#sec10.4.17"/>.
            /// </remarks>
            public const int RequestedRangeNotSatisfiable = 416;

            /// <summary>
            /// #417: The expectation given in an Expect request-header field could not be met by this server, or, if the server is a proxy, the server has unambiguous evidence that the request could not be met by the next-hop server.
            /// </summary>
            /// <remarks>
            /// For more information, see <see href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html#sec10.4.18"/>.
            /// </remarks>
            public const int ExpectationFailed = 417;

            /// <summary>
            /// #422: The 422 (<see cref="UnprocessableEntity"/>) status code means the server understands the content type of the request entity (hence a 415 (<see cref="UnsupportedMediaType"/>) status code is inappropriate), and the syntax of the request entity is correct (thus a 400 (<see cref="BadRequest"/>) status code is inappropriate) but was unable to process the contained instructions.
            /// </summary>
            /// <remarks>
            /// For more information, see <see href="http://www.webdav.org/specs/rfc2518.html#STATUS_422"/>.
            /// </remarks>
            public const int UnprocessableEntity = 422;

            /// <summary>
            /// #423: The 423 (<see cref="Locked"/>) status code means the source or destination resource of a method is locked.
            /// </summary>
            /// <remarks>
            /// For more information, see <see href="http://www.webdav.org/specs/rfc2518.html#STATUS_423"/>.
            /// </remarks>
            public const int Locked = 423;

            /// <summary>
            /// #424: The 424 (<see cref="FailedDependency"/>) status code means that the method could not be performed on the resource because the requested action depended on another action and that action failed.
            /// </summary>
            /// <remarks>
            /// For more information, see <see href="http://www.webdav.org/specs/rfc2518.html#STATUS_424"/>.
            /// </remarks>
            public const int FailedDependency = 424;
        }

        /// <summary>
        /// Response status codes beginning with the digit "5" indicate cases in which the server is aware that it has erred or is incapable of performing the request
        /// </summary>
        /// <remarks>
        /// For more information, see <see href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html#sec10.5"/>.
        /// </remarks>
        public static class ServerErrors
        {
            /// <summary>
            /// #500: The server encountered an unexpected condition which prevented it from fulfilling the request.
            /// </summary>
            /// <remarks>
            /// For more information, see <see href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html#sec10.5.1"/>.
            /// </remarks>
            public const int InternalServerError = 500;

            /// <summary>
            /// #501: The server does not support the functionality required to fulfill the request.
            /// </summary>
            /// <remarks>
            /// For more information, see <see href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html#sec10.5.2"/>.
            /// </remarks>
            public const int NotImplemented = 501;

            /// <summary>
            /// #502: The server, while acting as a gateway or proxy, received an invalid response from the upstream server it accessed in attempting to fulfill the request.
            /// </summary>
            /// <remarks>
            /// For more information, see <see href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html#sec10.5.3"/>.
            /// </remarks>
            public const int BadGateway = 502;

            /// <summary>
            /// #503: The server is currently unable to handle the request due to a temporary overloading or maintenance of the server.
            /// </summary>
            /// <remarks>
            /// For more information, see <see href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html#sec10.5.4"/>.
            /// </remarks>
            public const int ServiceUnavailable = 503;

            /// <summary>
            /// #504: The server, while acting as a gateway or proxy, did not receive a timely response from the upstream server specified by the URI (e.g. HTTP, FTP, LDAP) or some other auxiliary server (e.g. DNS) it needed to access in attempting to complete the request.
            /// </summary>
            /// <remarks>
            /// For more information, see <see href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html#sec10.5.5"/>.
            /// </remarks>
            public const int GatewayTimeout = 504;

            /// <summary>
            /// #505: The server does not support, or refuses to support, the HTTP protocol version that was used in the request message.
            /// </summary>
            /// <remarks>
            /// For more information, see <see href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html#sec10.5.6"/>.
            /// </remarks>
            public const int HttpVersionNotSupported = 505;

            /// <summary>
            /// #507: The 507 (<see cref="InsufficientStorage"/>) status code means the method could not be performed on the resource because the server is unable to store the representation needed to successfully complete the request.
            /// </summary>
            /// <remarks>
            /// For more information, see <see href="http://www.webdav.org/specs/rfc2518.html#STATUS_507"/>.
            /// </remarks>
            public const int InsufficientStorage = 507;
        }
    }
}

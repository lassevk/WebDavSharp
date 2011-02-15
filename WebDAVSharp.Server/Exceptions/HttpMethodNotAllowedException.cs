using System;

namespace WebDAVSharp.Server.Exceptions
{
    /// <summary>
    /// This exception is thrown when a request uses a HTTP method to request or manipulate a resource
    /// for which the specified HTTP method is not allowed.
    /// </summary>
    public class HttpMethodNotAllowedException : HttpException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpMethodNotAllowedException"/> class.
        /// </summary>
        /// <param name="message">
        /// The exception message stating the reason for the exception being thrown.
        /// </param>
        /// <param name="innerException">
        /// The <see cref="Exception"/> that is the cause for this exception;
        /// or <c>null</c> if no inner exception is specified.
        /// </param>
        public HttpMethodNotAllowedException(string message = null, Exception innerException = null)
            : base(HttpStatusCodes.ClientErrors.MethodNotAllowed, message, innerException)
        {
            // Do nothing here
        }
    }
}
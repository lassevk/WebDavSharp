using System;

namespace WebDAVSharp.Server.Exceptions
{
    /// <summary>
    /// This exception is thrown when a request uses a HTTP method or functionality that has yet to
    /// be implemented.
    /// </summary>
    public class HttpNotImplementedException : HttpException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpNotImplementedException"/> class.
        /// </summary>
        /// <param name="message">
        /// The exception message stating the reason for the exception being thrown.
        /// </param>
        /// <param name="innerException">
        /// The <see cref="Exception"/> that is the cause for this exception;
        /// or <c>null</c> if no inner exception is specified.
        /// </param>
        public HttpNotImplementedException(string message = null, Exception innerException = null)
            : base(HttpStatusCodes.ServerErrors.NotImplemented, message, innerException)
        {
            // Do nothing here
        }
    }
}
using System;

namespace WebDAVSharp.Server.Exceptions
{
    /// <summary>
    /// This exception is thrown when a request cannot be completed due to a conflict with the requested
    /// resource.
    /// </summary>
    public class HttpConflictException : HttpException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpConflictException"/> class.
        /// </summary>
        /// <param name="message">
        /// The exception message stating the reason for the exception being thrown.
        /// </param>
        /// <param name="innerException">
        /// The <see cref="Exception"/> that is the cause for this exception;
        /// or <c>null</c> if no inner exception is specified.
        /// </param>
        public HttpConflictException(string message = null, Exception innerException = null)
            : base(HttpStatusCodes.ClientErrors.Conflict, message, innerException)
        {
            // Do nothing here
        }
    }
}
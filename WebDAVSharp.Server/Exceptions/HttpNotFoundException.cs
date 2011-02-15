using System;

namespace WebDAVSharp.Server.Exceptions
{
    /// <summary>
    /// This exception is thrown when a request tries to access a resource that does not exist.
    /// </summary>
    public class HttpNotFoundException : HttpException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpNotFoundException"/> class.
        /// </summary>
        /// <param name="message">
        /// The exception message stating the reason for the exception being thrown.
        /// </param>
        /// <param name="innerException">
        /// The <see cref="Exception"/> that is the cause for this exception;
        /// or <c>null</c> if no inner exception is specified.
        /// </param>
        public HttpNotFoundException(string message = null, Exception innerException = null)
            : base(HttpStatusCodes.ClientErrors.NotFound, message, innerException)
        {
            // Do nothing here
        }
    }
}
using System;

namespace WebDAVSharp.Server.Exceptions
{
    /// <summary>
    /// This exception is thrown when the server throws a different exception than the standard
    /// ones that <see cref="WebDAVServer"/> knows how to respond to.
    /// </summary>
    public class HttpInternalServerException : HttpException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpInternalServerException"/> class.
        /// </summary>
        /// <param name="message">
        /// The exception message stating the reason for the exception being thrown.
        /// </param>
        /// <param name="innerException">
        /// The <see cref="Exception"/> that is the cause for this exception;
        /// or <c>null</c> if no inner exception is specified.
        /// </param>
        public HttpInternalServerException(string message = null, Exception innerException = null)
            : base(HttpStatusCodes.ServerErrors.InternalServerError, message, innerException)
        {
            // Do nothing here
        }
    }
}
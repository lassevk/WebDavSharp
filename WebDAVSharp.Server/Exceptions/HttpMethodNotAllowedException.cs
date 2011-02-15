using System;
using System.Runtime.Serialization;

namespace WebDAVSharp.Server.Exceptions
{
    /// <summary>
    /// This exception is thrown when a request uses a HTTP method to request or manipulate a resource
    /// for which the specified HTTP method is not allowed.
    /// </summary>
    [Serializable]
    public class HttpMethodNotAllowedException : HttpException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpMethodNotAllowedException"/> class.
        /// </summary>
        public HttpMethodNotAllowedException()
            : this(null, null)
        {
            // Do nothing here
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpMethodNotAllowedException"/> class.
        /// </summary>
        /// <param name="message">
        /// The exception message stating the reason for the exception being thrown.
        /// </param>
        public HttpMethodNotAllowedException(string message)
            : this(message, null)
        {
            // Do nothing here
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpMethodNotAllowedException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// The <paramref name="info"/> parameter is null.
        /// </exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">
        /// The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0).
        /// </exception>
        protected HttpMethodNotAllowedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // Do nothing here
        }

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
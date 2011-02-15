using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace WebDAVSharp.Server.Exceptions
{
    /// <summary>
    /// This exception, or a descendant, is thrown when requests fail, specifying the status code
    /// that the server should return back to the client.
    /// </summary>
    [Serializable]
    public class HttpException : Exception
    {
        private const string StatusCodeKey = "StatusCode";
        private const string StatusDescriptionKey = "StatusDescription";

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpException"/> class.
        /// </summary>
        public HttpException()
            : this(500, null, null)
        {
            // Do nothing here
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpException"/> class.
        /// </summary>
        /// <param name="message">
        /// The exception message stating the reason for the exception being thrown.
        /// </param>
        public HttpException(string message)
            : this(500, message, null)
        {
            // Do nothing here
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpException"/> class.
        /// </summary>
        /// <param name="message">
        /// The exception message stating the reason for the exception being thrown.
        /// </param>
        /// <param name="innerException">
        /// The <see cref="Exception"/> that is the cause for this exception;
        /// or <c>null</c> if no inner exception is specified.
        /// </param>
        public HttpException(string message, Exception innerException)
            : this(500, message, innerException)
        {
            // Do nothing here
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// The <paramref name="info"/> parameter is null.
        /// </exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">
        /// The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0).
        /// </exception>
        protected HttpException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            StatusCode = info.GetInt32(StatusCodeKey);
            StatusDescription = info.GetString(StatusDescriptionKey);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpException"/> class.
        /// </summary>
        /// <param name="statusCode">
        /// The HTTP status code that this <see cref="HttpException"/> maps to.
        /// </param>
        /// <param name="message">
        /// The exception message stating the reason for the exception being thrown.
        /// </param>
        /// <param name="innerException">
        /// The <see cref="Exception"/> that is the cause for this exception;
        /// or <c>null</c> if no inner exception is specified.
        /// </param>
        public HttpException(int statusCode, string message = null, Exception innerException = null)
            : base(GetMessage(statusCode, message), innerException)
        {
            StatusCode = statusCode;
            StatusDescription = HttpStatusCodes.GetName(statusCode);
        }

        /// <summary>
        /// Gets the HTTP status code that this <see cref="HttpException"/> maps to.
        /// </summary>
        public int StatusCode
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the status description for the HTTP <see cref="StatusCode"/>.
        /// </summary>
        public string StatusDescription
        {
            get;
            private set;
        }

        /// <summary>
        /// When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo"/> with information about the exception.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// The <paramref name="info"/> parameter is a null reference (Nothing in Visual Basic).
        /// </exception>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(StatusCodeKey, StatusCode);
            info.AddValue(StatusDescriptionKey, StatusDescription);
        }

        private static string GetMessage(int statusCode, string message)
        {
            string format = "%s";
            if (!StringEx.IsNullOrWhiteSpace(message))
                format = message;

            return format.Replace("%s", HttpStatusCodes.GetName(statusCode));
        }
    }
}
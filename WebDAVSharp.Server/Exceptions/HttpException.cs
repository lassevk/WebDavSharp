using System;

namespace WebDAVSharp.Server.Exceptions
{
    /// <summary>
    /// This exception, or a descendant, is thrown when requests fail, specifying the status code
    /// that the server should return back to the client.
    /// </summary>
    public class HttpException : Exception
    {
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
            StatusDescription = HttpStatusCodes.GetDescription(statusCode);
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

        private static string GetMessage(int statusCode, string message)
        {
            string format = "%s";
            if (!StringEx.IsNullOrWhiteSpace(message))
                format = message;

            return format.Replace("%s", HttpStatusCodes.GetDescription(statusCode));
        }
    }
}
using System;

namespace WebDAVSharp
{
    /// <summary>
    /// This <see cref="EventArgs"/> descendant holds information for the <see cref="EventBasedLogger.MessageLogged"/> event.
    /// </summary>
    public class LogEventArgs : EventArgs
    {
        private readonly DateTime _Timestamp;
        private readonly LogLevel _Level;
        private readonly string _Message;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogEventArgs"/> class.
        /// </summary>
        /// <param name="timestamp">
        /// The timestamp of the <paramref name="message"/> that was logged.
        /// </param>
        /// <param name="level">
        /// The log level of the <paramref name="message"/> that was logged.
        /// </param>
        /// <param name="message">
        /// The message that was logged.
        /// </param>
        public LogEventArgs(DateTime timestamp, LogLevel level, string message)
        {
            _Timestamp = timestamp;
            _Level = level;
            _Message = message ?? string.Empty;
        }

        /// <summary>
        /// Gets the timestamp of the <see cref="Message"/> that was logged.
        /// </summary>
        /// <value>
        /// The timestamp of the <see cref="Message"/> that was logged.
        /// </value>
        public DateTime Timestamp
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the log level of the <see cref="Message"/> that was logged.
        /// </summary>
        /// <value>
        /// The log level of the <see cref="Message"/> that was logged.
        /// </value>
        public LogLevel Level
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the message that was logged.
        /// </summary>
        /// <value>
        /// The message that was logged.
        /// </value>
        public string Message
        {
            get;
            private set;
        }
    }
}
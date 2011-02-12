using System;

namespace WebDAVSharp
{
    /// <summary>
    /// This implements <see cref="ILogger"/> by firing events for logged messages.
    /// </summary>
    public sealed class EventBasedLogger : ILogger
    {
        /// <summary>
        /// Logs the specified message with the specified log level to the log destination
        /// implemented by the implementor of <see cref="ILogger"/>.
        /// </summary>
        /// <param name="level">The <see cref="LogLevel"/> of the message.</param>
        /// <param name="message">The message to log.</param>
        public void Log(LogLevel level, string message)
        {
            OnMessageLogged(new LogEventArgs(DateTime.Now, level, message));
        }

        /// <summary>
        /// Raises the <see cref="MessageLogged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="WebDAVSharp.LogEventArgs"/> instance containing the event data.</param>
        private void OnMessageLogged(LogEventArgs e)
        {
            if (MessageLogged != null)
                MessageLogged(this, e);
        }

        /// <summary>
        /// This event is fired whenever a message is logged to this <see cref="EventBasedLogger"/>.
        /// </summary>
        public event EventHandler<LogEventArgs> MessageLogged;
    }
}
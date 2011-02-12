namespace WebDAVSharp
{
    /// <summary>
    /// This interface must be implemented by classes that will serve as logging
    /// implementations for the WebDAV system.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs the specified message with the specified log level to the log destination
        /// implemented by the implementor of <see cref="ILogger"/>.
        /// </summary>
        /// <param name="level">
        /// The <see cref="LogLevel"/> of the message.
        /// </param>
        /// <param name="message">
        /// The message to log.
        /// </param>
        void Log(LogLevel level, string message);
    }
}
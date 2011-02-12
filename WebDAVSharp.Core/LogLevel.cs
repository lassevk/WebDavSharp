namespace WebDAVSharp
{
    /// <summary>
    /// This enum is used by <see cref="ILogger"/> to indicate the level of a logged message.
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// The logged message is a debug message. This type of message is only interesting during development and
        /// debugging/troubleshooting.
        /// </summary>
        Debug,

        /// <summary>
        /// The logged message is an informational message. This is the type of messages you would show in a typical
        /// GUI for the software.
        /// </summary>
        Informational,

        /// <summary>
        /// The logged message is a warning message. This type of message usually indicates some kind of non-catastrophic
        /// problem.
        /// </summary>
        Warning,

        /// <summary>
        /// The logged message is an error message. This type of message usually indicates some kind of catastrophic
        /// problem.
        /// </summary>
        Error,
    }
}
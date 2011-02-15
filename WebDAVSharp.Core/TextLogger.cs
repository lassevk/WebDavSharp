using System;
using System.Collections.Generic;

namespace WebDAVSharp
{
    /// <summary>
    /// This is a base class that implements basic filtering and formatting for pure text-based log destinations.
    /// </summary>
    public abstract class TextLoggerBase : ILogger
    {
        private readonly Predicate<LogEventArgs> _Filter;
        private readonly Func<LogEventArgs, string> _Formatter;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextLoggerBase"/> class.
        /// </summary>
        /// <param name="filter">
        /// The predicate to filter the log messages through;
        /// or <c>null</c> if no filter should be used (will log everything but <see cref="LogLevel.Debug"/> messages.)
        /// </param>
        /// <param name="formatter">
        /// The function to format the log messages with;
        /// or <c>null</c> if the default log formatter should be used.
        /// </param>
        protected TextLoggerBase(Predicate<LogEventArgs> filter, Func<LogEventArgs, string> formatter)
        {
            _Filter = filter ?? (entry => entry.Level != LogLevel.Debug);
            _Formatter = formatter ?? (entry => entry.ToString());
        }

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
        public void Log(LogLevel level, string message)
        {
            var entry = new LogEventArgs(DateTime.Now, level, message);
            if (!_Filter(entry))
                return;

            Log(_Formatter(entry));
        }

        /// <summary>
        /// When implemented by descendant classes, logs the specified formatted string to the log destination.
        /// </summary>
        /// <param name="message">
        /// The formatted message to write to the text output.
        /// </param>
        protected abstract void Log(string message);
    }
}
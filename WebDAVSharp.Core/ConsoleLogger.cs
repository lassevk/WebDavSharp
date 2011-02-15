using System;
using System.Collections.Generic;
using System.IO;

namespace WebDAVSharp
{
    /// <summary>
    /// This implements <see cref="ILogger"/> by writing to the console.
    /// </summary>
    public sealed class ConsoleLogger : TextLoggerBase
    {
        private readonly TextWriter _Console;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleLogger"/> class.
        /// </summary>
        /// <param name="filter">The predicate to filter the log messages through;
        /// or <c>null</c> if no filter should be used (will log everything but <see cref="LogLevel.Debug"/> messages.)</param>
        /// <param name="formatter">The function to format the log messages with;
        /// or <c>null</c> if the default log formatter should be used.</param>
        /// <param name="outputConsole">
        /// The <see cref="Console"/> to log messages to this <see cref="ConsoleLogger"/> to;
        /// or <c>null</c> to use <see cref="Console.Out"/>.
        /// </param>
        public ConsoleLogger(Predicate<KeyValuePair<LogLevel, string>> filter, Func<DateTime, LogLevel, string, string> formatter, TextWriter outputConsole = null)
            : base(filter, formatter)
        {
            _Console = outputConsole ?? Console.Out;
        }

        /// <summary>
        /// When implemented by descendant classes, logs the specified formatted string to the log destination.
        /// </summary>
        /// <param name="message">The formatted message to write to the text output.</param>
        protected override void Log(string message)
        {
            _Console.WriteLine(message);
        }
    }
}
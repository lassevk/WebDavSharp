using System;
using System.Collections.Generic;
using System.Linq;

namespace WebDAVSharp
{
    /// <summary>
    /// This implements <see cref="ILogger"/> by multicasting logged messages to multiple
    /// <see cref="ILogger"/> instances.
    /// </summary>
    public sealed class MultiLogger : ILogger
    {
        private readonly ILogger[] _Loggers;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiLogger"/> class.
        /// </summary>
        /// <param name="loggers">
        /// The <see cref="ILogger"/> instances to multicast all logged messages to.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="loggers"/> is <c>null</c>.</para>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <para><paramref name="loggers"/> is empty.</para>
        /// <para>- or -</para>
        /// <para><paramref name="loggers"/> contains a <c>null</c>-reference.</para>
        /// </exception>
        public MultiLogger(IEnumerable<ILogger> loggers)
        {
            if (loggers == null)
                throw new ArgumentNullException("loggers");

            _Loggers = loggers.ToArray();

            if (_Loggers.Length == 0)
                throw new ArgumentException("MultiLogger was passed an empty logger collection", "loggers");
            if (_Loggers.Any(l => l == null))
                throw new ArgumentException("MultiLogger was passed a logger collection that contained a null-reference", "loggers");
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
            foreach (ILogger logger in _Loggers)
                logger.Log(level, message);
        }
    }
}
using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebDAVSharp.Tests
{
    [TestFixture]
    public class TextBasedLoggerBaseTests
    {
        public class TestLogger : TextLoggerBase
        {
            public readonly List<string> Logged = new List<string>();

            public TestLogger(Predicate<KeyValuePair<LogLevel, string>> filter, Func<DateTime, LogLevel, string, string> formatter)
                : base(filter, formatter)
            {
            }

            protected override void Log(string message)
            {
                Logged.Add(message);
            }
        }

        [Test]
        public void DefaultFilter_DebugLevel_DoesNotLog()
        {
            var logger = new TestLogger(null, null);

            logger.Log(LogLevel.Debug, "TEST");

            Assert.That(logger.Logged, Is.Empty);
        }

        [TestCase(LogLevel.Informational)]
        [TestCase(LogLevel.Warning)]
        [TestCase(LogLevel.Error)]
        public void DefaultFilter_NotDebugLevel_DoesLog(LogLevel input)
        {
            var logger = new TestLogger(null, null);

            logger.Log(input, "TEST");

            Assert.That(logger.Logged, Is.Not.Empty);
        }
    }
}
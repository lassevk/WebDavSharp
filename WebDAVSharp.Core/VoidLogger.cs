namespace WebDAVSharp
{
    internal sealed class VoidLogger : ILogger
    {
        public void Log(LogLevel level, string message)
        {
        }
    }
}
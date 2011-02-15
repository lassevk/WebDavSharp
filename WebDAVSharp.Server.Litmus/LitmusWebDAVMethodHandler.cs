using WebDAVSharp.Server.Adapters;
using WebDAVSharp.Server.MethodHandlers;
using WebDAVSharp.Server.Stores;

namespace WebDAVSharp.Server.Litmus
{
    public class LitmusWebDAVMethodHandler : IWebDAVMethodHandler
    {
        private readonly IWebDAVMethodHandler _Handler;

        public LitmusWebDAVMethodHandler(IWebDAVMethodHandler handler)
        {
            _Handler = handler;
        }

        public string[] Names
        {
            get
            {
                return _Handler.Names;
            }
        }

        public void ProcessRequest(WebDAVServer server, IHttpListenerContext context, IWebDAVStore store, ILogger logger)
        {
            logger.Log(LogLevel.Debug, "------- " + context.Request.Headers["X-litmus"] + " -------");
            _Handler.ProcessRequest(server, context, store, logger);
            logger.Log(LogLevel.Debug, "------- " + context.Request.Headers["X-litmus"] + " -------");
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using WebDAVSharp.Server.Adapters;
using WebDAVSharp.Server.Exceptions;
using WebDAVSharp.Server.MethodHandlers;
using WebDAVSharp.Server.Stores;

namespace WebDAVSharp.Server
{
    /// <summary>
    /// This class implements the core WebDAV server.
    /// </summary>
    public class WebDAVServer : WebDAVDisposableBase
    {
        private readonly IHttpListener _Listener;
        private readonly bool _OwnsListener;
        private readonly IWebDAVStore _Store;
        private readonly Dictionary<string, IWebDAVMethodHandler> _MethodHandlers;
        private readonly ILogger _Logger;
        private readonly object _ThreadLock = new object();

        private ManualResetEvent _StopEvent;
        private Thread _Thread;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebDAVServer"/> class.
        /// </summary>
        /// <param name="store">
        /// The <see cref="IWebDAVStore"/> store object that will provide
        /// collections and documents for this <see cref="WebDAVServer"/>.
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/> object that log messages will be sent to;
        /// or <c>null</c> to not use a logger.
        /// </param>
        /// <param name="listener">
        /// The <see cref="IHttpListener"/> object that will handle the web server portion of
        /// the WebDAV server; or <c>null</c> to use a fresh one.
        /// </param>
        /// <param name="methodHandlers">
        /// A collection of HTTP method handlers to use by this <see cref="WebDAVServer"/>;
        /// or <c>null</c> to use the built-in method handlers.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="listener"/> is <c>null</c>.</para>
        /// <para>- or -</para>
        /// <para><paramref name="store"/> is <c>null</c>.</para>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <para><paramref name="methodHandlers"/> is empty.</para>
        /// <para>- or -</para>
        /// <para><paramref name="methodHandlers"/> contains a <c>null</c>-reference.</para>
        /// </exception>
        public WebDAVServer(IWebDAVStore store, ILogger logger = null, IHttpListener listener = null, IEnumerable<IWebDAVMethodHandler> methodHandlers = null)
        {
            if (store == null)
                throw new ArgumentNullException("store");
            if (listener == null)
            {
                listener = new HttpListenerAdapter();
                _OwnsListener = true;
            }
            methodHandlers = methodHandlers ?? WebDAVMethodHandlers.BuiltIn;
            if (!methodHandlers.Any())
                throw new ArgumentException("The methodHandlers collection is empty", "methodHandlers");
            if (methodHandlers.Any(methodHandler => methodHandler == null))
                throw new ArgumentException("The methodHandlers collection contains a null-reference", "methodHandlers");

            _Listener = listener;
            _Store = store;
            var handlersWithNames =
                from methodHandler in methodHandlers
                from name in methodHandler.Names
                select new { name, methodHandler };
            _MethodHandlers = handlersWithNames.ToDictionary(v => v.name, v => v.methodHandler);
            _Logger = logger ?? new VoidLogger();
        }

        /// <summary>
        /// Gets the <see cref="IHttpListener"/> that this <see cref="WebDAVServer"/> uses for
        /// the web server portion.
        /// </summary>
        public IHttpListener Listener
        {
            get
            {
                return _Listener;
            }
        }

        /// <summary>
        /// Gets the <see cref="IWebDAVStore"/> this <see cref="WebDAVServer"/> is hosting.
        /// </summary>
        public IWebDAVStore Store
        {
            get
            {
                return _Store;
            }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            lock (_ThreadLock)
            {
                if (_Thread != null)
                    Stop();
            }

            if (_OwnsListener)
                _Listener.Dispose();
        }

        /// <summary>
        /// Starts this <see cref="WebDAVServer"/> and returns once it has
        /// been started successfully.
        /// </summary>
        /// <exception cref="ObjectDisposedException">
        /// This <see cref="WebDAVServer"/> instance has been disposed of.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// The server is already running.
        /// </exception>
        public void Start()
        {
            EnsureNotDisposed();
            lock (_ThreadLock)
            {
                if (_Thread != null)
                    throw new InvalidOperationException("This WebDAVServer instance is already running, call to Start is invalid at this point");

                _StopEvent = new ManualResetEvent(false);

                _Thread = new Thread(BackgroundThreadMethod);
                _Thread.Name = "WebDAVServer.Thread";
                _Thread.Start();
            }
        }

        /// <summary>
        /// Starts this <see cref="WebDAVServer"/> and returns once it has
        /// been stopped successfully.
        /// </summary>
        /// <exception cref="ObjectDisposedException">
        /// This <see cref="WebDAVServer"/> instance has been disposed of.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// The server is not running.
        /// </exception>
        public void Stop()
        {
            EnsureNotDisposed();
            lock (_ThreadLock)
            {
                if (_Thread == null)
                    throw new InvalidOperationException("This WebDAVServer instance is not running, call to Stop is invalid at this point");

                _StopEvent.Set();
                _Thread.Join();

                _StopEvent.Close();
                _StopEvent = null;
                _Thread = null;
            }
        }

        private void BackgroundThreadMethod()
        {
            _Logger.Log(LogLevel.Informational, "WebDAVServer background thread has started");
            try
            {
                _Listener.Start();
                while (true)
                {
                    if (_StopEvent.WaitOne(0))
                        return;

                    IHttpListenerContext context = Listener.GetContext(_StopEvent);
                    if (context == null)
                    {
                        _Logger.Log(LogLevel.Debug, "Exiting thread");
                        return;
                    }

                    ThreadPool.QueueUserWorkItem(ProcessRequest, context);
                }
            }
            finally
            {
                _Listener.Stop();
                _Logger.Log(LogLevel.Informational, "WebDAVServer background thread has terminated");
            }
        }

        private void ProcessRequest(object state)
        {
            var context = (IHttpListenerContext)state;

            _Logger.Log(LogLevel.Informational, context.Request.HttpMethod + " " + context.Request.RemoteEndPoint + ": " + context.Request.Url);
            try
            {
                try
                {
                    string method = context.Request.HttpMethod;
                    IWebDAVMethodHandler methodHandler;
                    if (!_MethodHandlers.TryGetValue(method, out methodHandler))
                        throw new HttpMethodNotAllowedException(String.Format("%s ({0})", context.Request.HttpMethod));

                    context.Response.AppendHeader("DAV", "1,2,1#extend"); 
                    methodHandler.ProcessRequest(this, context, Store, _Logger);
                }
                catch (HttpException)
                {
                    throw;
                }
                catch (FileNotFoundException ex)
                {
                    throw new HttpNotFoundException(innerException: ex);
                }
                catch (DirectoryNotFoundException ex)
                {
                    throw new HttpNotFoundException(innerException: ex);
                }
                catch (NotImplementedException ex)
                {
                    throw new HttpNotImplementedException(innerException: ex);
                }
                catch (Exception ex)
                {
                    throw new HttpInternalServerException(innerException: ex);
                }
            }
            catch (HttpException ex)
            {
                context.Response.StatusCode = ex.StatusCode;
                context.Response.StatusDescription = HttpStatusCodes.GetDescription(ex.StatusCode);
                if (ex.Message != context.Response.StatusDescription)
                {
                    byte[] buffer = Encoding.UTF8.GetBytes(ex.Message);
                    context.Response.ContentEncoding = Encoding.UTF8;
                    context.Response.ContentLength64 = buffer.Length;
                    context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                }
                context.Response.Close();
            }
            finally
            {
                _Logger.Log(LogLevel.Informational, context.Response.StatusCode + " " + context.Response.StatusDescription + ": " +  context.Request.HttpMethod + " " + context.Request.RemoteEndPoint + ": " + context.Request.Url);
            }
        }
    }
}
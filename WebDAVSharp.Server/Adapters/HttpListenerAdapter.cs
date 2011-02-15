﻿using System;
using System.Net;
using System.Threading;

namespace WebDAVSharp.Server.Adapters
{
    /// <summary>
    /// This <see cref="IHttpListener"/> implementation wraps around a
    /// <see cref="HttpListener"/> instance.
    /// </summary>
    public sealed class HttpListenerAdapter : WebDAVDisposableBase, IHttpListener, IAdapter<HttpListener>
    {
        private readonly HttpListener _Listener;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpListenerAdapter"/> class.
        /// </summary>
        public HttpListenerAdapter()
        {
            _Listener = new HttpListener();
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (_Listener.IsListening)
                _Listener.Close();
        }

        /// <summary>
        /// Waits for a request to come in to the web server and returns a
        /// <see cref="IHttpListenerContext"/> adapter around it.
        /// </summary>
        /// <param name="abortEvent">
        /// A <see cref="EventWaitHandle"/> to use for aborting the wait. If this
        /// event becomes set before a request comes in, this method will return <c>null</c>.
        /// </param>
        /// <returns>
        /// A <see cref="IHttpListenerContext"/> adapter object for a request;
        /// or <c>null</c> if the wait for a request was aborted due to <paramref name="abortEvent"/> being set.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="abortEvent"/> is <c>null</c>.</para>
        /// </exception>
        public IHttpListenerContext GetContext(EventWaitHandle abortEvent)
        {
            if (abortEvent == null)
                throw new ArgumentNullException("abortEvent");

            IAsyncResult ar = _Listener.BeginGetContext(null, null);
            int index = WaitHandle.WaitAny(new[] { abortEvent, ar.AsyncWaitHandle });
            if (index == 1)
            {
                HttpListenerContext context = _Listener.EndGetContext(ar);
                return new HttpListenerContextAdapter(context);
            }

            return null;
        }

        /// <summary>
        /// Gets the internal instance that was adapted for WebDAV#.
        /// </summary>
        public HttpListener AdaptedInstance
        {
            get
            {
                return _Listener;
            }
        }

        /// <summary>
        /// Gets the Uniform Resource Identifier (<see cref="Uri"/>) prefixes handled by the
        /// adapted <see cref="HttpListener"/> object.
        /// </summary>
        public HttpListenerPrefixCollection Prefixes
        {
            get
            {
                return _Listener.Prefixes;
            }
        }

        /// <summary>
        /// Allows the adapted <see cref="HttpListener"/> to receive incoming requests.
        /// </summary>
        public void Start()
        {
            _Listener.Start();
        }

        /// <summary>
        /// Causes the adapted <see cref="HttpListener"/> to stop receiving incoming requests.
        /// </summary>
        public void Stop()
        {
            _Listener.Stop();
        }
    }
}
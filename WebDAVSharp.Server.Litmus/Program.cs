using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using WebDAVSharp.Server.Adapters;
using WebDAVSharp.Server.MethodHandlers;
using WebDAVSharp.Server.Stores.MemoryStore;

namespace WebDAVSharp.Server.Litmus
{
    public static class Program
    {
        public static void Main()
        {
            var logger = new MultiLogger(new ILogger[]
            {
                new ConsoleLogger(kvp => kvp.Key < LogLevel.Error, null, Console.Out),
                new ConsoleLogger(kvp => kvp.Key >= LogLevel.Error, null, Console.Error),
                new DebugLogger(null, null),
            });

            var methodHandlers = new List<IWebDAVMethodHandler>();
            foreach (IWebDAVMethodHandler methodHandler in WebDAVMethodHandlers.BuiltIn)
                methodHandlers.Add(new LitmusWebDAVMethodHandler(methodHandler));

            using (var server = new WebDAVServer(new WebDAVMemoryStore(), logger, new HttpListenerAdapter(), methodHandlers))
            {
                server.Listener.Prefixes.Add("http://localhost:8888/");

                var ipv4Addresses =
                    from address in Dns.GetHostEntry(Environment.MachineName).AddressList
                    where !address.IsIPv6LinkLocal && !address.IsIPv6Multicast && !address.IsIPv6SiteLocal
                    select address.ToString();
                foreach (var address in ipv4Addresses)
                    server.Listener.Prefixes.Add(string.Format(CultureInfo.InvariantCulture, "http://{0}:8888/", address));
                server.Start();

                Console.In.ReadLine();
                server.Stop();
            }
        }
    }
}
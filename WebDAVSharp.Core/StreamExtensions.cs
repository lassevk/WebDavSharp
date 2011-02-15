using System;
using System.IO;

namespace WebDAVSharp
{
    internal static class StreamExtensions
    {
        internal static long CopyTo(this Stream source, Stream target)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (target == null)
                throw new ArgumentNullException("target");

            var buffer = new byte[8192];
            int inBuffer;
            long bytesCopied = 0;
            while ((inBuffer = source.Read(buffer, 0, buffer.Length)) > 0)
            {
                target.Write(buffer, 0, inBuffer);
                bytesCopied += inBuffer;
            }
            return bytesCopied;
        }
    }
}
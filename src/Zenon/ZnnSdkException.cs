using System;

namespace Zenon
{
    public class ZnnSdkException : Exception
    {
        private const string DefaultMessage = "Zenon SDK Exception";

        public ZnnSdkException()
            : base(DefaultMessage)
        { }

        public ZnnSdkException(string message)
            : base(DefaultMessage + ": " + message)
        { }
    }
}
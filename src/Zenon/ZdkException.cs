using System;

namespace Zenon
{
    public class ZdkException : Exception
    {
        private const string DefaultMessage = "Zenon SDK Exception";

        public ZdkException()
            : base(DefaultMessage)
        { }

        public ZdkException(string message)
            : base(DefaultMessage + ": " + message)
        { }
    }
}
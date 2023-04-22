using System;

namespace Zenon.Wallet
{
    public class InvalidKeyStorePathException : Exception
    {
        public InvalidKeyStorePathException(string message)
            : base(message)
        { }
    }
}

using System;

namespace Zenon.Wallet
{
    public class WalletException : Exception
    {
        public WalletException(string message)
            : base(message)
        { }
    }
}

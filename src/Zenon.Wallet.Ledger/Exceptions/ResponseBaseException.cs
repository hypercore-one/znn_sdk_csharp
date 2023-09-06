﻿namespace Zenon.Wallet.Ledger.Exceptions
{
    public abstract class ResponseBaseException : Exception
    {
        public byte[] ResponseData { get; }

        protected ResponseBaseException(string message, byte[] responseData) : base(message)
        {
            ResponseData = responseData;
        }
    }
}
﻿namespace Zenon.Wallet.Ledger.Exceptions
{
    public class SecurityException : ResponseBaseException
    {
        public SecurityException(byte[] responseData, int returnCode)
            : base("A security exception occurred. This probably means that the user has not entered their pin, or there is no app loaded.", responseData, returnCode)
        {
        }
    }
}

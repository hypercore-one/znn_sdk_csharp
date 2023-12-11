namespace Zenon.Wallet.Ledger.Exceptions
{
    public class InvalidAPDUResponseException : ResponseBaseException
    {
        public InvalidAPDUResponseException(string message, byte[] responseData, int returnCode) : base(message, responseData, returnCode)
        {
        }
    }
}

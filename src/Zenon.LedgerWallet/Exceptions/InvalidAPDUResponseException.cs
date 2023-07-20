namespace Zenon.LedgerWallet.Exceptions
{
    public class InvalidAPDUResponseException : ResponseBaseException
    {
        public InvalidAPDUResponseException(string message, byte[] responseData) : base(message, responseData)
        {
        }
    }
}

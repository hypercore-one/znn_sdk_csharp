namespace Zenon.Wallet.Ledger.Exceptions
{
    public class ResponseException : Exception
    {
        public byte[] ResponseData { get; }

        public int ReturnCode { get; }

        public ResponseException(string message, byte[] responseData, int returnCode) : base(message)
        {
            ResponseData = responseData;
            ReturnCode = returnCode;
        }
    }
}

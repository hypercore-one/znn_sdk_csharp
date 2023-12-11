namespace Zenon.Wallet.Ledger.Exceptions
{
    public abstract class ResponseBaseException : Exception
    {
        public byte[] ResponseData { get; }

        public int ReturnCode { get; }

        protected ResponseBaseException(string message, byte[] responseData, int returnCode) : base(message)
        {
            ResponseData = responseData;
            ReturnCode = returnCode;
        }
    }
}

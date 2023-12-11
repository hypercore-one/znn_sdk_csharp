namespace Zenon.Wallet.Ledger.Responses
{
    public abstract class ResponseBase
    {
        private const int HardeningConstant = 0xff;

        public byte[] Data { get; }
        public bool IsSuccess => ReturnCode == StatusCode.Success;
        public virtual int ReturnCode { get; }

        protected ResponseBase(byte[] data)
        {
            Data = data;
            var returnCode = GetReturnCode(data);
            ReturnCode = returnCode;
        }

        public static int GetReturnCode(byte[] data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            return (data[data.Length - 2] & HardeningConstant) << 8 | data[data.Length - 1] & HardeningConstant;
        }
    }
}

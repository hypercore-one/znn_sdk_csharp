namespace Zenon.LedgerWallet.Responses
{
    public class ZenonSignatureResponse : ResponseBase
    {
        public byte[]? SignatureData { get; }

        public string? Signature { get; }

        public ZenonSignatureResponse(byte[] data)
            : base(data)
        {
            if (!IsSuccess)
            {
                return;
            }

            using (var memoryStream = new MemoryStream(data))
            {
                var size = memoryStream.ReadByte();
                SignatureData = memoryStream.ReadAllBytes(size);
                Signature = Convert.ToHexString(SignatureData);
            }
        }
    }
}

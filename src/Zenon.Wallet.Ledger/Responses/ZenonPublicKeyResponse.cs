namespace Zenon.Wallet.Ledger.Responses
{
    public class ZenonPublicKeyResponse : ResponseBase
    {
        public string? PublicKey { get; }

        public byte[]? PublicKeyData { get; }
        public override int ReturnCode
        {
            get
            {
                if (base.ReturnCode != StatusCode.Success)
                    return base.ReturnCode;

                if (Data == null || Data.Length != 35)
                    return StatusCode.WrongResponseLength;

                return base.ReturnCode;
            }
        }
        public ZenonPublicKeyResponse(byte[] data)
            : base(data)
        {
            if (!IsSuccess)
            {
                return;
            }

            using (var memoryStream = new MemoryStream(data))
            {
                var publicKeyLength = memoryStream.ReadByte();
                PublicKeyData = memoryStream.ReadAllBytes(publicKeyLength);
                PublicKey = Convert.ToHexString(PublicKeyData);
            }
        }
    }
}

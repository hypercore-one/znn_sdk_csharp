namespace Zenon.Wallet.Ledger.Responses
{
    public class ZenonVersionResponse : ResponseBase
    {
        public Version? Version { get; }

        public override int ReturnCode {
            get
            {
                if (base.ReturnCode != Constants.SuccessStatusCode)
                    return base.ReturnCode;

                if (Data == null || Data.Length != 6)
                    return Constants.WrongResponseLengthStatusCode;

                return base.ReturnCode;
            }
        }

        public ZenonVersionResponse(byte[] data)
            : base(data)
        {
            if (!IsSuccess)
            {
                return;
            }

            using (var memoryStream = new MemoryStream(data))
            {
                var major = memoryStream.ReadByte();
                var minor = memoryStream.ReadByte();
                var build = memoryStream.ReadByte();

                Version = new Version(major, minor, build);
            }
        }
    }
}

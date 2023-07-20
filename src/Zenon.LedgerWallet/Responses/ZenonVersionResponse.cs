namespace Zenon.LedgerWallet.Responses
{
    public class ZenonVersionResponse : ResponseBase
    {
        public Version? Version { get; }

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

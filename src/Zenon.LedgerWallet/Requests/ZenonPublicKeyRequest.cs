namespace Zenon.LedgerWallet.Requests
{
    public class ZenonPublicKeyRequest : AddressPathRequestBase
    {
        public override byte Argument1 => (byte)(Display ? 1 : 0);
        public override byte Argument2 => 0;
        public override byte Cla => Constants.CLA;
        public override byte Ins => Constants.ZNN_INS_GET_PUBLIC_KEY;

        public bool Display { get; }

        public ZenonPublicKeyRequest(IAddressPath path, bool display)
            : base(path, new byte[0])
        {
            Display = display;
        }
    }
}

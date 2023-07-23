namespace Zenon.Wallet.Ledger.Requests
{
    public class ZenonVersionRequest : RequestBase
    {
        public override byte Argument1 => 0;
        public override byte Argument2 => 0;
        public override byte Cla => Constants.CLA;
        public override byte Ins => Constants.ZNN_INS_GET_VERSION;

        public ZenonVersionRequest()
            : base(new byte[0])
        { }
    }
}
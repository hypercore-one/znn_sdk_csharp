namespace Zenon.LedgerWallet.Requests
{
    public class ZenonAppNameRequest : RequestBase
    {
        public override byte Argument1 => 0;
        public override byte Argument2 => 0;
        public override byte Cla => Constants.CLA;
        public override byte Ins => Constants.ZNN_INS_GET_APP_NAME;

        public ZenonAppNameRequest()
            : base(new byte[0])
        { }
    }
}
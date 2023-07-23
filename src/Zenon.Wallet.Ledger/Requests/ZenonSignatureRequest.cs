namespace Zenon.Wallet.Ledger.Requests
{
    public class ZenonSignatureRequest : AddressPathRequestBase
    {
        public override byte Argument1 => 0;
        public override byte Argument2 => 0;
        public override byte Cla => Constants.CLA;
        public override byte Ins => Constants.ZNN_INS_SIGN_TX;

        public bool SignTransaction { get; }

        public ZenonSignatureRequest(IAddressPath path, byte[] data, bool signTransaction)
            : base(path, data)
        {
            if (!signTransaction)
                throw new NotSupportedException();

            SignTransaction = signTransaction;
        }
    }
}

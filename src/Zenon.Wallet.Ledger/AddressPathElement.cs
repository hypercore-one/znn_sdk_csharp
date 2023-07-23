namespace Zenon.Wallet.Ledger
{
    public class AddressPathElement : IAddressPathElement
    {
        public uint Value { get; set; }

        public bool Harden { get; set; }
    }
}

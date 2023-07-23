namespace Zenon.Wallet.Ledger
{
    public interface IAddressPathElement
    {
        uint Value { get; }
        bool Harden { get; }
    }
}

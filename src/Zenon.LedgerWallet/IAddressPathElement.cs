namespace Zenon.LedgerWallet
{
    public interface IAddressPathElement
    {
        uint Value { get; }
        bool Harden { get; }
    }
}

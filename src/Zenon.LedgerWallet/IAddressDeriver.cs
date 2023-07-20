namespace Zenon.LedgerWallet
{
    public interface IAddressDeriver
    {
        Task<string> GetAddressAsync(IAddressPath addressPath, bool isPublicKey, bool display);
    }
}

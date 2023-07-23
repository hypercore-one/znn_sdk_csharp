namespace Zenon.Wallet.Ledger
{
    public interface IAddressDeriver
    {
        Task<string> GetAddressAsync(IAddressPath addressPath, bool isPublicKey, bool display);
    }
}

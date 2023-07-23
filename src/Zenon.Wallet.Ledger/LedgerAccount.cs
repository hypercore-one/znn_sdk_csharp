using Zenon.Model.NoM;
using Zenon.Model.Primitives;

namespace Zenon.Wallet.Ledger
{
    public class LedgerAccount : IWalletAccount
    {
        private Address? address;

        public LedgerAccount(LedgerWallet wallet, IAddressPath addressPath)
        {
            Wallet = wallet;
            AddressPath = addressPath;
        }

        private LedgerWallet Wallet { get; }
        private IAddressPath AddressPath { get; }

        public async Task<Address> GetAddressAsync(CancellationToken cancellationToken = default)
        {
            return address ??= Address.FromPublicKey(await GetPublicKeyAsync());
        }

        public async Task<byte[]> GetPublicKeyAsync(CancellationToken cancellationToken = default)
        {
            return await GetPublicKeyAsync(false, cancellationToken);
        }

        public async Task<byte[]> GetPublicKeyAsync(bool display, CancellationToken cancellationToken = default)
        {
            return await Wallet.GetPublicKeyAsync(AddressPath, display);
        }

        public Task<byte[]> SignAsync(byte[] message, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<byte[]> SignTxAsync(AccountBlockTemplate tx, CancellationToken cancellationToken = default)
        {
            return await Wallet.SignTxAsync(AddressPath, tx);
        }
    }
}

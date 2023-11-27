using Zenon.Model.NoM;
using Zenon.Model.Primitives;

namespace Zenon.Wallet.Ledger
{
    public class LedgerAccount : IWalletAccount
    {
        private Address? address;
        private byte[]? publicKey;

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
            return publicKey ??= await Wallet.GetPublicKeyAsync(AddressPath);
        }

        public async Task<byte[]> GetPublicKeyAsync(bool confirm, CancellationToken cancellationToken = default)
        {
            // Do not retrieve from cache when confirmation is explicit
            if (confirm)
                publicKey = await Wallet.GetPublicKeyAsync(AddressPath, confirm);
            return publicKey ??= await Wallet.GetPublicKeyAsync(AddressPath, confirm);
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

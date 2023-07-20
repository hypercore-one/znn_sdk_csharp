using Zenon.Model.NoM;
using Zenon.Model.Primitives;
using Zenon.Wallet;

namespace Zenon.LedgerWallet
{
    public class LedgerAccount : ISigner
    {
        private Address? address;

        public LedgerAccount(LedgerWallet wallet, IAddressPath addressPath)
        {
            Wallet = wallet;
            AddressPath = addressPath;
        }

        private LedgerWallet Wallet { get; }
        private IAddressPath AddressPath { get; }

        public async Task<Address> GetAddressAsync()
        {
            if (address == null)
            {
                address = Address.FromPublicKey(await GetPublicKeyAsync());
            }
            return address;
        }

        public async Task<byte[]> GetPublicKeyAsync()
        {
            return await GetPublicKeyAsync(false);
        }

        public async Task<byte[]> GetPublicKeyAsync(bool display)
        {
            return await Wallet.GetPublicKeyAsync(AddressPath, display);
        }

        public Task<byte[]> SignAsync(byte[] message)
        {
            throw new NotImplementedException();
        }

        public async Task<byte[]> SignTxAsync(AccountBlockTemplate tx)
        {
            return await Wallet.SignTxAsync(AddressPath, tx);
        }
    }
}

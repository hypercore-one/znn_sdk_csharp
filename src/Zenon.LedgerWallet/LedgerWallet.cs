using Zenon.LedgerWallet.Requests;
using Zenon.LedgerWallet.Responses;
using Zenon.Model.NoM;
using Zenon.Utils;
using Zenon.Wallet;

namespace Zenon.LedgerWallet
{
    public class LedgerWallet : IWallet, IDisposable
    {
        private bool disposed;

        public static LedgerWallet Connect(string path)
        {
            return new LedgerWallet(new LedgerTransport(new HidDevice(path, nameof(LedgerWallet))));
        }

        public static LedgerWallet Connect(ushort vendorId, ushort proudctId)
        {
            return new LedgerWallet(new LedgerTransport(new HidDevice(vendorId, proudctId, nameof(LedgerWallet))));
        }

        public static LedgerWallet Connect(ushort vendorId, ushort proudctId, string serialNumber)
        {
            return new LedgerWallet(new LedgerTransport(new HidDevice(vendorId, proudctId, serialNumber, nameof(LedgerWallet))));
        }

        private LedgerTransport Transport { get; }

        private LedgerWallet(LedgerTransport transport)
        {
            Transport = transport;
        }

        public async Task<LedgerAccount> GetAccountAsync(int index = 0)
        {
            return await Task.Run(() =>
            {
                return new LedgerAccount(this,
                    AddressPathBase.Parse<BIP44AddressPath>(Derivation.GetDerivationAccount(index)));
            });
        }

        async Task<IWalletAccount> IWallet.GetAccountAsync(int index)
        {
            return await GetAccountAsync(index);
        }

        public async Task<Version> GetVersionAsync()
        {
            var response = await Transport
                .SendRequestAsync<ZenonVersionResponse, ZenonVersionRequest>(new ZenonVersionRequest());

            if (response.IsSuccess)
            {
                return response.Version!;
            }

            throw Helpers.HandleErrorResponse(response);
        }

        public async Task<string> GetAppNameAsync()
        {
            var response = await Transport
                .SendRequestAsync<ZenonAppNameResponse, ZenonAppNameRequest>(new ZenonAppNameRequest());

            if (response.IsSuccess)
            {
                return response.AppName!;
            }

            throw Helpers.HandleErrorResponse(response);
        }

        public async Task<byte[]> GetPublicKeyAsync(IAddressPath addressPath, bool display)
        {
            var response = await Transport
                .SendRequestAsync<ZenonPublicKeyResponse, ZenonPublicKeyRequest>(new ZenonPublicKeyRequest(addressPath, display));

            if (response.IsSuccess)
            {
                return response.PublicKeyData!;
            }

            throw Helpers.HandleErrorResponse(response);
        }

        public async Task<byte[]> SignTxAsync(IAddressPath addressPath, AccountBlockTemplate transaction)
        {
            var txData = BlockUtils.GetTransactionBytes(transaction);

            var response = await Transport
                .SendRequestAsync<ZenonSignatureResponse, ZenonSignatureRequest>(new ZenonSignatureRequest(addressPath, txData, true));

            if (response.IsSuccess)
            {
                return response.SignatureData!;
            }

            throw Helpers.HandleErrorResponse(response);
        }

        private void AssertDisposed()
        {
            if (disposed)
                throw new ObjectDisposedException(nameof(HidDevice));
        }

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool dispose)
        {
            if (disposed)
                return;

            disposed = true;
            Transport.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}

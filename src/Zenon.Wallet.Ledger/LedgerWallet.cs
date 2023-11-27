using Zenon.Model.NoM;
using Zenon.Utils;
using Zenon.Wallet.Ledger.Requests;
using Zenon.Wallet.Ledger.Responses;

namespace Zenon.Wallet.Ledger
{
    public class LedgerWallet : IWallet, IDisposable
    {
        private bool disposed;

        public static LedgerWallet Connect(string path, LedgerWalletOptions options)
        {
            return new LedgerWallet(new LedgerTransport(new HidDevice(path, nameof(LedgerWallet))), options);
        }

        public static LedgerWallet Connect(ushort vendorId, ushort proudctId, LedgerWalletOptions options)
        {
            return new LedgerWallet(new LedgerTransport(new HidDevice(vendorId, proudctId, nameof(LedgerWallet))), options);
        }

        public static LedgerWallet Connect(ushort vendorId, ushort proudctId, string serialNumber, LedgerWalletOptions options)
        {
            return new LedgerWallet(new LedgerTransport(new HidDevice(vendorId, proudctId, serialNumber, nameof(LedgerWallet))), options);
        }

        private LedgerTransport Transport { get; }
        private LedgerWalletOptions Options { get; }

        private LedgerWallet(LedgerTransport transport, LedgerWalletOptions options)
        {
            Transport = transport;
            Options = new LedgerWalletOptions()
            {
                ConfirmAddressByDefault = options.ConfirmAddressByDefault
            };
        }

        public async Task<LedgerAccount> GetAccountAsync(int index = 0)
        {
            AssertDisposed();

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
            AssertDisposed();

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
            AssertDisposed();

            var response = await Transport
                .SendRequestAsync<ZenonAppNameResponse, ZenonAppNameRequest>(new ZenonAppNameRequest());

            if (response.IsSuccess)
            {
                return response.AppName!;
            }

            throw Helpers.HandleErrorResponse(response);
        }

        public async Task<byte[]> GetPublicKeyAsync(IAddressPath addressPath, bool? confirm = null)
        {
            AssertDisposed();

            confirm ??= Options.ConfirmAddressByDefault;

            var response = await Transport
                .SendRequestAsync<ZenonPublicKeyResponse, ZenonPublicKeyRequest>(new ZenonPublicKeyRequest(addressPath, confirm.Value));

            if (response.IsSuccess)
            {
                return response.PublicKeyData!;
            }

            throw Helpers.HandleErrorResponse(response);
        }

        public async Task<byte[]> SignTxAsync(IAddressPath addressPath, AccountBlockTemplate transaction)
        {
            AssertDisposed();

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
                throw new ObjectDisposedException(nameof(LedgerWallet));
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
        }
    }
}

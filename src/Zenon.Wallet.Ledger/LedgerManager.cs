﻿using HidApi;

namespace Zenon.Wallet.Ledger
{
    public class LedgerManager : IWalletManager, IDisposable
    {
        private const int vendorId = 0x2c97; // Ledger

        private bool disposed;

        public LedgerManager()
        { }

        public async Task<IEnumerable<IWalletDefinition>> GetWalletDefinitionsAsync(CancellationToken cancellationToken = default)
        {
            AssertDisposed();

            return Hid.Enumerate(vendorId).Select(x => new LedgerDefinition(x));
        }

        public async Task<IWallet> GetWalletAsync(IWalletDefinition walletDefinition, IWalletOptions? walletOptions = null, CancellationToken cancellationToken = default)
        {
            AssertDisposed();

            return await Task.Run(() =>
            {
                if (!(walletDefinition is LedgerDefinition))
                {
                    throw new NotSupportedException($"Unsupported wallet definition '{walletDefinition.GetType().Name}'.");
                }
                return LedgerWallet.Connect(walletDefinition.WalletId);
            }, cancellationToken);
        }

        public async Task<bool> SupportsWalletAsync(IWalletDefinition walletDefinition, CancellationToken cancellationToken = default)
        {
            AssertDisposed();

            if (walletDefinition is LedgerDefinition)
            {
                return (await GetWalletDefinitionsAsync(cancellationToken))
                    .Any(x => x.WalletId == walletDefinition.WalletId);
            }
            return false;
        }

        private void AssertDisposed()
        {
            if (disposed)
                throw new ObjectDisposedException(nameof(LedgerManager));
        }

        public void Dispose()
        {
            Dispose(false);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!disposed)
            {
                Hid.Exit();

                disposed = true;
            }
        }
    }
}
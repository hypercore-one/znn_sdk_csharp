using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Zenon.Wallet
{
    /// <summary>
    /// Represents the wallet manager for interacting with wallets.
    /// </summary>
    public interface IWalletManager
    {
        /// <summary>
        /// Gets the definition of wallets.
        /// </summary>
        Task<IEnumerable<IWalletDefinition>> GetWalletDefinitionsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a <see cref="IWallet"/> by wallet definition.
        /// </summary>
        Task<IWallet> GetWalletAsync(IWalletDefinition walletDefinition, IWalletOptions walletOptions, CancellationToken cancellationToken = default);

        /// <summary>
        /// Determines whether or not the manager supports the given wallet definition.
        /// </summary>
        /// <returns><code>true</code> if the wallet is supported; otherwise <code>false</code>.</returns>
        Task<bool> SupportsWalletAsync(IWalletDefinition walletDefinition, CancellationToken cancellationToken = default);
    }
}

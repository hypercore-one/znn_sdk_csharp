using System.Threading;
using System.Threading.Tasks;
using Zenon.Model.NoM;
using Zenon.Model.Primitives;

namespace Zenon.Wallet
{
    /// <summary>
    /// Represents the account of a wallet.
    /// </summary>
    public interface IWalletAccount
    {
        /// <summary>
        /// Gets the public key of the wallet account.
        /// </summary>
        /// <returns>The public key as an array of bytes.</returns>
        Task<byte[]> GetPublicKeyAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the <see cref="Address"/> of the wallet account.
        /// <summary>
        /// <returns>The address.</returns>
        Task<Address> GetAddressAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Signs an arbitrary message.
        /// </summary>
        /// <returns>The signature as an array of bytes.</returns>
        Task<byte[]> SignAsync(byte[] message, CancellationToken cancellationToken = default);

        /// <summary>
        /// Signs a transaction.
        /// <summary>
        /// <returns>The signature as an array of bytes.</returns>
        Task<byte[]> SignTxAsync(AccountBlockTemplate tx, CancellationToken cancellationToken = default);
    }
}

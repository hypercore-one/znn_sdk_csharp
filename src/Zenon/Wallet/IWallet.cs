using System.Threading.Tasks;

namespace Zenon.Wallet
{
    /// <summary>
    /// Represents a wallet.
    /// </summary>
    public interface IWallet
    {
        /// <summary>
        /// Gets a <see cref="IWalletAccount"/> by account index.
        /// </summary>
        Task<IWalletAccount> GetAccountAsync(int accountIndex = 0);
    }
}

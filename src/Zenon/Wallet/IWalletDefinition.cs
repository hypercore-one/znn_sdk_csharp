namespace Zenon.Wallet
{
    /// <summary>
    /// Represents the definition of a wallet.
    /// </summary>
    public interface IWalletDefinition
    {
        /// <summary>
        /// Gets the id or path of the wallet.
        /// </summary>
        string WalletId { get; }

        /// <summary>
        /// Gets the name of the wallet.
        /// </summary>
        string WalletName { get; }
    }
}

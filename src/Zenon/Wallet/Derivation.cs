namespace Zenon.Wallet
{
    /// <summary>
    /// BIP44 https://github.com/bitcoin/bips/blob/master/bip-0044.mediawiki
    /// </summary>
    /// <remarks>
    /// m / purpose' / coin_type' / account' / change / address_index
    /// </remarks>
    public static class Derivation
    {
        public const string CoinType = "73404";
        public const string DerivationPath = $"m/44'/{CoinType}'";

        public static string GetDerivationAccount(int account = 0) => $"{DerivationPath}/{account}'";
    }
}

using System.IO;

namespace Zenon.Wallet
{
    public class KeyStoreDefinition : IWalletDefinition
    {
        public KeyStoreDefinition(string keyStorePath)
        {
            if (!File.Exists(keyStorePath))
            {
                throw new WalletException(
                    $"Given keyStore does not exist ({keyStorePath})");
            }

            this.WalletId = keyStorePath;
            this.WalletName = Path.GetFileNameWithoutExtension(keyStorePath);
        }

        public string WalletId { get; }

        public string WalletName { get; }

        public override bool Equals(object obj)
        {
            return obj is KeyStoreDefinition &&
                ((KeyStoreDefinition)obj).WalletId.Equals(WalletId);
        }

        public override int GetHashCode()
        {
            return WalletId.GetHashCode();
        }

        public override string ToString()
        {
            return WalletId;
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Zenon.Wallet.Json;

namespace Zenon.Wallet
{
    public class KeyStoreManager : IWalletManager
    {
        public KeyStoreManager()
            : this(ZdkPaths.Default.Wallet)
        { }

        public KeyStoreManager(string walletPath)
        {
            WalletPath = walletPath;
        }

        public string WalletPath { get; }

        public KeyStoreDefinition SaveKeyStore(KeyStore store, string password, string name)
        {
            name = name ?? store.GetKeyPair(0).Address.ToString();

            var encrypted = KeyFile.Encrypt(store, password);
            var filePath = Path.Join(WalletPath, name);
            Directory.CreateDirectory(WalletPath);
            File.WriteAllText(filePath, JsonConvert.SerializeObject(encrypted.ToJson()));
            return new KeyStoreDefinition(filePath);
        }

        public KeyStore ReadKeyStore(string password, string keyStorePath)
        {
            if (!File.Exists(keyStorePath))
            {
                throw new InvalidKeyStorePathException(
                    $"Given keyStore does not exist ({keyStorePath})");
            }

            var content = File.ReadAllText(keyStorePath);
            return new KeyFile(JsonConvert.DeserializeObject<JKeyFile>(content)).Decrypt(password);
        }

        public KeyStoreDefinition FindKeyStore(string name)
        {
            return ListAllKeyStores()
                .FirstOrDefault(x => x.WalletName == name);
        }

        public IEnumerable<KeyStoreDefinition> ListAllKeyStores()
        {
            if (Directory.Exists(WalletPath))
                return Directory.GetFiles(WalletPath).Select(x => new KeyStoreDefinition(x));
            return new KeyStoreDefinition[0];
        }

        public KeyStoreDefinition CreateNew(string passphrase, string name)
        {
            var store = KeyStore.NewRandom();
            return SaveKeyStore(store, passphrase, name: name);
        }

        public KeyStoreDefinition CreateFromMnemonic(
            string mnemonic, string passphrase, string name)
        {
            var store = KeyStore.FromMnemonic(mnemonic);
            return SaveKeyStore(store, passphrase, name: name);
        }

        public async Task<IEnumerable<IWalletDefinition>> GetWalletDefinitionsAsync(CancellationToken cancellationToken = default)
        {
            return await Task.Run(() =>
            {
                return ListAllKeyStores();
            }, cancellationToken);
        }

        public async Task<IWallet> GetWalletAsync(IWalletDefinition walletDefinition, IWalletOptions walletOptions, CancellationToken cancellationToken = default)
        {
            return await Task.Run(() =>
            {
                if (!(walletDefinition is KeyStoreDefinition))
                {
                    throw new NotSupportedException($"Unsupported wallet definition '{walletDefinition.GetType().Name}'.");
                }
                if (!(walletOptions is KeyStoreOptions))
                {
                    throw new NotSupportedException($"Unsupported wallet options '{walletOptions.GetType().Name}'.");
                }
                return ReadKeyStore(
                    ((KeyStoreOptions)walletOptions).DecryptionPassword,
                    ((KeyStoreDefinition)walletDefinition).WalletId);
            }, cancellationToken);
        }

        public async Task<bool> SupportsWalletAsync(IWalletDefinition walletDefinition, CancellationToken cancellationToken = default)
        {
            if (walletDefinition is KeyStoreDefinition)
            {
                return (await GetWalletDefinitionsAsync(cancellationToken))
                    .Any(x => x.WalletId == walletDefinition.WalletId);
            }
            return false;
        }
    }
}
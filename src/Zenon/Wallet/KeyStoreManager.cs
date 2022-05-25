using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using Zenon.Wallet.Json;

namespace Zenon.Wallet
{
    public class KeyStoreManager
    {
        public KeyStoreManager(string walletPath)
        {
            WalletPath = walletPath;
        }

        public string WalletPath { get; }

        public KeyStore KeyStoreInUse { get; set; }

        public string SaveKeyStore(KeyStore store, string password, string name)
        {
            name = name ?? store.GetKeyPair(0).Address.ToString();

            var encrypted = KeyFile.Encrypt(store, password);
            var filePath = Path.Join(WalletPath, name);
            File.WriteAllText(filePath, JsonConvert.SerializeObject(encrypted.ToJson()));
            return filePath;
        }

        public string GetMnemonicInUse()
        {
            if (KeyStoreInUse == null)
            {
                throw new ArgumentNullException("The keyStore in use is null");
            }
            return KeyStoreInUse.Mnemonic;
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

        public string FindKeyStore(string name)
        {
            return new DirectoryInfo(WalletPath)
                .GetFiles()
                .FirstOrDefault(x => x.Name == name).FullName;
        }

        public string[] ListAllKeyStores() 
        {
            return Directory.GetFiles(WalletPath);
        }

        public string CreateNew(string passphrase, string name) 
        {
            var store = KeyStore.NewRandom();
            return SaveKeyStore(store, passphrase, name: name);
        }

        public string CreateFromMnemonic(
            string mnemonic, string passphrase, string name) 
        {
            var store = KeyStore.FromMnemonic(mnemonic);
            return SaveKeyStore(store, passphrase, name: name);
        }
    }
}
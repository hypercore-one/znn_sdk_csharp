using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using Zenon.Model.Primitives;
using Zenon.Utils;
using Zenon.Wallet.BIP39;
using Zenon.Wallet.Json;

namespace Zenon.Wallet
{
    public class KeyStore
    {
        public static KeyStore FromMnemonic(string mnemonic)
        {
            var bip39 = new BIP39.BIP39();

            if (!bip39.ValidateMnemonic(mnemonic, BIP39Wordlist.English))
            {
                throw new ArgumentException("Invalid mnemonic.", "mnemonic");
            }

            var entropy = bip39.MnemonicToEntropy(mnemonic, BIP39Wordlist.English);
            var seed = bip39.MnemonicToSeedHex(mnemonic, "");

            return new KeyStore(mnemonic, entropy, seed);
        }

        public static KeyStore FromSeed(string seed)
        {
            return new KeyStore(null, null, seed);
        }

        public static KeyStore FromEntropy(string entropy)
        {
            return FromMnemonic(new BIP39.BIP39().EntropyToMnemonic(entropy, BIP39Wordlist.English));
        }

        public static KeyStore NewRandom()
        {
            using (var generator = RandomNumberGenerator.Create())
            {
                var entropy = new byte[32];
                generator.GetBytes(entropy);

                return FromEntropy(BytesUtils.ToHexString(entropy));
            };
        }

        private KeyStore(string mnemonic, string entropy, string seed)
        {
            this.Mnemonic = mnemonic;
            this.Entropy = entropy;
            this.Seed = seed;
        }

        public string Mnemonic { get; }
        public string Entropy { get; }
        public string Seed { get; }

        public KeyPair GetKeyPair(int index = 0)
        {
            return new KeyPair(Crypto.Crypto.DeriveKey(Derivation.GetDerivationAccount(index), BytesUtils.FromHexString(this.Seed)));
        }

        public Address[] DeriveAddressesByRange(int left, int right)
        {
            var addresses = new List<Address>();
            if (this.Seed != null)
            {
                for (var i = left; i < right; i++)
                {
                    addresses.Add(this.GetKeyPair(i).Address);
                }
            }
            return addresses.ToArray();
        }

        public FindResponse FindAddress(Address address, int numOfAddresses)
        {
            for (var i = 0; i < numOfAddresses; i++)
            {
                var pair = this.GetKeyPair(i);
                if (pair.Address.Equals(address))
                {
                    return new FindResponse(path: null, index: i, keyPair: pair);
                }
            }
            return null;
        }
    }

    public class FindResponse
    {
        public FindResponse(JFindResponse json)
        {
            this.Path = new FileInfo(json.keyStore);
            this.Index = json.index;
        }

        public FindResponse(FileInfo path, int index, KeyPair keyPair)
        {
            this.Path = path;
            this.Index = index;
            this.KeyPair = keyPair;
        }

        public FileInfo Path { get; }
        public int Index { get; }
        public KeyPair KeyPair { get; }

        public JFindResponse ToJson()
        {
            return new JFindResponse()
            {
                keyStore = this.Path.FullName,
                index = this.Index
            };
        }
    }
}

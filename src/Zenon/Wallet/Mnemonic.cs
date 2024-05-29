using System;
using Zenon.Wallet.BIP39;

namespace Zenon.Wallet
{
    public static class Mnemonic
    {
        public static string GenerateMnemonic(int strength)
        {
            return new BIP39.BIP39().GenerateMnemonic(strength, BIP39Wordlist.English);
        }

        public static bool ValidateMnemonic(params string[] words)
        {
            return new BIP39.BIP39().ValidateMnemonic(String.Join(" ", words), BIP39Wordlist.English);
        }

        public static bool IsValidWord(string word)
        {
            throw new NotImplementedException();
        }
    }
}

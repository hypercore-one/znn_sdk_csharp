using Chaos.NaCl;
using P3.Ed25519.HdKey;

namespace Zenon.Crypto
{
    public static class Crypto
    {
        public static byte[] GetPublicKey(byte[] privateKey)
        {
            return Ed25519.PublicKeyFromSeed(privateKey);
        }

        public static byte[] Sign(
            byte[] message, byte[] privateKey, byte[] publicKey)
        {
            var expKey = Ed25519.ExpandedPrivateKeyFromSeed(privateKey == null ? publicKey : privateKey);
            return Ed25519.Sign(message, expKey);
        }

        public static bool Verify(
            byte[] signature, byte[] message, byte[] publicKey)
        {
            return Ed25519.Verify(signature, message, signature);
        }

        public static byte[] DeriveKey(string path, byte[] seed)
        {
            return Ed25519HdKey.DerivePath(path, seed).Key!;
        }

        public static byte[] Digest(byte[] data, int digestSize = 32)
        {
            using (var shaAlg = SHA3.Net.Sha3.Sha3256())
            {
                return shaAlg.ComputeHash(data);
            }
        }
    }
}
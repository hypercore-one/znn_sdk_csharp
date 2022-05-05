using NSec.Cryptography;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Zenon.Crypto
{
    public static class Crypto
    {
        /*
        private static async Task<List<ushort>> ed25519HashFunc(List<ushort> m) 
        {
            final sink = const DartSha512().newHashSink();
                sink.add(m!);
            sink.close();
            var hash = await sink.hash();
            return Uint8List.fromList(hash.bytes);
        }
        */

        public static Task<List<byte>> getPublicKey(List<byte> privateKey)
        {
            return Task.Run(() =>
            {
                var algorithm = SignatureAlgorithm.Ed25519;
                using var key = Key.Import(algorithm, privateKey.ToArray(), KeyBlobFormat.RawPrivateKey);

                return new List<byte>(key.PublicKey.Export(KeyBlobFormat.RawPublicKey));
            });
        }

        public static async Task<List<byte>> Sign(
            List<byte> message, List<byte> privateKey, List<byte> publicKey)
        {
            return await Task.Run(() =>
            {
                var algorithm = SignatureAlgorithm.Ed25519;
                using var key = Key.Import(algorithm, privateKey.ToArray(), KeyBlobFormat.RawPrivateKey);

                return new List<byte>(algorithm.Sign(key, message.ToArray()));
            });
        }

        public static async Task<bool> Verify(
            List<byte> signature, List<byte> message, List<byte> publicKey)
        {
            return await Task.Run(() =>
            {
                var algorithm = SignatureAlgorithm.Ed25519;
                var key = PublicKey.Import(algorithm, publicKey.ToArray(), KeyBlobFormat.RawPublicKey);

                return SignatureAlgorithm.Ed25519.Verify(key, message.ToArray(), signature.ToArray());
            });
        }

        /*
        static List<int> deriveKey(String path, String seed)
        {
            return ed25519.Ed25519.derivePath(path, seed).key!;
        }
        */

        public static byte[] Digest(byte[] data, int digestSize = 32)
        {
            using (var shaAlg = SHA3.Net.Sha3.Sha3256())
            {
                return shaAlg.ComputeHash(data);
            }
        }
    }
}
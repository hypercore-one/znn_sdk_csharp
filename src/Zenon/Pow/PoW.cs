using System;
using System.Numerics;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Zenon.Model.Primitives;
using Zenon.Utils;

namespace Zenon.Pow
{
    public static class PoW
    {
        private const int OutSize = 8;
        private const int InSize = 32;
        private const int DataSize = 40;
        private static readonly SHA3.Net.Sha3 shaAlg = SHA3.Net.Sha3.Sha3256();

        public static async Task<string> Generate(Hash hash, long difficulty)
        {
            return await Task.Run(() =>
            {
                return BytesUtils.ToHexString(GenerateInternal(hash.Bytes, difficulty));
            });
        }

        public static async Task<string> Benchmark(long difficulty)
        {
            return await Task.Run(() =>
            {
                return BytesUtils.ToHexString(BenchmarkInternal(difficulty));
            });
        }

        private static byte[] GenerateInternal(byte[] hash, long difficulty)
        {
            var target = GetTarget(difficulty);
            var entropy = GetRandomSeed();
            var data = GetData(entropy, hash);
            byte[] h = new byte[OutSize];
            while (true)
            {
                Hash(h, data);

                if (Greater(h, target))
                {
                    return DataToNonce(data);
                }

                if (!NextData(data, entropy.Length))
                {
                    data = GetData(GetRandomSeed(), hash);
                }
            }
        }

        private static byte[] BenchmarkInternal(long difficulty)
        {
            var target = GetTarget(difficulty);
            var data = GetData(new byte[OutSize], new byte[InSize]);
            byte[] h = new byte[OutSize];
            while (true)
            {
                Hash(h, data);

                if (Greater(h, target))
                {
                    return DataToNonce(data);
                }

                if (!NextData(data, OutSize))
                {
                    data = new byte[OutSize];
                }
            }
        }

        private static void Hash(byte[] hash, byte[] data)
        {
            shaAlg.Initialize();
            var digest = shaAlg.ComputeHash(data);
            Array.Copy(digest, hash, 8);
        }

        private static byte[] GetTarget(long difficulty)
        {
            // set big to 1 << 64
            var big = new BigInteger(1L << 62);
            big *= 4;

            var x = big / difficulty;
            x = big - x;

            var h = new byte[OutSize];
            // set little ending encoding
            for (int i = 0; i < 8; i += 1)
            {
                h[i] = (byte)((x >> (i * 8)) & 255);
            }
            return h;
        }

        private static bool NextData(byte[] data, int max_size)
        {
            for (int i = 0; i < max_size; i += 1)
            {
                data[i] += 1;
                if (data[i] != 0)
                {
                    return true;
                }
            }
            return false;
        }

        private static bool Greater(byte[] a, byte[] b)
        {
            for (int i = 7; i >= 0; i -= 1)
            {
                if (a[i] == b[i])
                {
                    continue;
                }
                return a[i] > b[i];
            }
            return true;
        }

        private static byte[] GetRandomSeed()
        {
            return RandomNumberGenerator.GetBytes(OutSize);
        }

        private static byte[] GetData(byte[] entropy, byte[] hash)
        {
            byte[] data = new byte[DataSize];

            for (int i = 0; i < entropy.Length; i += 1)
            {
                data[i] = entropy[i];
            }

            for (int i = 0; i < hash.Length; i += 1) 
            {
                data[i + entropy.Length] = hash[i] ;
            }

            return data;
        }

        private static byte[] DataToNonce(byte[] data)
        {
            byte[] hash = new byte[8];
            for (int i = 0; i < hash.Length; i += 1)
            {
                hash[i] = data[i];
            }
            return hash;
        }
    }
}

using System;
using System.Linq;
using System.Numerics;

namespace Zenon.Utils
{
    public static class BytesUtils
    {
        public static BigInteger DecodeBigInt(byte[] bytes, bool unsigned = true)
        {
            return new BigInteger(bytes, unsigned, true);
            
        }

        public static byte[] EncodeBigInt(BigInteger value, bool unsigned = true)
        {
            return value.ToByteArray(unsigned, true);
            
        }

        public static byte[] BigIntToBytes(BigInteger b, int numBytes)
        {
            var bytes = new byte[numBytes];
            var biBytes = EncodeBigInt(b, true);
            var start = (biBytes.Length == numBytes + 1) ? 1 : 0;
            var length = Math.Min(biBytes.Length, numBytes);
            Buffer.BlockCopy(biBytes, start, bytes, numBytes - length, length);
            return bytes;
        }

        public static byte[] BigIntToBytesSigned(BigInteger b, int numBytes)
        {
            var bytes = b.Sign < 0 ? Enumerable.Repeat((byte)0xFF, numBytes).ToArray() : new byte[numBytes];
            var biBytes = EncodeBigInt(b, false);
            var start = (biBytes.Length == numBytes + 1) ? 1 : 0;
            var length = Math.Min(biBytes.Length, numBytes);
            Buffer.BlockCopy(biBytes, start, bytes, numBytes - length, length);
            return bytes;
        }

        public static BigInteger BytesToBigInt(byte[] bb)
        {
            return (bb.Length == 0) ? BigInteger.Zero : DecodeBigInt(bb);
        }

        public static byte[] LeftPadBytes(byte[] bytes, int size)
        {
            if (bytes.Length >= size)
            {
                return bytes;
            }
            var result = new byte[size];
            Buffer.BlockCopy(bytes, 0, result, size - bytes.Length, bytes.Length);
            return result;
        }
    }
}

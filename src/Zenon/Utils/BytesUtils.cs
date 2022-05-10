using System;
using System.Linq;
using System.Numerics;

namespace Zenon.Utils
{
    public static class BytesUtils
    {
        public static BigInteger DecodeBigInt(byte[] bytes)
        {
            return new BigInteger(bytes, true, true);
            /*
            var result = new BigInteger.Zero;
            for (var i = 0; i < bytes.Length; i++)
            {
                result *= new BigInteger(256);
                result += new BigInteger(bytes[i]);
            }
            return result;
            */
        }

        public static byte[] EncodeBigInt(BigInteger value, bool unsigned = true)
        {
            return value.ToByteArray(unsigned, true);

            // Not handling negative numbers. Decide how you want to do that.
            /*
            var size = (value.GetBitLength() + 7) >> 3;
            var result = new byte[size];
            var _byteMask = new BigInteger(0xff);
            for (var i = 0; i < size; i++)
            {
                result[size - i - 1] = (byte)(number & _byteMask);
                number = number >> 8;
            }
            return result;
            */
        }

        public static byte[] BigIntToBytes(BigInteger b, int numBytes)
        {
            var bytes = new byte[numBytes];
            var biBytes = EncodeBigInt(b);
            var start = (biBytes.Length == numBytes + 1) ? 1 : 0;
            var length = Math.Min(biBytes.Length, numBytes);
            Buffer.BlockCopy(biBytes, start, bytes, numBytes - length, length);
            return bytes;
        }

        public static byte[] BigIntToBytesSigned(BigInteger b, int numBytes)
        {
            var bytes = b.Sign < 0 ? Enumerable.Repeat((byte)0xFF, numBytes).ToArray() : new byte[numBytes];
            var biBytes = EncodeBigInt(b);
            var start = (biBytes.Length == numBytes + 1) ? 1 : 0;
            var length = Math.Min(biBytes.Length, numBytes);
            Buffer.BlockCopy(biBytes, start, bytes, numBytes - length, length);
            return bytes;
        }

        public static BigInteger BytesToBigInt(byte[] bb)
        {
            return (bb.Length == 0) ? BigInteger.Zero : DecodeBigInt(bb);
        }
    }
}

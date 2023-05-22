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

        public static byte[] GetBytes(int value)
        {
            return EndianBitConverter.Big.GetBytes(value);
        }

        public static byte[] GetBytes(long value)
        {
            return EndianBitConverter.Big.GetBytes(value);
        }

        public static string ToBase64String(byte[] data)
        {
            return Convert.ToBase64String(data);
        }

        public static byte[] FromBase64String(String data)
        {
            return Convert.FromBase64String(data);
        }

        /// <summary>
        /// Converts an array of 8-bit unsigned integers to its equivalent string representation
        /// that is encoded with lowercase or uppercase hex characters.
        /// </summary>
        /// <param name="bytes">The array of 8-bit unsigned integers to encode</param>
        /// <param name="uppercase">True to encode with uppercase hex characters; otherwise lowercase</param>
        /// <returns>The hex encoded representation of the bytes</returns>
        public static string ToHexString(byte[] bytes, bool uppercase = false)
        {
            if (uppercase)
            {
                return Convert.ToHexString(bytes);
            }
            else
            {
                return Convert.ToHexString(bytes).ToLowerInvariant();
            }
        }

        /// <summary>
        /// Converts the specified string, which encodes binary data as hex characters, to
        /// an equivalent 8-bit unsigned integer array.
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <returns>An array of 8-bit unsigned integers that is equivalent to <paramref name="value"/>.</returns>
        public static byte[] FromHexString(string value)
        {
            return Convert.FromHexString(value);
        }
    }
}

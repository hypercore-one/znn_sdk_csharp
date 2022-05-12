using System;
using System.Numerics;
using Zenon.Utils;

namespace Zenon.Abi
{
    public class UnsignedIntType : NumericType
    {
        public UnsignedIntType(string name)
            : base(name)
        { }

        public override string CanonicalName => this.Name == "uint" ? "uint256" : base.CanonicalName;

        public static byte[] EncodeInt(ulong i)
        {
            return EncodeIntBig(new BigInteger(i));
        }

        public static byte[] EncodeIntBig(BigInteger bigInt)
        {
            if (bigInt.Sign == -1)
            {
                throw new ArgumentException("Value must be unsigned", "bigInt");
            }
            return BytesUtils.BigIntToBytes(bigInt, 32);
        }

        public static BigInteger DecodeInt(byte[] encoded, int offset)
        {
            return BytesUtils.DecodeBigInt(new ArraySegment<byte>(encoded, offset, offset + 32).ToArray());
        }

        public override byte[] Encode(object value)
        {
            var bigInt = EncodeInternal(value);
            return EncodeIntBig(bigInt);
        }

        public override object Decode(byte[] encoded, int offset = 0)
        {
            return DecodeInt(encoded, offset);
        }
    }
}
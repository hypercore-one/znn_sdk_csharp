using System;
using System.Numerics;
using Zenon.Utils;

namespace Zenon.Abi
{
    public class UnsignedIntType : NumericType
    {
        public static byte[] EncodeInt(long i)
        {
            return EncodeIntBig(new BigInteger(i));
        }

        public static byte[] EncodeIntBig(BigInteger bigInt)
        {
            if (bigInt.Sign == -1)
            {
                throw new ArgumentException("Value must be unsigned", "bigInt");
            }
            return BytesUtils.BigIntToBytes(bigInt, Int32Size);
        }

        public static BigInteger DecodeInt(byte[] encoded, int offset)
        {
            return BytesUtils.DecodeBigInt(encoded.Sublist(offset, offset + Int32Size), true);
        }

        public UnsignedIntType(string name)
            : base(name)
        { }

        public override string CanonicalName => this.Name == "uint" ? "uint256" : base.CanonicalName;

        public override byte[] Encode(object value)
        {
            return EncodeIntBig(this.EncodeInternal(value));
        }

        public override object Decode(byte[] encoded, int offset = 0)
        {
            return DecodeInt(encoded, offset);
        }
    }
}
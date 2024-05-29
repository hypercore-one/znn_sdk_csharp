using System.Numerics;
using Zenon.Utils;

namespace Zenon.Abi
{
    public class IntType : NumericType
    {
        public static byte[] EncodeInt(long i)
        {
            return EncodeIntBig(new BigInteger(i));
        }

        public static byte[] EncodeIntBig(BigInteger bigInt)
        {
            return BytesUtils.BigIntToBytesSigned(bigInt, Int32Size);
        }

        public static BigInteger DecodeInt(byte[] encoded, int offset)
        {
            return BytesUtils.DecodeBigInt(encoded.Sublist(offset, offset + Int32Size), false);
        }

        public IntType(string name)
            : base(name)
        { }

        public override string CanonicalName => this.Name == "int" ? "int256" : base.CanonicalName;

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

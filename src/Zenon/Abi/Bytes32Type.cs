using System;
using System.Numerics;

namespace Zenon.Abi
{
    public class Bytes32Type : AbiType
    {
        public Bytes32Type(string name)
            : base(name)
        { }

        public override byte[] Encode(object value)
        {
            if (value is int)
            {
                return IntType.EncodeIntBig(new BigInteger((int)value));
            }
            else if (value is int)
            {
                return IntType.EncodeIntBig(new BigInteger((long)value));
            }
            else if (value is double)
            {
                return IntType.EncodeIntBig(new BigInteger((double)value));
            }
            else if (value is string)
            {
                var result = new byte[AbiType.Int32Size];
                var bytes = Convert.FromHexString((string)value);
                Buffer.BlockCopy(bytes, 0, result, 0, bytes.Length);
                return result;
            }
            else if (value is byte[])
            {
                var result = new byte[AbiType.Int32Size];
                var bytes = (byte[])value;
                Buffer.BlockCopy(bytes, 0, result, 0, bytes.Length);
                return result;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public override dynamic Decode(byte[] encoded, int offset = 0)
        {
            var result = new byte[AbiType.Int32Size];
            Buffer.BlockCopy(encoded, offset, result, 0, FixedSize);
            return result;
        }
    }
}
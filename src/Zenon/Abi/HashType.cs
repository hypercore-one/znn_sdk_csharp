using System;
using Zenon.Model.Primitives;
using Zenon.Utils;

namespace Zenon.Abi
{
    public class HashType : AbiType
    {
        public HashType(string name)
            : base(name)
        { }

        public override byte[] Encode(object value)
        {
            if (value is string)
            {
                var result = new byte[AbiType.Int32Size];
                var bytes = BytesUtils.FromHexString((string)value);
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
                return IntType.EncodeIntBig(NumericType.ToBigInt(value));
            }
        }

        public override Hash Decode(byte[] encoded, int offset = 0)
        {
            var result = new byte[AbiType.Int32Size];
            Buffer.BlockCopy(encoded, offset, result, 0, FixedSize);
            return Hash.FromBytes(result);
        }
    }
}

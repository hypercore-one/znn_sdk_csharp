using System;
using Zenon.Utils;

namespace Zenon.Abi
{
    public class BytesType : AbiType
    {
        public static readonly BytesType Bytes = new BytesType("bytes");
        
        public BytesType(string name)
            : base(name)
        { }

        public override byte[] Encode(object value)
        {
            byte[] bytes;
            if (value is byte[])
            {
                bytes = (byte[])value;
            }
            else if (value is string)
            {
                bytes = Convert.FromHexString((string)value);
            }
            else
            {
                throw new NotSupportedException($"Value type '{value.GetType().Name}' is not supported.");
            }

            var resultLength = ((int)((bytes.Length - 1) / AbiType.Int32Size) + 1) * AbiType.Int32Size;
            var result = new byte[resultLength];

            Buffer.BlockCopy(bytes, 0, result, 0, bytes.Length);

            return ArrayUtils.Concat(IntType.EncodeInt(bytes.Length), result);
        }

        public override dynamic Decode(byte[] encoded, int offset = 0)
        {
            var len = (int)IntType.DecodeInt(encoded, offset);
            if (len == 0)
            {
                return new byte[0];
            }
            offset += 32;
            var l = new byte[len];
            Buffer.BlockCopy(encoded, offset, l, 0, len);
            return l;
        }

        public override bool IsDynamicType => true;
    }
}
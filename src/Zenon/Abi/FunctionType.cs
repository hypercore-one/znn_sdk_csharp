using System;
using Zenon.Utils;

namespace Zenon.Abi
{
    public class FunctionType : Bytes32Type
    {
        public FunctionType()
            : base("function")
        { }

        public override byte[] Encode(object value)
        {
            var bytes = value as byte[];

            if (bytes == null)
                throw new ArgumentException("Value must be a byte array", "value");

            if (bytes.Length != 24)
                throw new ArgumentException("Byte array must be 24 bytes in length", "value");

            return base.Encode(ArrayUtils.Concat(bytes, new byte[8]));
        }

        public override dynamic Decode(byte[] encoded, int offset = 0)
        {
            throw new NotImplementedException();
        }
    }
}
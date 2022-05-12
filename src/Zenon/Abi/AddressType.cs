using System;
using Zenon.Model.Primitives;
using Zenon.Utils;

namespace Zenon.Abi
{
    public class AddressType : IntType
    {
        public const string DefaultName = "address";

        public AddressType()
            : base(DefaultName)
        { }

        public override byte[] Encode(object value)
        {
            if (value is string)
            {
                return BytesUtils.LeftPadBytes(Address.Parse((string)value).Bytes, 32);
            }
            if (value is Address)
            {
                return BytesUtils.LeftPadBytes(((Address)value).Bytes, 32);
            }
            throw new NotSupportedException("Value type is not supported.");
        }

        public override object Decode(byte[] encoded, int offset = 0)
        {
            var l = new byte[20];
            Buffer.BlockCopy(encoded, offset + 12, l, 0, 20);
            var a = new Address("z", l);
            return a;
        }
    }
}
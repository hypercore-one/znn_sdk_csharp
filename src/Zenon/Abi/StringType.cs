using System;
using System.Text;

namespace Zenon.Abi
{
    public class StringType : BytesType
    {
        public StringType()
            : base("string")
        { }

        public override byte[] Encode(object value)
        {
            if (!(value is string))
                throw new ArgumentException();

            return base.Encode(Encoding.UTF8.GetBytes((string)value));
        }

        public override string Decode(byte[] encoded, int offset = 0)
        {
            return Encoding.UTF8.GetString((byte[])base.Decode(encoded, offset));
        }
    }
}
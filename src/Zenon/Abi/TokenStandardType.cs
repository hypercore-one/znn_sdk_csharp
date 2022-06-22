using System;
using Zenon.Model.Primitives;
using Zenon.Utils;

namespace Zenon.Abi
{
    public class TokenStandardType : IntType
    {
        public const string DefaultName = "tokenStandard";

        public TokenStandardType()
            : base(DefaultName)
        { }

        public override byte[] Encode(object value)
        {
            if (value is string)
            {
                return BytesUtils.LeftPadBytes(TokenStandard.Parse((string)value).Bytes, Int32Size);
            }
            if (value is TokenStandard)
            {
                return BytesUtils.LeftPadBytes(((TokenStandard)value).Bytes, Int32Size);
            }

            throw new NotSupportedException($"Value type '{value.GetType().Name}' is not supported.");
        }

        public override object Decode(byte[] encoded, int offset = 0)
        {
            var result = new byte[10];
            Buffer.BlockCopy(encoded, offset + 22, result, 0, 10);
            return TokenStandard.FromBytes(result);
        }
    }
}
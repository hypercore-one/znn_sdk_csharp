using Zenon.Utils;

namespace Zenon.Abi
{
    public class DynamicArrayType : ArrayType
    {
        public DynamicArrayType(string name)
            : base(name)
        { }

        public override string CanonicalName => "[]";

        public override byte[] EncodeList(byte[] bytes)
        {
            return ArrayUtils.Concat(IntType.EncodeInt(bytes.Length), EncodeTuple(bytes));
        }

        public override object Decode(byte[] encoded, int origOffset = 0)
        {
            var len = (int)IntType.DecodeInt(encoded, origOffset);
            origOffset += 32;
            var offset = origOffset;
            var ret = new byte[len][];

            for (var i = 0; i < len; i++)
            {
                if (this.ElementType.IsDynamicType)
                {
                    ret[i] = (byte[])this.ElementType.Decode(
                        encoded, origOffset + (int)IntType.DecodeInt(encoded, offset));
                }
                else
                {
                    ret[i] = (byte[])this.ElementType.Decode(encoded, offset);
                }

                offset += this.ElementType.FixedSize;
            }

            return ret;
        }

        public override bool IsDynamicType => true;
    }
}

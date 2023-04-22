using System;
using Zenon.Utils;

namespace Zenon.Abi
{
    public class DynamicArrayType : ArrayType
    {
        public DynamicArrayType(string name)
            : base(name)
        { }

        public override string CanonicalName => $"{this.ElementType.CanonicalName}[]";

        public override byte[] EncodeList(Array l)
        {
            return ArrayUtils.Concat(IntType.EncodeInt(l.Length), EncodeTuple(l));
        }

        public override object Decode(byte[] encoded, int origOffset = 0)
        {
            var len = (int)IntType.DecodeInt(encoded, origOffset);
            origOffset += 32;
            var offset = origOffset;
            var ret = new object[len];

            for (var i = 0; i < len; i++)
            {
                if (this.ElementType.IsDynamicType)
                {
                    ret[i] = this.ElementType.Decode(
                        encoded, origOffset + (int)IntType.DecodeInt(encoded, offset));
                }
                else
                {
                    ret[i] = this.ElementType.Decode(encoded, offset);
                }

                offset += this.ElementType.FixedSize;
            }

            return ret;
        }

        public override bool IsDynamicType => true;
    }
}

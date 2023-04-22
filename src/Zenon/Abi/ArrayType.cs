using Newtonsoft.Json;
using System;
using Zenon.Utils;

namespace Zenon.Abi
{
    public abstract class ArrayType : AbiType
    {
        public new static ArrayType GetType(string typeName)
        {
            var idx1 = typeName.IndexOf('[');
            var idx2 = typeName.IndexOf(']', idx1);
            if (idx1 + 1 == idx2)
            {
                return new DynamicArrayType(typeName);
            }
            else
            {
                return new StaticArrayType(typeName);
            }
        }

        public ArrayType(String name)
            : base(name)
        {
            var idx = name.IndexOf('[');
            var st = name.Substring(0, idx);
            var idx2 = name.IndexOf(']', idx);
            var subDim = idx2 + 1 == name.Length ? "" : name.Substring(idx2 + 1);
            this.ElementType = AbiType.GetType(st + subDim);
        }

        public AbiType ElementType { get; }

        public override byte[] Encode(object value)
        {
            if (value is Array)
            {
                return EncodeList((Array)value);
            }
            else if (value is string)
            {
                var array = JsonConvert.DeserializeObject<Array>((string)value);
                return EncodeList(array);
            }
            else
            {
                throw new NotSupportedException($"Value type '{value.GetType().Name}' is not supported.");
            }
        }

        public abstract byte[] EncodeList(Array l);

        public byte[] EncodeTuple(Array l)
        {
            byte[][] elems;
            if (ElementType.IsDynamicType)
            {
                elems = new byte[l.Length * 2][];
                var offset = l.Length * this.FixedSize;

                for (var i = 0; i < l.Length; i++)
                {
                    elems[i] = IntType.EncodeInt(offset);
                    byte[] encoded = this.ElementType.Encode(l.GetValue(i));
                    elems[l.Length + i] = encoded;
                    offset += (int)(this.FixedSize * ((encoded.Length - 1) / this.FixedSize + 1));
                }
            }
            else
            {
                elems = new byte[l.Length][];
                for (var i = 0; i < l.Length; i++)
                {
                    elems[i] = this.ElementType.Encode(l.GetValue(i));
                }
            }
            return ArrayUtils.Concat(elems);
        }

        public override object Decode(byte[] encoded, int offset = 0)
        {
            throw new NotImplementedException();
        }

        public dynamic DecodeTuple(byte[] encoded, int origOffset, int len)
        {
            var offset = origOffset;
            var ret = new object[len];

            for (var i = 0; i < len; i++)
            {
                if (this.ElementType.IsDynamicType)
                {
                    ret[i] = this.ElementType.Decode(encoded, origOffset + (int)IntType.DecodeInt(encoded, offset));
                }
                else
                {
                    ret[i] = this.ElementType.Decode(encoded, offset);
                }
                offset += this.ElementType.FixedSize;
            }

            return ret;
        }
    }
}

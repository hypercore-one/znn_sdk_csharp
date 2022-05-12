using System;

namespace Zenon.Abi
{
    public class StaticArrayType : ArrayType
    {
        public int size = 0;

        public StaticArrayType(string name) 
            : base(name) 
        {
            var idx1 = name.IndexOf('[');
            var idx2 = name.IndexOf(']', idx1);
            var dim = name.Substring(idx1 + 1, idx2);
            size = Int32.Parse(dim);
        }

        public override string CanonicalName => $"[{size}]";

        public override byte[] EncodeList(byte[] bytes)
        {
            if (bytes.Length != size)
                throw new ArgumentException("Bytes size must equal.");
            return this.EncodeTuple(bytes);
        }

        public override object Decode(byte[] encoded, int offset = 0)
        {
            var result = new byte[size][];

            for (var i = 0; i < size; i++)
            {
                result[i] =
                    (byte[])this.ElementType.Decode(encoded, offset + i * this.ElementType.FixedSize);
            }

            return result;
        }

        public override int FixedSize => this.ElementType.FixedSize * size;
    }
}

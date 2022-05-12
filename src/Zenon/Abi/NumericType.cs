using System;
using System.Globalization;
using System.Numerics;
using Zenon.Utils;

namespace Zenon.Abi
{
    public abstract class NumericType : AbiType
    {
        public NumericType(string name)
            : base(name)
        { }

        public BigInteger EncodeInternal(string value)
        {
            if (value.StartsWith("0x"))
            {
                return BigInteger.Parse(value.Substring(2), NumberStyles.HexNumber);
            }
            else if (value.Contains('a') ||
                value.Contains('b') ||
                value.Contains('c') ||
                value.Contains('d') ||
                value.Contains('e') ||
                value.Contains('f'))
            {
                return BigInteger.Parse(value.Substring(2), NumberStyles.HexNumber);
            }
            return BigInteger.Parse(value);
        }

        public BigInteger EncodeInternal(int value)
        {
            return new BigInteger((int)value);
        }

        public BigInteger EncodeInternal(long value)
        {
            return new BigInteger((long)value);
        }

        public BigInteger EncodeInternal(double value)
        {
            return new BigInteger((double)value);
        }

        public BigInteger EncodeInternal(byte[] value)
        {
            return BytesUtils.BytesToBigInt(value);
        }

        public BigInteger EncodeInternal(object value)
        {
            if (value is string)
            {
                return this.EncodeInternal((string)value);
            }
            else if (value is BigInteger)
            {
                return (BigInteger)value;
            }
            else if (value is int)
            {
                return this.EncodeInternal((int)value);
            }
            else if (value is long)
            {
                return this.EncodeInternal((long)value);
            }
            else if (value is double)
            {
                return this.EncodeInternal((double)value);
            }
            else if (value is byte[])
            {
                return this.EncodeInternal((byte[])value);
            }
            else
            {
                throw new NotSupportedException("Value type is not supported.");
            }
        }
    }
}

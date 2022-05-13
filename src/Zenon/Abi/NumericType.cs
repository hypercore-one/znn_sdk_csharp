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
            else if (value is byte)
            {
                return new BigInteger((byte)value);
            }
            else if (value is ushort)
            {
                return new BigInteger((ushort)value);
            }
            else if (value is uint)
            {
                return new BigInteger((uint)value);
            }
            else if (value is ulong)
            {
                return new BigInteger((ulong)value);
            }
            else if (value is sbyte)
            {
                return new BigInteger((sbyte)value);
            }
            else if (value is short)
            {
                return new BigInteger((short)value);
            }
            else if (value is int)
            {
                return new BigInteger((int)value);
            }
            else if (value is long)
            {
                return new BigInteger((long)value);
            }
            else if (value is double)
            {
                return new BigInteger((double)value);
            }
            else if (value is decimal)
            {
                return new BigInteger((decimal)value);
            }
            else if (value is byte[])
            {
                return BytesUtils.BytesToBigInt((byte[])value);
            }
            else
            {
                throw new NotSupportedException($"Value type '{value.GetType().Name}' is not supported.");
            }
        }
    }
}

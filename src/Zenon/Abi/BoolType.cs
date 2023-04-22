﻿using System;

namespace Zenon.Abi
{
    public class BoolType : IntType
    {
        public const string DefaultName = "bool";

        public BoolType()
            : base(DefaultName)
        { }

        public override byte[] Encode(object value)
        {
            if (value is string)
            {
                return base.Encode(Boolean.TrueString.Equals((string)value, StringComparison.InvariantCultureIgnoreCase) ? 1 : 0);
            }
            else if (value is bool)
            {
                return base.Encode((bool)value == true ? 1 : 0);
            }
            throw new NotSupportedException($"Value type '{value.GetType().Name}' is not supported.");
        }

        public override object Decode(byte[] encoded, int offset = 0)
        {
            return (base.Decode(encoded, offset).ToString() != "0");
        }
    }
}
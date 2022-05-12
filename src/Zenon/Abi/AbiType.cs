using System;

namespace Zenon.Abi
{
    public abstract class AbiType
    {
        public const int Int32Size = 32;

        protected AbiType(string name)
        {
            this.Name = name;
        }

        public virtual string Name { get; }

        public virtual string CanonicalName
        {
            get
            {
                return this.Name;
            }
        }
        
        public static AbiType GetType(string typeName)
        {
            if (typeName.Contains("[")) 
                return ArrayType.GetType(typeName);
            if (typeName == "bool")
                return new BoolType();
            if (typeName.StartsWith("int"))
                return new IntType(typeName);
            if (typeName.StartsWith("uint")) 
                return new UnsignedIntType(typeName);
            if (typeName == "address")
                return new AddressType();
            if (typeName == "tokenStandard")
                return new TokenStandardType();
            if (typeName == "string")
                return new StringType();
            if (typeName == "bytes")
                return BytesType.Bytes;
            if (typeName == "function")
                return new FunctionType();
            if (typeName == "hash")
                return new HashType(typeName);
            if (typeName.StartsWith("bytes"))
                return new Bytes32Type(typeName);

            throw new NotSupportedException($"The type {typeName} is not supported");
        }
        
        public abstract byte[] Encode(object value);

        public abstract object Decode(byte[] encoded, int offset = 0);

        public virtual int FixedSize
        {
            get
            {
                return 32;
            }
        }

        public virtual bool IsDynamicType
        {
            get
            {
                return false;
            }
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}

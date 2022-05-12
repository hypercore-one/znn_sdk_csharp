using System;
using Zenon.Utils;

namespace Zenon.Abi
{
    public class AbiFunction : Entry
    {
        public const int EncodedSignLength = 4;

        public AbiFunction(string name, Param[] inputs)
            : base(name, inputs, TypeEnum.Function)
        { }

        public object[] Decode(byte[] encoded)
        {
            return Param.DecodeList(
                this.Inputs, new ArraySegment<byte>(encoded, EncodedSignLength, encoded.Length).ToArray());
        }

        public byte[] Encode(params object[] args)
        {
            return ArrayUtils.Concat(this.EncodeSignature(), this.EncodeArguments(args));
        }

        public override byte[] EncodeSignature()
        {
            return ExtractSignature(base.EncodeSignature());
        }

        public static byte[] ExtractSignature(byte[] data)
        {
            return new ArraySegment<byte>(data, 0, EncodedSignLength).ToArray();
        }
    }
}

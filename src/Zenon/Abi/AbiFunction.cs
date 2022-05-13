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
                this.Inputs, encoded.Sublist(EncodedSignLength, encoded.Length));
        }

        public byte[] Encode(object[] args)
        {
            return ArrayUtils.Concat(this.EncodeSignature(), this.EncodeArguments(args));
        }

        public override byte[] EncodeSignature()
        {
            return ExtractSignature(base.EncodeSignature());
        }

        public static byte[] ExtractSignature(byte[] data)
        {
            return data.Sublist(0, EncodedSignLength);
        }
    }
}

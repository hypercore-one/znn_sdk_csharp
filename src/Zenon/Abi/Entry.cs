using System;
using System.Linq;
using System.Text;
using Zenon.Utils;

namespace Zenon.Abi
{
    public class Entry
    {
        public Entry(string name, Param[] inputs, TypeEnum type)
        {
            this.Name = name;
            this.Inputs = inputs;
            this.Type = type;
        }

        public string Name { get; }
        public Param[] Inputs { get; }
        public TypeEnum Type { get; }

        public virtual string FormatSignature()
        {
            var paramsTypes = String.Join(',', this.Inputs.Select(x => x.Type.CanonicalName));
            return $"{this.Name}({paramsTypes})";
        }

        public virtual byte[] FingerprintSignature()
        {
            return Crypto.Crypto.Digest(Encoding.UTF8.GetBytes(this.FormatSignature()));
        }

        public virtual byte[] EncodeSignature()
        {
            return this.FingerprintSignature();
        }

        public virtual byte[] EncodeArguments(object[] args)
        {
            if (args.Length > this.Inputs.Length)
                throw new ArgumentException("Arguments cannot be bigger than input size.", "args");

            var staticSize = 0;
            var dynamicCnt = 0;

            for (var i = 0; i < args.Length; i++)
            {
                var type = this.Inputs[i].Type;
                if (type.IsDynamicType)
                {
                    dynamicCnt++;
                }
                staticSize += type.FixedSize;
            }

            var bb = new byte[args.Length + dynamicCnt][];


            for (int curDynamicPtr = staticSize, curDynamicCnt = 0, i = 0; i < args.Length; i++)
            {
                var type = this.Inputs![i].Type;
                if (type.IsDynamicType)
                {
                    var dynBB = type.Encode(args[i]);
                    bb[i] = IntType.EncodeInt(curDynamicPtr);
                    bb[args.Length + curDynamicCnt] = dynBB;
                    curDynamicCnt++;
                    curDynamicPtr += dynBB.Length;
                }
                else
                {
                    bb[i] = type.Encode(args[i]);
                }
            }

            return ArrayUtils.Concat(bb);
        }
    }
}

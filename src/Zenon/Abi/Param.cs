using System.Collections.Generic;

namespace Zenon.Abi
{
    public class Param
    {
        public static object[] DecodeList(Param[] parameters, byte[] encoded)
        {
            var result = new List<object>();

            var offset = 0;

            foreach (var param in parameters)
            {
                var decoded = param.Type.IsDynamicType
                    ? param.Type.Decode(encoded, (int)IntType.DecodeInt(encoded, offset))
                    : param.Type.Decode(encoded, offset);
                result.Add(decoded);

                offset += param.Type.FixedSize;
            }

            return result.ToArray();
        }

        public Param(string name, AbiType type)
        {
            this.Name = name;
            this.Type = type;
        }
        public bool Indexed { get; set; } = false;
        public string Name { get; }
        public AbiType Type { get; }
    }
}

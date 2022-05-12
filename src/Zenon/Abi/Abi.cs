using System;
using System.Collections.Generic;
using System.Linq;
using Zenon.Abi.Json;

namespace Zenon.Abi
{
    public class Abi
    {
        private static Entry[] ParseEntries(JEntry[] json)
        {
            var entries = new List<Entry>();

            foreach (var entry in json)
            {
                if (entry.type != "function")
                {
                    throw new ZnnSdkException("Only ABI functions supported");
                }

                var inputs = entry.inputs != null
                    ? entry.inputs.Select(x => new Param(x.name, AbiType.GetType(x.type))).ToArray()
                    : new Param[0];

                entries.Add(new AbiFunction(entry.name, inputs));
            }

            return entries.ToArray();
        }

        public Abi(Entry[] entries)
        {
            this.Entries = entries;
        }

        public Abi(JEntry[] json)
        {
            this.Entries = ParseEntries(json);
        }
        public Entry[] Entries { get; }

        public byte[] EncodeFunction(string name, params object[] args)
        {
            var f = this.Entries
                .Where(x => x.Name == name)
                .Select(x => new AbiFunction(x.Name, x.Inputs))
                .First();

            return f.Encode(args);
        }

        public dynamic DecodeFunction(byte[] encoded)
        {
            var f = this.Entries
                .Where(x => AbiFunction.ExtractSignature(x.EncodeSignature()).SequenceEqual(AbiFunction.ExtractSignature(encoded)))
                .Select(x => new AbiFunction(x.Name, x.Inputs))
                .First();

            return f.Decode(encoded);
        }
    }
}

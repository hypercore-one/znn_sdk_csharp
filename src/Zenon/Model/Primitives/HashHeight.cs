﻿using Newtonsoft.Json;
using Zenon.Model.Primitives.Json;
using Zenon.Utils;

namespace Zenon.Model.Primitives
{
    public class HashHeight
    {
        public static readonly HashHeight Empty = new HashHeight(Hash.Empty, 0);

        public HashHeight(JHashHeight json)
        {
            Hash = Hash.Parse(json.hash);
            Height = json.height;
        }

        public HashHeight(Hash hash, ulong? height)
        {
            Hash = hash;
            Height = height;
        }

        public Hash Hash { get; }
        public ulong? Height { get; }

        public byte[] GetBytes()
        {
            return ArrayUtils.Concat(Hash.Bytes, BytesUtils.GetBytes(Height.Value));
        }

        public Json.JHashHeight ToJson()
        {
            return new Json.JHashHeight()
            {
                hash = Hash.ToString(),
                height = Height
            };
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(ToJson(), Formatting.None);
        }
    }
}
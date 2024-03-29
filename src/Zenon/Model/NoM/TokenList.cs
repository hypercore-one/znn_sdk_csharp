﻿using System.Linq;
using Zenon.Model.NoM.Json;

namespace Zenon.Model.NoM
{
    public class TokenList : IJsonConvertible<JTokenList>
    {
        public TokenList(JTokenList json)
        {
            Count = json.count;
            List = json.list != null ? json.list.Select(x => new Token(x)).ToArray() : new Token[0];
        }

        public TokenList(ulong count, Token[] list)
        {
            Count = count;
            List = list;
        }

        public ulong Count { get; }
        public Token[] List { get; }

        public virtual JTokenList ToJson()
        {
            var data = new JTokenList()
            {
                count = this.Count
            };

            if (this.List != null)
                data.list = this.List.Select(x => x.ToJson()).ToArray();

            return data;
        }
    }
}

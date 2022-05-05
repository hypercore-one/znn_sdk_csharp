using System.Linq;

namespace Zenon.Model.NoM
{
    public class TokenList
    {
        public TokenList(Json.JTokenList json)
        {
            Count = json.count;
            List = json.list != null ? json.list.Select(x => new Token(x)).ToArray() : new Token[0];
        }

        public TokenList(long? count, Token[] list)
        {
            Count = count;
            List = list;
        }

        public long? Count { get; }
        public Token[] List { get; }

        public virtual Json.JTokenList ToJson()
        {
            var data = new Json.JTokenList()
            {
                count = Count
            };

            if (List != null)
                data.list = List.Select(x => x.ToJson()).ToArray();

            return data;
        }
    }
}

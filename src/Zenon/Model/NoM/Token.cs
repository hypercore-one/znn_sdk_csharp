using System;
using Zenon.Model.NoM.Json;
using Zenon.Model.Primitives;

namespace Zenon.Model.NoM
{
    public class Token : IJsonConvertible<JToken>
    {
        public Token(JToken json)
        {
            Name = json.name;
            Symbol = json.symbol;
            Domain = json.domain;
            TotalSupply = json.totalSupply;
            Decimals = json.decimals;
            Owner = Address.Parse(json.owner);
            TokenStandard = TokenStandard.Parse(json.tokenStandard);
            MaxSupply = json.maxSupply;
            IsBurnable = json.isBurnable;
            IsMintable = json.isMintable;
            IsUtility = json.isUtility;
        }

        public Token(
            string name,
            string symbol,
            string domain,
            long totalSupply,
            long decimals,
            Address owner,
            TokenStandard tokenStandard,
            long maxSupply,
            bool isBurnable,
            bool isMintable,
            bool isUtility)
        {
            Name = name;
            Symbol = symbol;
            Domain = domain;
            TotalSupply = totalSupply;
            Decimals = decimals;
            Owner = owner;
            TokenStandard = tokenStandard;
            MaxSupply = maxSupply;
            IsBurnable = isBurnable;
            IsMintable = isMintable;
            IsUtility = isUtility;
        }

        public string Name { get; }
        public string Symbol { get; }
        public string Domain { get; }
        public long TotalSupply { get; }
        public long Decimals { get; }
        public Address Owner { get; }
        public TokenStandard TokenStandard { get; }
        public long MaxSupply { get; }
        public bool IsBurnable { get; }
        public bool IsMintable { get; }
        public bool IsUtility { get; }

        public virtual JToken ToJson()
        {
            return new JToken()
            {
                name = Name,
                symbol = Symbol,
                domain = Domain,
                totalSupply = TotalSupply,
                decimals = Decimals,
                owner = Owner.ToString(),
                tokenStandard = TokenStandard.ToString(),
                maxSupply = MaxSupply,
                isBurnable = IsBurnable,
                isMintable = IsMintable,
                isUtility = IsUtility,
            };
        }

        public override int GetHashCode()
        {
            return TokenStandard.ToString().GetHashCode();
        }

        public int DecimalsExponent
        {
            get
            {
                return (int)Math.Pow(10.0, Decimals);
            }

        }

        public override bool Equals(object obj) => Equals(obj as Token);

        public bool Equals(Token other)
        {
            return TokenStandard == other.TokenStandard;
        }
    }
}

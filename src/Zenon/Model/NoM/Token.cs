using System;
using System.Numerics;
using Zenon.Model.NoM.Json;
using Zenon.Model.Primitives;
using Zenon.Utils;

namespace Zenon.Model.NoM
{
    public class Token : IJsonConvertible<JToken>
    {
        public Token(JToken json)
        {
            Name = json.name;
            Symbol = json.symbol;
            Domain = json.domain;
            TotalSupply = AmountUtils.ParseAmount(json.totalSupply);
            Decimals = json.decimals;
            Owner = Address.Parse(json.owner);
            TokenStandard = TokenStandard.Parse(json.tokenStandard);
            MaxSupply = AmountUtils.ParseAmount(json.maxSupply);
            IsBurnable = json.isBurnable;
            IsMintable = json.isMintable;
            IsUtility = json.isUtility;
        }

        public Token(
            string name,
            string symbol,
            string domain,
            BigInteger totalSupply,
            long decimals,
            Address owner,
            TokenStandard tokenStandard,
            BigInteger maxSupply,
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
        public BigInteger TotalSupply { get; }
        public long Decimals { get; }
        public Address Owner { get; }
        public TokenStandard TokenStandard { get; }
        public BigInteger MaxSupply { get; }
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
                totalSupply = TotalSupply.ToString(),
                decimals = Decimals,
                owner = Owner.ToString(),
                tokenStandard = TokenStandard.ToString(),
                maxSupply = MaxSupply.ToString(),
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

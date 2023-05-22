using Zenon.Model.Embedded.Json;
using Zenon.Model.Primitives;

namespace Zenon.Model.Embedded
{
    public class TokenPair : IJsonConvertible<JTokenPair>
    {
        public TokenPair(JTokenPair json)
        {
            TokenStandard = TokenStandard.Parse(json.tokenStandard);
            TokenAddress = json.tokenAddress;
            Bridgeable = json.bridgeable;
            Redeemable = json.redeemable;
            Owned = json.owned;
            MinAmount = json.minAmount;
            FeePercentage = json.feePercentage;
            RedeemDelay = json.redeemDelay;
            Metadata = json.metadata;
        }

        public TokenStandard TokenStandard { get; }
        public string TokenAddress { get; }
        public bool Bridgeable { get; }
        public bool Redeemable { get; }
        public bool Owned { get; }
        public long MinAmount { get; }
        public int FeePercentage { get; }
        public int RedeemDelay { get; }
        public string Metadata { get; }

        public virtual JTokenPair ToJson()
        {
            return new JTokenPair()
            {
                tokenStandard = TokenStandard.ToString(),
                tokenAddress = TokenAddress.ToString(),
                bridgeable = Bridgeable,
                redeemable = Redeemable,
                owned = Owned,
                minAmount = MinAmount,
                feePercentage = FeePercentage,
                redeemDelay = RedeemDelay,
                metadata = Metadata
            };
        }
    }
}
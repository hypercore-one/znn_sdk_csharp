using System.Numerics;
using Zenon.Model.Embedded.Json;
using Zenon.Model.Primitives;
using Zenon.Utils;

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
            MinAmount = AmountUtils.ParseAmount(json.minAmount);
            FeePercentage = json.feePercentage;
            RedeemDelay = json.redeemDelay;
            Metadata = json.metadata;
        }

        public TokenStandard TokenStandard { get; }
        public string TokenAddress { get; }
        public bool Bridgeable { get; }
        public bool Redeemable { get; }
        public bool Owned { get; }
        public BigInteger MinAmount { get; }
        public uint FeePercentage { get; }
        public ulong RedeemDelay { get; }
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
                minAmount = MinAmount.ToString(),
                feePercentage = FeePercentage,
                redeemDelay = RedeemDelay,
                metadata = Metadata
            };
        }
    }
}
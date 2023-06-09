using System.Numerics;
using Zenon.Model.Embedded.Json;
using Zenon.Model.Primitives;
using Zenon.Utils;

namespace Zenon.Model.Embedded
{
    public class LiquidityStakeEntry : IJsonConvertible<JLiquidityStakeEntry>
    {
        public LiquidityStakeEntry(JLiquidityStakeEntry json)
        {
            Amount = AmountUtils.ParseAmount(json.amount);
            TokenStandard = TokenStandard.Parse(json.tokenStandard);
            WeightedAmount = AmountUtils.ParseAmount(json.weightedAmount);
            StartTime = json.startTime;
            RevokeTime = json.revokeTime;
            ExpirationTime = json.expirationTime;
        }

        public BigInteger Amount { get; }
        public TokenStandard TokenStandard { get; }
        public BigInteger WeightedAmount { get; }
        public long StartTime { get; }
        public long RevokeTime { get; }
        public long ExpirationTime { get; }

        public virtual JLiquidityStakeEntry ToJson()
        {
            return new JLiquidityStakeEntry()
            {
                amount = Amount.ToString(),
                tokenStandard = TokenStandard.ToString(),
                weightedAmount = WeightedAmount.ToString(),
                startTime = StartTime,
                revokeTime = RevokeTime,
                expirationTime = ExpirationTime,
            };
        }
    }
}

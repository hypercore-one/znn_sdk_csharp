using Zenon.Model.Embedded.Json;
using Zenon.Model.Primitives;

namespace Zenon.Model.Embedded
{
    public class LiquidityStakeEntry : IJsonConvertible<JLiquidityStakeEntry>
    {
        public LiquidityStakeEntry(JLiquidityStakeEntry json)
        {
            Amount = json.amount;
            TokenStandard = TokenStandard.Parse(json.tokenStandard);
            WeightedAmount = json.weightedAmount;
            StartTime = json.startTime;
            RevokeTime = json.revokeTime;
            ExpirationTime = json.expirationTime;
        }

        public long Amount { get; }
        public TokenStandard TokenStandard { get; }
        public long WeightedAmount { get; }
        public long StartTime { get; }
        public long RevokeTime { get; }
        public long ExpirationTime { get; }

        public virtual JLiquidityStakeEntry ToJson()
        {
            return new JLiquidityStakeEntry()
            {
                amount = Amount,
                tokenStandard = TokenStandard.ToString(),
                weightedAmount = WeightedAmount,
                startTime = StartTime,
                revokeTime = RevokeTime,
                expirationTime = ExpirationTime,
            };
        }
    }
}

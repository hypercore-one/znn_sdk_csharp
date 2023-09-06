using System.Linq;
using System.Numerics;
using Zenon.Model.Embedded.Json;
using Zenon.Utils;

namespace Zenon.Model.Embedded
{
    public class LiquidityStakeList : IJsonConvertible<JLiquidityStakeList>
    {
        public LiquidityStakeList(JLiquidityStakeList json)
        {
            TotalAmount = AmountUtils.ParseAmount(json.totalAmount);
            TotalWeightedAmount = AmountUtils.ParseAmount(json.totalWeightedAmount);
            Count = json.count;
            List = json.list != null
                ? json.list.Select(x => new LiquidityStakeEntry(x)).ToArray()
                : new LiquidityStakeEntry[0];
        }

        public BigInteger TotalAmount { get; }
        public BigInteger TotalWeightedAmount { get; }
        public ulong Count { get; }
        public LiquidityStakeEntry[] List { get; }

        public virtual JLiquidityStakeList ToJson()
        {
            return new JLiquidityStakeList()
            {
                totalAmount = TotalAmount.ToString(),
                totalWeightedAmount = TotalWeightedAmount.ToString(),
                count = Count,
                list = List.Select(x => x.ToJson()).ToArray()
            };
        }
    }
}

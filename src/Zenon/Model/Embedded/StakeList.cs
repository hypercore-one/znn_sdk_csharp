using System.Linq;
using System.Numerics;
using Zenon.Model.Embedded.Json;
using Zenon.Utils;

namespace Zenon.Model.Embedded
{
    public class StakeList : IJsonConvertible<JStakeList>
    {
        public StakeList(JStakeList json)
        {
            TotalAmount = AmountUtils.ParseAmount(json.totalAmount);
            TotalWeightedAmount = AmountUtils.ParseAmount(json.totalWeightedAmount);
            Count = json.count;
            List = json.list != null
                ? json.list.Select(x => new StakeEntry(x)).ToArray()
                : new StakeEntry[0];
        }

        public StakeList(BigInteger totalAmount, BigInteger totalWeightedAmount, ulong count, StakeEntry[] list)
        {
            TotalAmount = totalAmount;
            TotalWeightedAmount = totalWeightedAmount;
            Count = count;
            List = list;
        }

        public BigInteger TotalAmount { get; }
        public BigInteger TotalWeightedAmount { get; }
        public ulong Count { get; }
        public StakeEntry[] List { get; }

        public virtual JStakeList ToJson()
        {
            return new JStakeList()
            {
                totalAmount = TotalAmount.ToString(),
                totalWeightedAmount = TotalWeightedAmount.ToString(),
                count = Count,
                list = List.Select(x => x.ToJson()).ToArray()
            };
        }
    }
}
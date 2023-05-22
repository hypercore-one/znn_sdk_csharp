using System.Linq;
using Zenon.Model.Embedded.Json;

namespace Zenon.Model.Embedded
{
    public class StakeList : IJsonConvertible<JStakeList>
    {
        public StakeList(JStakeList json)
        {
            TotalAmount = json.totalAmount;
            TotalWeightedAmount = json.totalWeightedAmount;
            Count = json.count;
            List = json.list != null
                ? json.list.Select(x => new StakeEntry(x)).ToArray()
                : new StakeEntry[0];
        }

        public StakeList(long totalAmount, long totalWeightedAmount, long count, StakeEntry[] list)
        {
            TotalAmount = totalAmount;
            TotalWeightedAmount = totalWeightedAmount;
            Count = count;
            List = list;
        }

        public long TotalAmount { get; }
        public long TotalWeightedAmount { get; }
        public long Count { get; }
        public StakeEntry[] List { get; }

        public virtual JStakeList ToJson()
        {
            return new JStakeList()
            {
                totalAmount = TotalAmount,
                totalWeightedAmount = TotalWeightedAmount,
                count = Count,
                list = List.Select(x => x.ToJson()).ToArray()
            };
        }
    }
}
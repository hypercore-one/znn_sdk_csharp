using System.Linq;

namespace Zenon.Model.Embedded
{
    public class StakeList
    {
        public StakeList(Json.JStakeList json)
        {
            TotalAmount = json.totalAmount;
            TotalWeightedAmount = json.totalWeightedAmount;
            Count = json.count;
            List = json.list.Select(x => new StakeEntry(x)).ToArray();
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

        public virtual Json.JStakeList ToJson()
        {
            return new Json.JStakeList()
            {
                totalAmount = TotalAmount,
                totalWeightedAmount = TotalWeightedAmount,
                count = Count,
                list = List.Select(x => x.ToJson()).ToArray()
            };
        }
    }
}
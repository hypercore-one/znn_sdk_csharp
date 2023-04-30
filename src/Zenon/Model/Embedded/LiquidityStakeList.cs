using System.Linq;
using Zenon.Model.Embedded.Json;

namespace Zenon.Model.Embedded
{
    public class LiquidityStakeList : IJsonConvertible<JLiquidityStakeList>
    {
        public LiquidityStakeList(JLiquidityStakeList json)
        {
            Count = json.count;
            List = json.list != null
                ? json.list.Select(x => new LiquidityStakeEntry(x)).ToArray()
                : new LiquidityStakeEntry[0];
        }

        public long Count { get; }
        public LiquidityStakeEntry[] List { get; }

        public virtual JLiquidityStakeList ToJson()
        {
            return new JLiquidityStakeList()
            {
                count = Count,
                list = List.Select(x => x.ToJson()).ToArray()
            };
        }
    }
}

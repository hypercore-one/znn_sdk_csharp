using System.Linq;
using Zenon.Model.Embedded.Json;

namespace Zenon.Model.Embedded
{
    public class RewardHistoryList : IJsonConvertible<JRewardHistoryList>
    {
        public RewardHistoryList(JRewardHistoryList json)
        {
            Count = json.count;
            List = json.list != null
                ? json.list.Select(x => new RewardHistoryEntry(x)).ToArray()
                : new RewardHistoryEntry[0];
        }

        public ulong Count { get; }
        public RewardHistoryEntry[] List { get; }

        public virtual JRewardHistoryList ToJson()
        {
            return new JRewardHistoryList()
            {
                count = Count,
                list = List.Select(x => x.ToJson()).ToArray()
            };
        }
    }
}

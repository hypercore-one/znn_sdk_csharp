using System.Linq;
using Zenon.Model.Embedded.Json;

namespace Zenon.Model.Embedded
{
    public class PillarEpochHistoryList : IJsonConvertible<JPillarEpochHistoryList>
    {
        public PillarEpochHistoryList(JPillarEpochHistoryList json)
        {
            Count = json.count;
            List = json.list != null
                ? json.list.Select(x => new PillarEpochHistory(x)).ToArray()
                : new PillarEpochHistory[0];
        }

        public ulong Count { get; }
        public PillarEpochHistory[] List { get; }

        public virtual JPillarEpochHistoryList ToJson()
        {
            return new JPillarEpochHistoryList()
            {
                count = Count,
                list = List.Select(x => x.ToJson()).ToArray()
            };
        }
    }
}

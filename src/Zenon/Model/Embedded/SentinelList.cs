using System.Linq;
using Zenon.Model.Embedded.Json;

namespace Zenon.Model.Embedded
{
    public class SentinelInfoList : IJsonConvertible<JSentinelInfoList>
    {
        public SentinelInfoList(JSentinelInfoList json)
        {
            Count = json.count;
            List = json.list.Select(x => new SentinelInfo(x)).ToArray();
        }

        public SentinelInfoList(long count, SentinelInfo[] list)
        {
            Count = count;
            List = list;
        }

        public long Count { get; }
        public SentinelInfo[] List { get; }

        public virtual JSentinelInfoList ToJson()
        {
            return new JSentinelInfoList()
            {
                count = Count,
                list = List.Select(x => x.ToJson()).ToArray()
            };
        }
    }
}
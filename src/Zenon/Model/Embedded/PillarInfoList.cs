using System.Linq;
using Zenon.Model.Embedded.Json;

namespace Zenon.Model.Embedded
{
    public class PillarInfoList : IJsonConvertible<JPillarInfoList>
    {
        public PillarInfoList(JPillarInfoList json)
        {
            Count = json.count;
            List = json.list.Select(x => new PillarInfo(x)).ToArray();
        }

        public long Count { get; }
        public PillarInfo[] List { get; }

        public virtual JPillarInfoList ToJson()
        {
            return new JPillarInfoList()
            {
                count = Count,
                list = List.Select(x => x.ToJson()).ToArray()
            };
        }
    }
}

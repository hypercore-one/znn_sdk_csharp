using System.Linq;
using Zenon.Model.NoM.Json;

namespace Zenon.Model.NoM
{
    public class DetailedMomentumList
    {
        public DetailedMomentumList(JDetailedMomentumList json)
        {
            Count = json.count;
            List = json.list != null ? json.list.Select(x => new DetailedMomentum(x)).ToArray() : null;
        }

        public long? Count { get; }
        public DetailedMomentum[] List { get; }

        public virtual JDetailedMomentumList ToJson()
        {
            return new JDetailedMomentumList()
            {
                count = Count,
                list = List != null ? List.Select(x => x.ToJson()).ToArray() : null
            };
        }
    }
}
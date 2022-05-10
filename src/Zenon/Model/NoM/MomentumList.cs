using System.Linq;
using Zenon.Model.NoM.Json;

namespace Zenon.Model.NoM
{
    public class MomentumList : IJsonConvertible<Json.JMomentumList>
    {
        public MomentumList(JMomentumList json)
        {
            Count = json.count;
            List = json.list.Select(x => new Momentum(x)).ToArray();
        }

        public long Count { get; }
        public Momentum[] List { get; }

        public virtual JMomentumList ToJson()
        {
            return new JMomentumList()
            {
                count = Count,
                list = List.Select(x => x.ToJson()).ToArray()
            };
        }
    }
}

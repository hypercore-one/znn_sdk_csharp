using System.Linq;
using Zenon.Model.Embedded.Json;

namespace Zenon.Model.Embedded
{
    public class SporkList : IJsonConvertible<JSporkList>
    {
        public SporkList(JSporkList json)
        {
            Count = json.count;
            List = json.list != null
                ? json.list.Select(x => new Spork(x)).ToArray()
                : new Spork[0];
        }

        public ulong Count { get; }
        public Spork[] List { get; }

        public virtual JSporkList ToJson()
        {
            return new JSporkList()
            {
                count = Count,
                list = List.Select(x => x.ToJson()).ToArray()
            };
        }
    }
}

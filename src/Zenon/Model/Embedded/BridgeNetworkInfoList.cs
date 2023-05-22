using System.Linq;
using Zenon.Model.Embedded.Json;

namespace Zenon.Model.Embedded
{
    public class BridgeNetworkInfoList : IJsonConvertible<JBridgeNetworkInfoList>
    {
        public BridgeNetworkInfoList(JBridgeNetworkInfoList json)
        {
            Count = json.count;
            List = json.list != null
                ? json.list.Select(x => new BridgeNetworkInfo(x)).ToArray()
                : new BridgeNetworkInfo[0];
        }

        public long Count { get; }
        public BridgeNetworkInfo[] List { get; }

        public virtual JBridgeNetworkInfoList ToJson()
        {
            return new JBridgeNetworkInfoList()
            {
                count = Count,
                list = List.Select(x => x.ToJson()).ToArray()
            };
        }
    }
}

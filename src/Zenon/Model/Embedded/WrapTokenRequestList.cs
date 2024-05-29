using System.Linq;
using Zenon.Model.Embedded.Json;

namespace Zenon.Model.Embedded
{
    public class WrapTokenRequestList : IJsonConvertible<JWrapTokenRequestList>
    {
        public WrapTokenRequestList(JWrapTokenRequestList json)
        {
            Count = json.count;
            List = json.list != null
                ? json.list.Select(x => new WrapTokenRequest(x)).ToArray()
                : new WrapTokenRequest[0];
        }

        public ulong Count { get; }
        public WrapTokenRequest[] List { get; }

        public virtual JWrapTokenRequestList ToJson()
        {
            return new JWrapTokenRequestList()
            {
                count = Count,
                list = List.Select(x => x.ToJson()).ToArray()
            };
        }
    }
}

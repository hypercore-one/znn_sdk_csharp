using System.Linq;
using Zenon.Model.Embedded.Json;

namespace Zenon.Model.Embedded
{
    public class UnwrapTokenRequestList : IJsonConvertible<JUnwrapTokenRequestList>
    {
        public UnwrapTokenRequestList(JUnwrapTokenRequestList json)
        {
            Count = json.count;
            List = json.list != null
                ? json.list.Select(x => new UnwrapTokenRequest(x)).ToArray()
                : new UnwrapTokenRequest[0];
        }

        public long Count { get; }
        public UnwrapTokenRequest[] List { get; }

        public virtual JUnwrapTokenRequestList ToJson()
        {
            return new JUnwrapTokenRequestList()
            {
                count = Count,
                list = List.Select(x => x.ToJson()).ToArray()
            };
        }
    }
}

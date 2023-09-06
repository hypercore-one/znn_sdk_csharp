using System.Linq;
using Zenon.Model.NoM.Json;

namespace Zenon.Model.NoM
{
    public class AccountBlockList : IJsonConvertible<JAccountBlockList>
    {
        public AccountBlockList(JAccountBlockList json)
        {
            Count = json.count;
            List = json.list != null
                ? json.list.Select(x => new AccountBlock(x)).ToArray()
                : new AccountBlock[0];
            More = json.more;
        }

        public AccountBlockList(ulong count, AccountBlock[] list, bool more)
        {
            Count = count;
            List = list;
            More = more;
        }

        public ulong Count { get; }
        public AccountBlock[] List { get; }

        /// If true, there are more than `count` elements, but only these can be retrieved
        public bool More { get; }

        public virtual JAccountBlockList ToJson()
        {
            return new JAccountBlockList()
            {
                count = Count,
                list = List != null ? List.Select(x => x.ToJson()).ToArray() : null,
                more = More
            };
        }
    }
}
using System.Linq;
using Zenon.Model.Embedded.Json;

namespace Zenon.Model.Embedded
{
    public class TimeChallengesList : IJsonConvertible<JTimeChallengesList>
    {
        public TimeChallengesList(JTimeChallengesList json)
        {
            Count = json.count;
            List = json.list != null
                ? json.list.Select(x => new TimeChallengeInfo(x)).ToArray()
                : new TimeChallengeInfo[0];
        }

        public ulong Count { get; }
        public TimeChallengeInfo[] List { get; }

        public virtual JTimeChallengesList ToJson()
        {
            return new JTimeChallengesList()
            {
                count = Count,
                list = List.Select(x => x.ToJson()).ToArray()
            };
        }
    }
}

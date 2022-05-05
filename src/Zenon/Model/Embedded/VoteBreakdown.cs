using Newtonsoft.Json;
using Zenon.Model.Embedded.Json;
using Zenon.Model.Primitives;

namespace Zenon.Model.Embedded
{
    public class VoteBreakdown
    {
        public VoteBreakdown(JVoteBreakdown json)
        {
            Id = Hash.Parse(json.id);
            Yes = json.yes;
            No = json.no;
            Total = json.total;
        }

        public Hash Id { get; }
        public long Yes { get; }
        public long No { get; }
        public long Total { get; }

        public virtual JVoteBreakdown ToJson()
        {
            return new JVoteBreakdown()
            {
                id = Id.ToString(),
                yes = Yes,
                no = No,
                total = Total
            };
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(ToJson(), Formatting.None);
        }
    }
}

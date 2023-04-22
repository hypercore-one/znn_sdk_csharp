using Zenon.Model.Embedded.Json;
using Zenon.Model.Primitives;

namespace Zenon.Model.Embedded
{
    public class PillarVote : IJsonConvertible<JPillarVote>
    {
        public PillarVote(JPillarVote json)
        {
            Id = Hash.Parse(json.id);
            Name = json.name;
            Vote = json.vote;
        }

        public Hash Id { get; }
        public string Name { get; }
        public long Vote { get; }

        public virtual JPillarVote ToJson()
        {
            return new JPillarVote()
            {
                id = Id.ToString(),
                name = Name,
                vote = Vote
            };
        }
    }
}
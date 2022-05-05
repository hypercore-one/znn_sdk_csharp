using Zenon.Model.Primitives;

namespace Zenon.Model.Embedded
{
    public class PillarVote
    {
        public PillarVote(Json.JPillarVote json)
        {
            Id = Hash.Parse(json.id);
            Name = json.name;
            Vote = json.vote;
        }

        public Hash Id { get; }
        public string Name { get; }
        public long Vote { get; }

        public virtual Json.JPillarVote ToJson()
        {
            return new Json.JPillarVote()
            {
                id = Id.ToString(),
                name = Name,
                vote = Vote
            };
        }
    }
}
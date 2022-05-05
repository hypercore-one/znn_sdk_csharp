using Zenon.Model.Primitives;

namespace Zenon.Model.NoM
{
    public class MomentumShort
    {
        public MomentumShort(Json.JMomentumShort json)
        {
            Hash = Hash.Parse(json.hash);
            Height = json.height;
            Timestamp = json.timestamp;
        }

        public Hash Hash { get; }
        public long? Height { get; }
        public long? Timestamp { get; }

        public virtual Json.JMomentumShort ToJson()
        {
            return new Json.JMomentumShort()
            {
                hash = Hash.ToString(),
                height = Height,
                timestamp = Timestamp
            };
        }
    }
}

using Zenon.Model.NoM.Json;
using Zenon.Model.Primitives;

namespace Zenon.Model.NoM
{
    public class MomentumShort : IJsonConvertible<JMomentumShort>
    {
        public MomentumShort(JMomentumShort json)
        {
            Hash = Hash.Parse(json.hash);
            Height = json.height;
            Timestamp = json.timestamp;
        }

        public Hash Hash { get; }
        public long? Height { get; }
        public long? Timestamp { get; }

        public virtual JMomentumShort ToJson()
        {
            return new JMomentumShort()
            {
                hash = Hash.ToString(),
                height = Height,
                timestamp = Timestamp
            };
        }
    }
}

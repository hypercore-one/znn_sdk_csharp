using Zenon.Model.Embedded.Json;
using Zenon.Model.Primitives;

namespace Zenon.Model.Embedded
{
    public class Spork : IJsonConvertible<JSpork>
    {
        public Spork(JSpork json)
        {
            Id = Hash.Parse(json.id);
            Name = json.name;
            Description = json.description;
            Activated = json.activated;
            EnforcementHeight = json.enforcementHeight;
        }

        public Hash Id { get; }
        public string Name { get; }
        public string Description { get; }
        public bool Activated { get; }
        public long EnforcementHeight { get; }

        public virtual JSpork ToJson()
        {
            return new JSpork()
            {
                id = Id.ToString(),
                name = Name,
                description = Description,
                activated = Activated,
                enforcementHeight = EnforcementHeight,
            };
        }
    }
}

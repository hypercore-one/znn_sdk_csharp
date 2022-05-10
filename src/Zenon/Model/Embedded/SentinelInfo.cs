using Zenon.Model.Embedded.Json;
using Zenon.Model.Primitives;

namespace Zenon.Model.Embedded
{
    public class SentinelInfo : IJsonConvertible<JSentinelInfo>
    {
        public SentinelInfo(JSentinelInfo json)
        {
            Owner = Address.Parse(json.owner);
            RegistrationTimestamp = json.registrationTimestamp;
            IsRevocable = json.isRevocable;
            RevokeCooldown = json.revokeCooldown;
            Active = json.active;
        }

        public SentinelInfo(Address address, long registrationTimestamp, bool isRevocable, long revokeCooldown, bool active)
        {
            Owner = address;
            RegistrationTimestamp = registrationTimestamp;
            IsRevocable = isRevocable;
            RevokeCooldown = revokeCooldown;
            Active = active;
        }

        public Address Owner { get; }
        public long RegistrationTimestamp { get; }
        public bool IsRevocable { get; }
        public long RevokeCooldown { get; }
        public bool Active { get; }

        public virtual JSentinelInfo ToJson()
        {
            return new JSentinelInfo()
            {
                owner = Owner.ToString(),
                registrationTimestamp = RegistrationTimestamp,
                isRevocable = IsRevocable,
                revokeCooldown = RevokeCooldown,
                active = Active
            };
        }
    }
}
using System.Linq;
using Zenon.Model.Embedded.Json;
using Zenon.Model.Primitives;

namespace Zenon.Model.Embedded
{
    public class SecurityInfo : IJsonConvertible<JSecurityInfo>
    {
        public SecurityInfo(JSecurityInfo json)
        {
            Guardians = json.guardians.Select(x => Address.Parse(x)).ToArray();
            GuardiansVotes = json.guardiansVotes.Select(x => Address.Parse(x)).ToArray();
            AdministratorDelay = json.administratorDelay;
            SoftDelay = json.softDelay;
        }

        public Address[] Guardians { get; }
        public Address[] GuardiansVotes { get; }
        public long AdministratorDelay { get; }
        public long SoftDelay { get; }

        public virtual JSecurityInfo ToJson()
        {
            return new JSecurityInfo()
            {
                guardians = Guardians.Select(x => x.ToString()).ToArray(),
                guardiansVotes = GuardiansVotes.Select(x => x.ToString()).ToArray(),
                administratorDelay = AdministratorDelay,
                softDelay = SoftDelay,
            };
        }
    }
}
using System.Numerics;
using Zenon.Model.Embedded.Json;
using Zenon.Utils;

namespace Zenon.Model.Embedded
{
    public class DelegationInfo : IJsonConvertible<JDelegationInfo>
    {
        public DelegationInfo(JDelegationInfo json)
        {
            Name = json.name;
            Status = json.status;
            Weight = AmountUtils.ParseAmount(json.weight);
        }

        public DelegationInfo(string name, long status, BigInteger weight)
        {
            Name = name;
            Status = status;
            Weight = weight;
        }

        public string Name { get; }
        public long Status { get; }
        public BigInteger Weight { get; }

        public bool IsPillarActive => Status == 1;

        public virtual JDelegationInfo ToJson()
        {
            return new JDelegationInfo()
            {
                name = Name,
                status = Status,
                weight = Weight.ToString()
            };
        }
    }
}

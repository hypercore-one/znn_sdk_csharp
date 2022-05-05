using Zenon.Model.Embedded.Json;
using Zenon.Utils;

namespace Zenon.Model.Embedded
{
    public class DelegationInfo
    {
        public DelegationInfo(JDelegationInfo json)
        {
            Name = json.name;
            Status = json.status;
            Weight = json.weight;
            WeightWithDecimals = AmountUtils.AddDecimals(Weight, 8);
        }

        public DelegationInfo(string name, long status, long weight)
        {
            Name = name;
            Status = status;
            Weight = weight;
            WeightWithDecimals = AmountUtils.AddDecimals(Weight, 8);
        }

        public string Name { get; }
        public long Status { get; }
        public long Weight { get; }
        public double WeightWithDecimals { get; }

        public bool IsPillarActive => Status == 1;

        public virtual JDelegationInfo ToJson()
        {
            return new JDelegationInfo()
            {
                name = Name,
                status = Status,
                weight = Weight
            };
        }
    }
}

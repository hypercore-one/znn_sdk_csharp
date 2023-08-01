using Newtonsoft.Json;
using System.Numerics;
using Zenon.Model.Embedded.Json;
using Zenon.Model.Primitives;

namespace Zenon.Model.Embedded
{
    public class ZtsFeesInfo
    {
        public ZtsFeesInfo(JZtsFeesInfo json)
        {
            TokenStandard = TokenStandard.Parse(json.tokenStandard);
            AccumulatedFee = BigInteger.Parse(json.accumulatedFee);
        }

        public TokenStandard TokenStandard { get; }
        public BigInteger AccumulatedFee { get; }

        public virtual JZtsFeesInfo ToJson()
        {
            return new JZtsFeesInfo()
            {
                tokenStandard = TokenStandard.ToString(),
                accumulatedFee = AccumulatedFee.ToString(),
            };
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(ToJson(), Formatting.None);
        }
    }
}
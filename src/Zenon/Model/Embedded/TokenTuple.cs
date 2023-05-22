using Zenon.Model.Embedded.Json;
using Zenon.Model.Primitives;

namespace Zenon.Model.Embedded
{
    public class TokenTuple : IJsonConvertible<JTokenTuple>
    {
        public TokenTuple(JTokenTuple json)
        {
            TokenStandard = TokenStandard.Parse(json.tokenStandard);
            ZnnPercentage = json.znnPercentage;
            QsrPercentage = json.qsrPercentage;
            MinAmount = json.minAmount;
        }

        public TokenStandard TokenStandard { get; }
        public long ZnnPercentage { get; }
        public long QsrPercentage { get; }
        public long MinAmount { get; }

        public virtual JTokenTuple ToJson()
        {
            return new JTokenTuple()
            {
                tokenStandard = TokenStandard.ToString(),
                znnPercentage = ZnnPercentage,
                qsrPercentage = QsrPercentage,
                minAmount = MinAmount
            };
        }
    }
}

using System.Linq;
using Zenon.Model.Embedded.Json;
using Zenon.Model.Primitives;

namespace Zenon.Model.Embedded
{
    public class LiquidityInfo : IJsonConvertible<JLiquidityInfo>
    {
        public LiquidityInfo(JLiquidityInfo json)
        {
            Administrator = Address.Parse(json.administrator);
            IsHalted = json.isHalted;
            ZnnReward = json.znnReward;
            QsrReward = json.qsrReward;
            TokenTuples = json.tokenTuples != null
                ? json.tokenTuples.Select(x => new TokenTuple(x)).ToArray()
                : new TokenTuple[0];
        }

        public Address Administrator { get; }
        public bool IsHalted { get; }
        public long ZnnReward { get; }
        public long QsrReward { get; }
        public TokenTuple[] TokenTuples { get; }

        public virtual JLiquidityInfo ToJson()
        {
            return new JLiquidityInfo()
            {
                administrator = Administrator.ToString(),
                isHalted = IsHalted,
                znnReward = ZnnReward,
                qsrReward = QsrReward,
                tokenTuples = TokenTuples.Select(x => x.ToJson()).ToArray()
            };
        }
    }
}
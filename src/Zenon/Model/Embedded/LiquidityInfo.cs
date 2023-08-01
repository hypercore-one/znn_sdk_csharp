using System.Linq;
using System.Numerics;
using Zenon.Model.Embedded.Json;
using Zenon.Model.Primitives;
using Zenon.Utils;

namespace Zenon.Model.Embedded
{
    public class LiquidityInfo : IJsonConvertible<JLiquidityInfo>
    {
        public LiquidityInfo(JLiquidityInfo json)
        {
            Administrator = Address.Parse(json.administrator);
            IsHalted = json.isHalted;
            ZnnReward = AmountUtils.ParseAmount(json.znnReward);
            QsrReward = AmountUtils.ParseAmount(json.qsrReward);
            TokenTuples = json.tokenTuples != null
                ? json.tokenTuples.Select(x => new TokenTuple(x)).ToArray()
                : new TokenTuple[0];
        }

        public Address Administrator { get; }
        public bool IsHalted { get; }
        public BigInteger ZnnReward { get; }
        public BigInteger QsrReward { get; }
        public TokenTuple[] TokenTuples { get; }

        public virtual JLiquidityInfo ToJson()
        {
            return new JLiquidityInfo()
            {
                administrator = Administrator.ToString(),
                isHalted = IsHalted,
                znnReward = ZnnReward.ToString(),
                qsrReward = QsrReward.ToString(),
                tokenTuples = TokenTuples.Select(x => x.ToJson()).ToArray()
            };
        }
    }
}
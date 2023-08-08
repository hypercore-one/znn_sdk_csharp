using System.Numerics;
using Zenon.Model.Embedded.Json;
using Zenon.Utils;

namespace Zenon.Model.Embedded
{
    public class RewardHistoryEntry : IJsonConvertible<JRewardHistoryEntry>
    {
        public RewardHistoryEntry(JRewardHistoryEntry json)
        {
            Epoch = json.epoch;
            ZnnAmount = AmountUtils.ParseAmount(json.znnAmount);
            QsrAmount = AmountUtils.ParseAmount(json.qsrAmount);
        }

        public long Epoch { get; }
        public BigInteger ZnnAmount { get; }
        public BigInteger QsrAmount { get; }

        public virtual JRewardHistoryEntry ToJson()
        {
            return new JRewardHistoryEntry()
            {
                epoch = Epoch,
                znnAmount = ZnnAmount.ToString(),
                qsrAmount = QsrAmount.ToString()
            };
        }
    }
}

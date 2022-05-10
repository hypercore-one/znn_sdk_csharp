using Zenon.Model.Embedded.Json;

namespace Zenon.Model.Embedded
{
    public class RewardHistoryEntry : IJsonConvertible<JRewardHistoryEntry>
    {
        public RewardHistoryEntry(JRewardHistoryEntry json)
        {
            Epoch = json.epoch;
            ZnnAmount = json.znnAmount;
            QsrAmount = json.qsrAmount;
        }

        public long Epoch { get; }
        public long ZnnAmount { get; }
        public long QsrAmount { get; }

        public virtual JRewardHistoryEntry ToJson()
        {
            return new JRewardHistoryEntry()
            {
                epoch = Epoch,
                znnAmount = ZnnAmount,
                qsrAmount = QsrAmount
            };
        }
    }
}

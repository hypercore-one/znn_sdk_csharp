using Zenon.Model.Embedded.Json;

namespace Zenon.Model.Embedded
{
    public class PillarEpochHistory : IJsonConvertible<JPillarEpochHistory>
    {
        public PillarEpochHistory(JPillarEpochHistory json)
        {
            Name = json.name;
            Epoch = json.epoch;
            GiveBlockRewardPercentage = json.giveBlockRewardPercentage;
            GiveDelegateRewardPercentage = json.giveDelegateRewardPercentage;
            ProducedBlockNum = json.producedBlockNum;
            ExpectedBlockNum = json.expectedBlockNum;
            Weight = json.weight;
        }

        public PillarEpochHistory(
            string name,
            long epoch,
            long giveBlockRewardPercentage,
            long giveDelegateRewardPercentage,
            long producedBlockNum,
            long expectedBlockNum,
            long weight)
        {
            Name = name;
            Epoch = epoch;
            GiveBlockRewardPercentage = giveBlockRewardPercentage;
            GiveDelegateRewardPercentage = giveDelegateRewardPercentage;
            ProducedBlockNum = producedBlockNum;
            ExpectedBlockNum = expectedBlockNum;
            Weight = weight;
        }

        public string Name { get; }
        public long Epoch { get; }
        public long GiveBlockRewardPercentage { get; }
        public long GiveDelegateRewardPercentage { get; }
        public long ProducedBlockNum { get; }
        public long ExpectedBlockNum { get; }
        public long Weight { get; }

        public virtual JPillarEpochHistory ToJson()
        {
            return new JPillarEpochHistory()
            {
                name = Name,
                epoch = Epoch,
                giveBlockRewardPercentage = GiveBlockRewardPercentage,
                giveDelegateRewardPercentage = GiveDelegateRewardPercentage,
                producedBlockNum = ProducedBlockNum,
                expectedBlockNum = ExpectedBlockNum,
                weight = Weight
            };
        }
    }
}

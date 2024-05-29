using System.Numerics;
using Zenon.Model.Embedded.Json;
using Zenon.Utils;

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
            Weight = AmountUtils.ParseAmount(json.weight);
        }

        public PillarEpochHistory(
            string name,
            ulong epoch,
            int giveBlockRewardPercentage,
            int giveDelegateRewardPercentage,
            int producedBlockNum,
            int expectedBlockNum,
            BigInteger weight)
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
        public ulong Epoch { get; }
        public int GiveBlockRewardPercentage { get; }
        public int GiveDelegateRewardPercentage { get; }
        public int ProducedBlockNum { get; }
        public int ExpectedBlockNum { get; }
        public BigInteger Weight { get; }

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
                weight = Weight.ToString()
            };
        }
    }
}

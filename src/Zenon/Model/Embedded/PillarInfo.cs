using System.Numerics;
using Zenon.Model.Embedded.Json;
using Zenon.Model.Primitives;
using Zenon.Utils;

namespace Zenon.Model.Embedded
{
    public class PillarInfo : IJsonConvertible<JPillarInfo>
    {
        public const int UnknownType = 0;
        public const int LegacyPillarType = 1;
        public const int RegularPillarType = 2;

        public PillarInfo(JPillarInfo json)
        {
            Name = json.name;
            Rank = json.rank;
            Type = json.type; // UnknownType
            OwnerAddress = Address.Parse(json.ownerAddress);
            ProducerAddress = Address.Parse(json.producerAddress);
            WithdrawAddress = Address.Parse(json.withdrawAddress);
            GiveMomentumRewardPercentage = json.giveMomentumRewardPercentage;
            GiveDelegateRewardPercentage = json.giveDelegateRewardPercentage;
            IsRevocable = json.isRevocable;
            RevokeCooldown = json.revokeCooldown;
            RevokeTimestamp = json.revokeTimestamp;
            CurrentStats = new PillarEpochStats(json.currentStats);
            Weight = AmountUtils.ParseAmount(json.weight);
            ProducedMomentums = json.producedMomentums;
            ExpectedMomentums = json.expectedMomentums;
        }

        public string Name { get; }
        public int Rank { get; }
        public int Type { get; }
        public Address OwnerAddress { get; }
        public Address ProducerAddress { get; }
        public Address WithdrawAddress { get; }
        public long GiveMomentumRewardPercentage { get; }
        public long GiveDelegateRewardPercentage { get; }
        public bool IsRevocable { get; }
        public long RevokeCooldown { get; }
        public long RevokeTimestamp { get; }
        public PillarEpochStats CurrentStats { get; }
        public BigInteger Weight { get; }
        public int ProducedMomentums { get; }
        public int ExpectedMomentums { get; }

        public virtual JPillarInfo ToJson()
        {
            return new JPillarInfo()
            {
                name = Name,
                rank = Rank,
                ownerAddress = OwnerAddress.ToString(),
                producerAddress = ProducerAddress.ToString(),
                withdrawAddress = WithdrawAddress.ToString(),
                isRevocable = IsRevocable,
                revokeCooldown = RevokeCooldown,
                currentStats = CurrentStats.ToJson(),
                weight = Weight.ToString()
            };
        }
    }
}

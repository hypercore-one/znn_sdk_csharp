namespace Zenon.Model.Embedded.Json
{
    public class JPillarInfo
    {
        public string name { get; set; }
        public long rank { get; set; }
        public int type { get; set; }
        public string ownerAddress { get; set; }
        public string producerAddress { get; set; }
        public string withdrawAddress { get; set; }
        public long giveMomentumRewardPercentage { get; set; }
        public long giveDelegateRewardPercentage { get; set; }
        public bool isRevocable { get; set; }
        public long revokeCooldown { get; set; }
        public long revokeTimestamp { get; set; }
        public JPillarEpochStats currentStats { get; set; }
        public long weight { get; set; }
        public long producedMomentums { get; set; }
        public long expectedMomentums { get; set; }
    }
}

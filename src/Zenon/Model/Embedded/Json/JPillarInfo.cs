namespace Zenon.Model.Embedded.Json
{
    public class JPillarInfo
    {
        public string name { get; set; }
        public int rank { get; set; }
        public int type { get; set; }
        public string ownerAddress { get; set; }
        public string producerAddress { get; set; }
        public string withdrawAddress { get; set; }
        public int giveMomentumRewardPercentage { get; set; }
        public int giveDelegateRewardPercentage { get; set; }
        public bool isRevocable { get; set; }
        public long revokeCooldown { get; set; }
        public long revokeTimestamp { get; set; }
        public JPillarEpochStats currentStats { get; set; }
        public string weight { get; set; }
        public int producedMomentums { get; set; }
        public int expectedMomentums { get; set; }
    }
}

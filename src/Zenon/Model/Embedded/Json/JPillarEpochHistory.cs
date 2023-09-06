namespace Zenon.Model.Embedded.Json
{
    public class JPillarEpochHistory
    {
        public string name { get; set; }
        public ulong epoch { get; set; }
        public int giveBlockRewardPercentage { get; set; }
        public int giveDelegateRewardPercentage { get; set; }
        public int producedBlockNum { get; set; }
        public int expectedBlockNum { get; set; }
        public string weight { get; set; }
    }
}

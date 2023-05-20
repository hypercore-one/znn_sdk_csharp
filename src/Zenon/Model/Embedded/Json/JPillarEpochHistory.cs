namespace Zenon.Model.Embedded.Json
{
    public class JPillarEpochHistory
    {
        public string name { get; set; }
        public long epoch { get; set; }
        public long giveBlockRewardPercentage { get; set; }
        public long giveDelegateRewardPercentage { get; set; }
        public long producedBlockNum { get; set; }
        public long expectedBlockNum { get; set; }
        public string weight { get; set; }
    }
}

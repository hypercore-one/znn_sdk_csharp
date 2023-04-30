namespace Zenon.Model.Embedded.Json
{
    public class JLiquidityInfo
    {
        public string administrator { get; set; }
        public bool isHalted { get; set; }
        public long znnReward { get; set; }
        public long qsrReward { get; set; }
        public string[] tokenTuples { get; set; }
    }
}
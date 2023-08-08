namespace Zenon.Model.Embedded.Json
{
    public class JLiquidityInfo
    {
        public string administrator { get; set; }
        public bool isHalted { get; set; }
        public string znnReward { get; set; }
        public string qsrReward { get; set; }
        public JTokenTuple[] tokenTuples { get; set; }
    }
}
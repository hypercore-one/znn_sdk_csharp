namespace Zenon.Model.NoM.Json
{
    public class JBalanceInfoListItem
    {
        public JToken token { get; set; }
        public long? balance { get; set; }
        public double? balanceWithDecimals { get; set; }
        public string balanceFormatted { get; set; }
    }
}

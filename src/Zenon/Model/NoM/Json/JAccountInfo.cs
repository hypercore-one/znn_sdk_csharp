namespace Zenon.Model.NoM.Json
{
    public class JAccountInfo
    {
        public string address { get; set; }
        public long? accountHeight { get; set; }
        public JBalanceInfoListItem[] balanceInfoMap { get; set; }
    }
}

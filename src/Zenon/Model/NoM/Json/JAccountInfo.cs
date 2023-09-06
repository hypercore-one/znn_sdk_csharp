using System.Collections.Generic;

namespace Zenon.Model.NoM.Json
{
    public class JAccountInfo
    {
        public string address { get; set; }
        public ulong? accountHeight { get; set; }
        public IDictionary<string, JBalanceInfoListItem> balanceInfoMap { get; set; }
    }
}

using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Zenon.Model.NoM.Json;
using Zenon.Model.Primitives;

namespace Zenon.Model.NoM
{
    public class AccountInfo : IJsonConvertible<JAccountInfo>
    {
        public string Address { get; }
        public long? BlockCount { get; }
        public BalanceInfoListItem[] BalanceInfoList { get; }

        public AccountInfo(JAccountInfo json)
        {
            Address = json.address;
            BlockCount = json.accountHeight;
            BalanceInfoList = this.BlockCount > 0 
                ? json.balanceInfoMap.Select(x => new BalanceInfoListItem(x.Value)).ToArray()
                : new BalanceInfoListItem[0];
        }

        public AccountInfo(string address, long? blockCount, BalanceInfoListItem[] balanceInfoList)
        {
            Address = address;
            BlockCount = blockCount;
            BalanceInfoList = balanceInfoList;
        }

        public long? Znn => GetBalance(TokenStandard.ZnnZts);

        public long? Qsr => GetBalance(TokenStandard.QsrZts);

        public long GetBalance(TokenStandard tokenStandard)
        {
            var info = BalanceInfoList!.FirstOrDefault(
                x => x.Token!.TokenStandard == tokenStandard);
            return info?.Balance ?? 0;
        }
        public double GgetBalanceWithDecimals(TokenStandard tokenStandard)
        {
            var info = BalanceInfoList!.FirstOrDefault(
                x => x.Token!.TokenStandard == tokenStandard);
            return info?.BalanceWithDecimals! ?? 0;
        }

        public Token FindTokenByTokenStandard(TokenStandard tokenStandard)
        {
            try
            {
                return BalanceInfoList!
                    .First((x) => x.Token!.TokenStandard == tokenStandard)
                    .Token;
            }
            catch
            {
                return null;
            }
        }

        public virtual JAccountInfo ToJson()
        {
            return new JAccountInfo()
            {
                address = this.Address.ToString(),
                accountHeight = this.BlockCount,
                balanceInfoMap = this.BalanceInfoList
                    .ToDictionary(x => x.Token.TokenStandard.ToString(), x => x.ToJson())
            };
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(ToJson(), Formatting.None);
        }
    }
}